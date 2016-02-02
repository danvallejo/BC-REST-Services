using System;
using System.Net.Http;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace HelloWorldClient
{
    public class Contact
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("phones")]
        public Phone[] Phones { get; set; }
    }

    public class Phone
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("phone_type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        Unknown,
        Home,
        Mobile,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/helloworldservice/api/");
            client.DefaultRequestHeaders.Add("Api-Key", "value");

            var newContact = new Contact
            {
                Name = "New Name",
                Phones = new[] { 
                    new Phone { 
                        Number = "425-111-2222",
                        PhoneType = PhoneType.Mobile
                    }
                }
            };

            var newJson = JsonConvert.SerializeObject(newContact);
            var postContent = new StringContent(newJson, System.Text.Encoding.UTF8, "application/json");
            var postResult = client.PostAsync("contacts", postContent).Result;

            Console.WriteLine(postResult.StatusCode);

            var result = client.GetAsync("contacts").Result;

            var json = result.Content.ReadAsStringAsync().Result;

            Console.WriteLine(json);
            
            var list = JsonConvert.DeserializeObject<List<Contact>>(json);

            foreach (var item in list)
            {
                Console.WriteLine("Id={0} Name={1}", item.Id, item.Name);
            }

            // Delete
            var deleteResult = client.DeleteAsync("contacts/" + list[0].Id).Result;
            Console.WriteLine("Delete result={0}", deleteResult.StatusCode);

            var score = new NewScore {
                myPlayer= "Mike",
                myScore= 501
            };

            var parseClient = new ParseClient();

            var posted = parseClient.Post("AnotherSetOfScores", score);

            var getted = parseClient.Get<NewScore>("AnotherSetOfScores", posted.Id);

            Console.ReadLine();
        }

        class NewScore
        {
            public string myPlayer;
            public int myScore;
        }
    }
}