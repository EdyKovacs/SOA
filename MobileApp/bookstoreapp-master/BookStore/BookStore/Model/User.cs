using System;
using Newtonsoft.Json;

namespace BookStore.Model
{
    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
