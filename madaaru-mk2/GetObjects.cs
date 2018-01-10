using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace madaarumk2 {
    public class GetObjects {
        
        public async Task<SearchedInfo> GetItemInfo(string jancode) {
            string serverUrl = ServerInfo.url;
            string searchAPIUrl = "/things/search";
            string reqUrl = $"{serverUrl}{searchAPIUrl}?jancode={jancode}";

            SearchedInfo thingInfo = new SearchedInfo();
            WrappedHttpClient whc = new WrappedHttpClient();
            //Jsonを取得できなかった時の例外のハンドリング
            try {
                string jsonString = await whc.GetStringAsync(reqUrl);
                thingInfo = JsonConvert.DeserializeObject<SearchedInfo>(jsonString);
            } catch (HttpRequestException e) {
                DependencyService.Get<IMyFormsToast>().Show("HttpClient ERROR: " + e.Message);
            }
            return thingInfo;
        }

        //Jsonを受け取ってオブジェクトに変換
        public SearchedInfo GetItemObjectFromJson(string jsonString) {
            SearchedInfo thingInfo = JsonConvert.DeserializeObject<SearchedInfo>(jsonString);
            return thingInfo;
        }

        public List<Expendables> GetAllItemsObjectFromJson(string jsonString) {
            List<Expendables> expendablesInfo = JsonConvert.DeserializeObject<List<Expendables>>(jsonString);
            return expendablesInfo;
        }

        //public async Task<List<Bought_things>> GetAllItemsInfo(int user_id)
        //{
        //    string serverUrl = ServerInfo.url;
        //    string searchAPIUrl = "/bought_things";
        //    string reqUrl = $"{serverUrl}{searchAPIUrl}?user_id={user_id}";

        //    HttpClient whc = new HttpClient();
        //    string jsonString = await whc.GetStringAsync(reqUrl);
        //    List<Bought_things> thingsInfo = JsonConvert.DeserializeObject<List<Bought_things>>(jsonString);
        //    return thingsInfo;
        //}

        public async Task<string> GetExpendablesInfo(int user_id) {
            string serverUrl = ServerInfo.url;
            string searchAPIUrl = "/expendables.json";
            string reqUrl = $"{serverUrl}{searchAPIUrl}?user_id={user_id}";

            WrappedHttpClient whc = new WrappedHttpClient();

            string jsonString = await whc.GetStringAsync(reqUrl);
            return jsonString;
        }

        //jsonの生データを表示するデバッグ用
        public async Task<string> GetItemJsonString(string jancode) {
            string serverUrl = ServerInfo.url;
            string searchAPIUrl = "/things/search";
            string reqUrl = $"{serverUrl}{searchAPIUrl}?jancode={jancode}";

            WrappedHttpClient whc = new WrappedHttpClient();

            string jsonString = await whc.GetStringAsync(reqUrl);
            return jsonString;
        }


        public async Task<Expendables> GetExpendablesObject(int user_id) {
            string serverUrl = ServerInfo.url;
            string searchAPIUrl = "/expendables.json";
            string reqUrl = $"{serverUrl}{searchAPIUrl}?=user_id={user_id}";

            WrappedHttpClient whc = new WrappedHttpClient();
            string jsonString = await whc.GetStringAsync(reqUrl);
            Expendables expendables = JsonConvert.DeserializeObject<Expendables>(jsonString);
            return expendables;
        }

        public async Task<List<G_Buy_Thing>> GetBuyThingObjects(int user_id){
            string serverUrl = ServerInfo.url;
            string searchAPIUrl = "/bought_things/exists_bought";
            string reqUrl = $"{serverUrl}{searchAPIUrl}?=user_id={user_id}";
            //bought_things/exists_bought?user_id=1;


            WrappedHttpClient whc = new WrappedHttpClient();
            string jsonString = await whc.GetStringAsync(reqUrl);
            List<G_Buy_Thing> G_Buy_Thing_Objects_List= JsonConvert.DeserializeObject<List<G_Buy_Thing>>(jsonString);

            return G_Buy_Thing_Objects_List;
        }
    }
}
