using System;
using Newtonsoft.Json;

namespace madaarumk2 {
    [JsonObject("g_buy_thing")]
    public class G_Buy_Thing {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("thing_id")]
        public int thing_id { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("num")]
        public int num { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("alias")]
        public string alias { get; set; }
    }
}
