using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madaarumk2 {
    //買ったものを登録するときにPOSTして使うJson
    [JsonObject("bought_thing")]
    public class Bought_thing {
        //ものを買ったユーザーのid
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        //物のid
        [JsonProperty("thing_id")]
        public int thing_id { get; set; }
        //個数
        [JsonProperty("num")]
        public int num { get; set; }
    }
}