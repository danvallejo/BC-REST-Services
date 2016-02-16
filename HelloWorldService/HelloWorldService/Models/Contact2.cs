using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorldService.Models
{
    public class Contact2 : ModelContact
    {
        [JsonProperty("age")]
        public int Age { get; set; }
    }
}