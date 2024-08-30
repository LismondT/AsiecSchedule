using AsiecSchedule.Models;
using HtmlAgilityPack;
using System.Globalization;

namespace AsiecSchedule.Data.Asiec
{
    public class AsiecParser
    {
        private static HttpClient? _httpClient;

        private static HttpClient Client
        {
            get
            {
                HttpClientHandler clientHandler = new()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                return _httpClient ??= new HttpClient(clientHandler);
            }
        }

        public static async Task<string> GetSheduleBody(string id, RequestType type, DateTime firstDate, DateTime secondDate)
        {
            string firstDateFormat = firstDate.ToString("yyyy-MM-dd");
            string secondDateFormat = secondDate.ToString("yyyy-MM-dd");

            string requestId = AsiecData.GetIDValue(id, type);

            Dictionary<string, string> data = FillContentByIdType(type, requestId, firstDateFormat, secondDateFormat);

            var content = new FormUrlEncodedContent(data);

            Client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Client.DefaultRequestHeaders.Add("User-Agent", "Defined");
            Client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            var response = await Client.PostAsync("https://www.asiec.ru/ras/ras.php", content);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return body;
        }

        private static Dictionary<string, string> FillContentByIdType(RequestType type, string id, string firstDate, string secondDate)
        {
            Dictionary<string, string> content;

            string idKey;
            string rasValue;

            switch (type)
            {
                case RequestType.GroupId:
                    idKey = "gruppa";
                    rasValue = "GRUP";
                    break;

                case RequestType.TeacherId:
                    idKey = "prepod";
                    rasValue = "PREP";
                    break;

                case RequestType.ClassroomId:
                    idKey = "auditoria";
                    rasValue = "AUD";
                    break;

                default:
                    idKey = "";
                    rasValue = "";
                    break;
            }


            content = new Dictionary<string, string>
            {
                { "dostup", "True" },
                { idKey, id },
                { "calendar", firstDate },
                { "calendar2", secondDate },
                { "Content-Type", "application/x-www-form-urlencoded" },
                { "ras", rasValue }
            };

            return content;
        }

        //TODO: Переделать Парсер
        public static async Task<ScheduleModel?> GetSchedule(string id, RequestType type, DateTime firstDate, DateTime secondDate)
        {
            ScheduleModel schedule = new()
            {
                FirstDate = firstDate,
                LastDate = secondDate,
                Days = []
            };

            HtmlDocument document = new();
            string body = await GetSheduleBody(id, type, firstDate, secondDate);

            document.LoadHtml(body);

            HtmlNode tbody = document.DocumentNode.SelectSingleNode("//tbody");

            if (tbody.ChildNodes.Count < 2)
                return schedule;

            DayModel? day = null;
            LessonModel? lesson = null; 

            foreach (HtmlNode node in tbody.SelectNodes("//td"))
            {
                bool hasDataLabelAttr = node.Attributes.Contains("data-label");
                string dataLabelValue = hasDataLabelAttr ? node.Attributes["data-label"].Value : "";

                if (node.HasClass("den"))
                {
                    string innerText = node.InnerText;
                    string[] parts = innerText.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    string dateString = parts[1];
                    DateTime date = DateTime.ParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                    if (day != null) schedule.Days.Add(day);
                    day = new DayModel(date, []);
                }

                if (node.HasClass("para_b"))
                {
                    string innerText = node.InnerText;
                    string[] parts = innerText.Split(new char[] { '(', '-', ')' }, StringSplitOptions.RemoveEmptyEntries);

                    int number = int.Parse(parts[0].Trim());

                    _ = TimeSpan.TryParse(parts[1].Trim(), out TimeSpan startTime);
                    _ = TimeSpan.TryParse(parts[2].Trim(), out TimeSpan endTime);

                    lesson = new LessonModel()
                    {
                        Number = number,
                        StartTime = startTime,
                        EndTime = endTime
                    };
                }

                if (node.HasClass("group_b"))
                {
                    lesson.Group = node.InnerText;
                }

                if (dataLabelValue == "Дисциплина")
                {
                    lesson.Name = node.InnerText;
                }

                if (dataLabelValue == "Преподаватель")
                {
                    lesson.Teacher = node.InnerText;
                }

                if (node.HasClass("ter_pc"))
                {
                    lesson.Territory = node.InnerText;
                }

                if (node.HasClass("aud_pc"))
                {
                    lesson.Classroom = node.InnerText;
                    lesson.Date = day?.Date;
 
                    day?.Add(lesson);
                }
            }

            schedule.Days.Add(day);

            return schedule;
        }
    }
}
