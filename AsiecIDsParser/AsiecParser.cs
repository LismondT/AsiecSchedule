namespace AsiecIDsParser
{
    public enum RequestType
    {
        GroupId,
        TeacherId,
        ClassroomId
    }

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

        public static async Task<string> GetIDsPageBody(RequestType type)
        {
            string url = "";

            switch (type)
            {
                case RequestType.GroupId: url = "https://www.asiec.ru/ras/filter_grup.php";
                    break;
                case RequestType.TeacherId: url = "https://www.asiec.ru/ras/filter_prep.php";
                    break;
                case RequestType.ClassroomId: url = "https://www.asiec.ru/ras/filter_aud.php";
                    break;
            }

            Dictionary<string, string> data = new()
            {
                { "dostup", "True" },
            };

            FormUrlEncodedContent content = new(data);

            Client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Client.DefaultRequestHeaders.Add("User-Agent", "Defined");
            Client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            HttpResponseMessage response = await Client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}