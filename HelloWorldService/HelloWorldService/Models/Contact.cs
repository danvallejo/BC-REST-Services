using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HelloWorldService.Models
{
    public class ModelContact
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
        public ModelPhone[] Phones { get; set; }
    }

    public class ModelPhone
    {
        [JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Number { get; set; }

        [JsonProperty("phone_type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ModelPhoneType PhoneType { get; set; }
    }

    public enum ModelPhoneType
    {
        Unknown,
        Home,
        Mobile,
    }
}