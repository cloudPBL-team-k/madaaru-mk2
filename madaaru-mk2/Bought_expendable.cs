using System;
using Newtonsoft.Json;

namespace madaarumk2 {
    //消耗品のオブジェクト
    [JsonObject("bought_expendable")]
    public class Bought_expendable {
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("thing_id")]
        public int thing_id { get; set; }
        [JsonProperty("limit")]
        public string limit { get; set; }
    }
}
