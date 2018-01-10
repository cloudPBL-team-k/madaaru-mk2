using System;

namespace madaarumk2 {
    //[JsonObject("user")]
    public class User {
        //[JsonProperty("name")]
        public string name { get; set; }
        //[JsonProperty("password")]
        public string password { get; set; }
        //[JsonProperty("id")]
        public int id { get; set; }
        //[JsonProperty("token")]
        public string token { get; set; }
        //public string Email { get; set; }
    }
}
