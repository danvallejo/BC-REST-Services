using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace HelloWorldClient
{
    public class ParseClient
    {
        private const string applicationId = "GqcoNDhC584cdpuUCYwrBU8dK5ZYLA3AImPb1EeK";
        private const string restApiKey = "MKErB2Euy6lfdQwCpU7SFusBEY7XAJTnhjSVVDuO";
        private HttpClient httpClient;

        public class PostResult
        {
            [JsonProperty("createdAt")]
            public DateTime CreatedAt;

            [JsonProperty("objectId")]
            public string Id;
        }

        public ParseClient()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.parse.com/1/classes/");
            httpClient.DefaultRequestHeaders.Add("X-Parse-Application-Id", "GqcoNDhC584cdpuUCYwrBU8dK5ZYLA3AImPb1EeK");
            httpClient.DefaultRequestHeaders.Add("X-Parse-REST-API-Key", "MKErB2Euy6lfdQwCpU7SFusBEY7XAJTnhjSVVDuO");
        }

        public PostResult Post(string table, object obj)
        {
            var newJson = JsonConvert.SerializeObject(obj);
            var postContent = new StringContent(newJson, System.Text.Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(table, postContent).Result;
            var json = result.Content.ReadAsStringAsync().Result;

            var postedResult = JsonConvert.DeserializeObject<PostResult>(json);

            return postedResult;
        }

        public T Get<T>(string table, string objectId)
        {
            var result = httpClient.GetAsync(table + "/" + objectId).Result;

            var json = result.Content.ReadAsStringAsync().Result;

            var getResult = JsonConvert.DeserializeObject<T>(json);

            return getResult;
        }
    }
}
