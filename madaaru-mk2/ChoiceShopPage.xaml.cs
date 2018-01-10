using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace madaarumk2 {
    public partial class ChoiceShopPage : ContentPage {
        public ChoiceShopPage() {
            InitializeComponent();
        }


        async void addScanBtnClicked(object sender, EventArgs s) {
            string scanedJancode = "";

            var scanPage = new ZXingScannerPage() {
                DefaultOverlayTopText = "バーコードを読み取ります",
                DefaultOverlayBottomText = "",
            };
            await Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) => {
                scanPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () => {
                    scanedJancode = result.Text;
                    await Navigation.PopAsync();

                    //選択した店名をchosenShopNameに入れる
                    string chosenShopName = shopANameLabel.Text;

                    DependencyService.Get<IMyFormsToast>().Show("Jancode: " + scanedJancode + "で問い合わせ中");

                    //jancodeを元にサーバに商品情報を取得
                    GetObjects go = new GetObjects();
                    string jsonString = await go.GetItemJsonString(scanedJancode);

                    if (scanedJancode != null) {//jsonの内容をチェック
                        //SearchedInfo thingInfo = new SearchedInfo();
                        SearchedInfo thingInfo = go.GetItemObjectFromJson(jsonString);

                        //chosenShopName,SearchedInfoを渡す
                        await Navigation.PushAsync(new BoughtThingResultEditPage(chosenShopName, thingInfo), true);
                    } else {//jancode is null
                        DependencyService.Get<IMyFormsToast>().Show("サーバにデータがありません。商品名を手入力してください");
                        //できればdisplayactionsheetで再スキャンか
                        //入力画面に移行するかを選べるようにする

                        //手入力画面に移行する.手入力ページが未実装なのでコメントアウト
                        //await Navigation.PushAsync(new ManualInputBoughtThingPage(), true);
                    }
                });
            };

            //DependencyService.Get<IMyFormsToast>().Show("ScanedJancode: " + scanedJancode);

            ////選択した店名をchosenShopNameに入れる
            //string chosenShopName = shopANameLabel.Text;
            ////chosenShopName,jancodeを渡す
            //await Navigation.PushAsync(new BoughtThingResultEditPage(chosenShopName, scanedJancode), true);

        }
    }
}
