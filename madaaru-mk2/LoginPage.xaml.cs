using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }



        async void LoginBtnClicked(object sender, EventArgs s) {
            try {
                if (nameInput.Text.Length != 0 && idInput.Text.Length != 0 && passInput.Text.Length != 0) {
                    if (nameInput.Text.Length >= 3 && idInput.Text.Length >= 1 && passInput.Text.Length >= 5) {//id>=3,pass>=5
                        int inputUserId = 1;
                        if (int.TryParse(idInput.Text, out inputUserId)) {//idが数値に変換できるか確かめ、出来た場合inputUserIdに入る
                            //Userに情報を入れる
                            User user = new User();
                            user.UserName = nameInput.Text;
                            user.userId = inputUserId;
                            user.Password = passInput.Text;

                            //Login出来たフラグを立ててメインページに遷移
                            App.IsUserLoggedIn = true;
                            Navigation.InsertPageBefore(new madaaru_mk2Page(), this);
                            await Navigation.PopAsync();

                            DependencyService.Get<IMyFormsToast>().Show("実は本当のLogin機能は実装されていません!SeqGaba");
                        } else {//Inputが数字以外
                            DependencyService.Get<IMyFormsToast>().Show("Number ERROR: 数字を入力してください");
                            passInput.Text = string.Empty;
                        }

                    } else {
                        DependencyService.Get<IMyFormsToast>().Show("Login ERROR: nameは３文字以上、idは1文字以上、Passは５文字以上入力してください");
                    }
                } else {//id = null, pass = null
                    DependencyService.Get<IMyFormsToast>().Show("Login ERROR: name,id,passを入力してください");
                }
            } catch (NullReferenceException e) {
                DependencyService.Get<IMyFormsToast>().Show("NULL EXCEPTION ERROR: name,id,passがNullです:" + e.Message);
            }
        }
    }
}
