using HtmlAgilityPack;

namespace AsiecIDsParser
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Начинаем...");

            
            Console.WriteLine("Получаем страницу групп...");
            string groupbody = await AsiecParser.GetIDsPageBody(RequestType.GroupId);
            Console.WriteLine("Получена\n");

            Console.WriteLine("Получаем страницу преподавателей...");
            string teacherbody = await AsiecParser.GetIDsPageBody(RequestType.TeacherId);
            Console.WriteLine("Получена\n");

            Console.WriteLine("Получаем страницу аудиторий...");
            string classroombody = await AsiecParser.GetIDsPageBody(RequestType.ClassroomId);
            Console.WriteLine("Получена\n");


            Console.WriteLine("Парсим страницу групп...");
            var groupIds = ParseBody(groupbody, RequestType.GroupId);
            Console.WriteLine("Запарсено\n");

            Console.WriteLine("Парсим страницу преподавателей...");
            var teacherIds = ParseBody(teacherbody, RequestType.TeacherId);
            Console.WriteLine("Запарсено\n");

            Console.WriteLine("Парсим страницу аудиторий...");
            var classroomIds = ParseBody(classroombody, RequestType.ClassroomId);
            Console.WriteLine("Запарсено\n");

            
            Console.WriteLine("Сохраняем id групп");
            SaveDictionary("groups.txt", groupIds);
            Console.WriteLine("Сохраненно\n");

            Console.WriteLine("Сохраняем id преподавателей");
            SaveDictionary("teachers.txt", teacherIds);
            Console.WriteLine("Сохраненно\n");

            Console.WriteLine("Сохраняем id аудиторий");
            SaveDictionary("classrooms.txt", classroomIds);
            Console.WriteLine("Сохраненно\n");
        }

        private static Dictionary<string, string> ParseBody(string body, RequestType type)
        {
            Dictionary<string, string> idNamePairs = [];
            string xpath = "";

            switch (type)
            {
                case RequestType.GroupId: xpath = "//*[@id=\"gruppa\"]";
                    break;
                case RequestType.TeacherId: xpath = "//*[@id=\"prepod\"]";
                    break;
                case RequestType.ClassroomId: xpath = "//*[@id=\"auditoria\"]";
                    break;
            }

            HtmlDocument doc = new();
            doc.LoadHtml(body);

            HtmlNode root = doc.DocumentNode.SelectSingleNode(xpath);

            foreach (HtmlNode node in root.ChildNodes)
            {
                if (node.Name != "option")
                    continue;

                string id = node.Attributes["value"].Value;
                string name = node.InnerText;

                idNamePairs.Add(id, name);
            }

            return idNamePairs;
        }

        private static void SaveDictionary(string filename, Dictionary<string, string> pairs)
        {
            string text = "";

            foreach(KeyValuePair<string, string> pair in pairs)
            {
                string key = pair.Key.Replace("\n", "").Replace("\r", "");
                string value = pair.Value.Replace("\n", "").Replace("\r", "");
                text += $"{{ \"{value}\", \"{key}\" }}, \n";
            }

            File.WriteAllText(filename, text);
        }
    }
}
