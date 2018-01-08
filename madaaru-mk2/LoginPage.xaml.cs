using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }

        async void LoginBtnClicked(object sender, EventArgs s) {
            User inputUser = new User();
            try {
                inputUser.name = nameInput.Text;
                inputUser.password = passInput.Text;
                inputUser.id = 0;
            } catch(NullReferenceException e) {
                DependencyService.Get<IMyFormsToast>().Show("NULL EXCEPTION ERROR: name,passがNullです:" + e.Message);
                return;
            }

            // サーバにログイン
            WrappedHttpClient wHttpClient = new WrappedHttpClient();
            String baseURL = ServerInfo.url;
            String jsonString = JsonConvert.SerializeObject(inputUser);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await wHttpClient.PostAsync(baseURL + "/login", content);
            String result = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(result);
            if(user.id == 0) {
                // ログイン失敗
                DependencyService.Get<IMyFormsToast>().Show("ログイン失敗");
                return;
            } else {
                //Login出来たフラグを立ててメインページに遷移
                App.IsUserLoggedIn = true;
                Application.Current.Properties["user"] = user;
                Navigation.InsertPageBefore(new madaaru_mk2Page(), this);
                await Navigation.PopAsync();
            }
        }
    }
}
