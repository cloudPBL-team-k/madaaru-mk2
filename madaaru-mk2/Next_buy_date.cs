using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madaarumk2 {
    //買ったものを登録した後に返される次に買うべき日付
    [JsonObject("next_buy_date")]
    public class Next_buy_date {
        //この商品の次に買うべき日付
        [JsonProperty("next_buy_date")]
        public string next_buy_date { get; set; }
    }
}