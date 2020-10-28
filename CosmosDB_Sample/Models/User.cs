using Newtonsoft.Json;
using System;

namespace CosmosDB_Sample.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
