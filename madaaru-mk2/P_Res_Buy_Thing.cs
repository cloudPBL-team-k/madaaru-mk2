using System;
using Newtonsoft.Json;

namespace madaarumk2 {
    [JsonObject("p_res_buy_thing")]
    public class P_Res_Buy_Thing {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("thing_id")]
        public int thing_id { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("num")]
        public int num { get; set; }
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }
    }
}
