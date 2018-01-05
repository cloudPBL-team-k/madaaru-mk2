using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madaarumk2 {
    //[JsonArray]
    //public class SearchedInfos{public List<SearchedInfo> JSON;}
    //Jsonをシリアライズするときに使うクラス
    [JsonObject("searchedinfo")]
    public class SearchedInfo {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("jancode")]
        public string Jancode { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreateDate { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdateDate { get; set; }
    }
}