using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace madaarumk2 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage {
        public SignUpPage() {
            InitializeComponent();
        }

        async void SignupBtnClicked(object sender, EventArgs s) {
            SignupReq signupReq = new SignupReq();
            try {
                signupReq.name = nameInput.Text;
                if (passInput.Text != passInput2.Text) {
                    DependencyService.Get<IMyFormsToast>().Show("パスワードが一致しません");
                    return;
                }
                signupReq.password = passInput.Text;
            } catch(NullReferenceException e) {
                DependencyService.Get<IMyFormsToast>().Show("NULL EXCEPTION ERROR: name,passがNullです:" + e.Message);
                return;
            }

            // サーバにサインアップ
            WrappedHttpClient wHttpClient = new WrappedHttpClient();
            String baseURL = ServerInfo.url;
            String jsonString = JsonConvert.SerializeObject(signupReq);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await wHttpClient.PostAsync(baseURL + "/users", content);
            String result = await response.Content.ReadAsStringAsync();
            SignupRes signupRes = JsonConvert.DeserializeObject<SignupRes>(result);
            if(signupRes.status != 200) {
                // サインアップ失敗
                DependencyService.Get<IMyFormsToast>().Show("サインアップ失敗");
                return;
            } else {
                //Login出来たフラグを立ててメインページに遷移
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Navigation.PopAsync();
            }
        }
    }

    class SignupReq {
        public string name { get; set; }
        public string password { get; set; }
    }

    class SignupRes {
        public int status { get; set; }
    }
}