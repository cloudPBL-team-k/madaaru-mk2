using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace madaarumk2 {
    public class WrappedHttpClient {
        
        public async Task<string> GetStringAsync(string reqUrl) {
            string rtnString = "";
            try{
                HttpClient hc = new HttpClient();
                rtnString = await hc.GetStringAsync(reqUrl);
            }catch(HttpRequestException e){
                throw;
            }
            //HttpClient hc = new HttpClient();
            //string rtnString = await hc.GetStringAsync(reqUrl);
            return rtnString;
        }

        public async Task<HttpResponseMessage> PostAsync(string reqUrl, HttpContent content) {
            HttpResponseMessage rtnHRM = new HttpResponseMessage();
            try{
                HttpClient hc = new HttpClient();
                rtnHRM = await hc.PostAsync(reqUrl, content);
            }catch(HttpRequestException e){
                throw;
            }
            //HttpClient hc = new HttpClient();
            //HttpResponseMessage rtnHRM = await hc.PostAsync(reqUrl, content);
            return rtnHRM;
        }

    }
}
