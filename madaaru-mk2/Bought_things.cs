﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madaarumk2 {
    //買ったもの(BoughtThing)を取得する
    [JsonObject("bought_things")]
    public class Bought_things {
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



