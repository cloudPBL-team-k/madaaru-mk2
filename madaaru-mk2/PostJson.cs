using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Forms;
using System.Text;
using System.Linq;

namespace madaarumk2 {
    public class PostJson {
        //買う商品の情報オブジェクトBought_thingsを受け取ってJson化してPost後、
        //サーバーから帰ってくる[次にこの商品を買うべき日付]をオブジェクト化して変えす
        //public async Task<List<Next_buy_date>> PostBoughtThingsInfo(Bought_things bt)
        public async Task<Next_buy_date> PostBoughtThingInfo(Bought_thing bt) {
            string serverUrl = ServerInfo.url;
            string APIUrl = "/bought_things";
            string reqUrl = $"{serverUrl}{APIUrl}";

            string jsonString = JsonConvert.SerializeObject(bt);

            //HttpClient hc = new HttpClient();
            WrappedHttpClient whc = new WrappedHttpClient();

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await hc.PostAsync(reqUrl, content);
            HttpResponseMessage response = await whc.PostAsync(reqUrl, content);

            string result = await response.Content.ReadAsStringAsync();
            Next_buy_date NBD = JsonConvert.DeserializeObject<Next_buy_date>(result);

            return NBD;
        }

        //Expendables情報をpost後、その情報をもう一度返す
        //返り値のExpendablesは使い道ないので捨てて良い
        //なら返り値要らないのでは
        public async Task<Expendables> PostExpendablesInfo(Bought_expendable be) {
            string serverUrl = ServerInfo.url;
            string APIUrl = "/expendables.json";
            string reqUrl = $"{serverUrl}{APIUrl}";

            string jsonString = JsonConvert.SerializeObject(be);

            //HttpClient hc = new HttpClient();
            WrappedHttpClient whc = new WrappedHttpClient();

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await hc.PostAsync(reqUrl, content);
            HttpResponseMessage response = await whc.PostAsync(reqUrl, content);


            //result string => ""
            //resut無いのに必要？
            string result = await response.Content.ReadAsStringAsync();
            Expendables ex = JsonConvert.DeserializeObject<Expendables>(result);
            return ex;
        }
    }
}
