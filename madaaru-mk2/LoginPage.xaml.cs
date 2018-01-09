using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using PCLCrypto;

namespace madaarumk2 {
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }

        async void LoginBtnClicked(object sender, EventArgs s) {
            LoginRequest loginReq = new LoginRequest();
            try {
                loginReq.name = nameInput.Text;
                string before_hash = nameInput.Text + ":::" + passInput.Text;
                byte[] data = System.Text.Encoding.UTF8.GetBytes(before_hash);
                var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha512);
                byte[] hash = hasher.HashData(data);
                loginReq.hashed = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            } catch (NullReferenceException e) {
                DependencyService.Get<IMyFormsToast>().Show("NULL EXCEPTION ERROR: name,passがNullです:" + e.Message);
                return;
            }

            // サーバにログイン
            WrappedHttpClient wHttpClient = new WrappedHttpClient();
            String baseURL = ServerInfo.url;
            String jsonString = JsonConvert.SerializeObject(loginReq);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await wHttpClient.PostAsync(baseURL + "/login", content);
            String result = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(result);
            if (user.id == 0) {
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

        async void SignupBtnClicked(object sender, EventArgs s) {
            Navigation.InsertPageBefore(new SignUpPage(), this);
            await Navigation.PopAsync();
        }
    }
}
