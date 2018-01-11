using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madaarumk2{
    //買ったものを登録するときにPOSTして使うJson
    [JsonObject("buy_thing")]
    public class Buy_thing{
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