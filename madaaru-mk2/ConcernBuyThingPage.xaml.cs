using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class ConcernBuyThingPage : ContentPage {
        Bought_thing bt = new Bought_thing();

        public ConcernBuyThingPage(Bought_thing bt) {
            InitializeComponent();
            this.bt = bt;
        }

        //個数はここで決めてもらう
        async void OkDoneBtnClicked(object sender, EventArgs s) {

            //入力した個数を取得
            //thingsNumは個数
            int itemNum = 1;
            if (int.TryParse(numInput.Text, out itemNum)) {//数値に変換できた場合itemNumに入る
                //数を取得
                bt.num = itemNum;
                //サーバにPost
                PostJson pj = new PostJson();
                P_Res_Buy_Thing prbt = await pj.PostBuyThingInfo(bt);

                //Todo:なにかメッセージ表示する
                await DisplayAlert("登録完了", prbt.updated_at.ToString(), "OK");
                //ページを離れる

            if (Navigation.NavigationStack.Count == 4) {
                    if (Device.RuntimePlatform == Device.Android) {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    } else {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    }
                }
                await Navigation.PopAsync();

            } else {//Inputが数字以外
                //正しい入力を促す
                DependencyService.Get<IMyFormsToast>().Show("Number ERROR: 数字を入力してください");
            }
        }

        async void CancelBtnClicked(object sender, EventArgs s) {
            DependencyService.Get<IMyFormsToast>().Show("NavigationStuck :" + Navigation.NavigationStack.Count);

            if(Navigation.NavigationStack.Count == 4){
                if (Device.RuntimePlatform == Device.Android) {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                } else {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            await Navigation.PopAsync();
            //BuyThingListPageまでもどる
        }
    }


}
