using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HelloWorldService.Models
{
    public class Contact
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// This is the name field
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// This is the date added field
        /// </summary>
        [JsonProperty("date_added", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime DateAdded { get; set; }

        [JsonProperty("phones", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Phone[] Phones { get; set; }
    }

    public class Phone
    {
        [JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
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
}