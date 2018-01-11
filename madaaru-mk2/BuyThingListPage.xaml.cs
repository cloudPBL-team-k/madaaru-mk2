using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace madaarumk2 {
    public partial class BuyThingListPage : ContentPage {
        public BuyThingListPage() {
            InitializeComponent();
            setBuyList();
        }


        async void addBtnClicked(object sender, EventArgs s) {


            string nextPageResult = await DisplayActionSheet(
                    "追加方法を選ぶ", "閉じる", "キャンセル",
                    new string[] { "リストから", "スキャンする" });

            //自分で入力を追加
            //string result = await DisplayActionSheet(
            //"ダイアログのタイトル", "閉じる", "キャンセル",
            //new string[] { "リストから", "スキャンする", "自分で入力" });

            if (nextPageResult == "リストから") {//リストから選ぶページを表示
                await Navigation.PushAsync(new ChoiceFromBoughtThingPage(), true);

            } else if (nextPageResult == "スキャンする") {//スキャンするページを表示
                                                    //await Navigation.PushAsync(new ScanBuyThingPage(), true);
                                                    //ZXingのスキャナーを呼ぶ
                string scanedJancode = "";
                var scanPage = new ZXingScannerPage() {
                    DefaultOverlayTopText = "バーコードを読み取ります",
                    DefaultOverlayBottomText = "",
                };
                await Navigation.PushAsync(scanPage);
                scanPage.OnScanResult += (result) => {
                    //scanPage.OnScanResult += (result) => {
                    scanPage.IsScanning = false;
                    Device.BeginInvokeOnMainThread(async () => {
                        scanedJancode = result.Text;
                        await Navigation.PopAsync();

                        DependencyService.Get<IMyFormsToast>().Show("Jancode: " + scanedJancode + "で問い合わせ中");
                        //jancodeを元にサーバに商品情報を取得
                        GetObjects go = new GetObjects();
                        string jsonString = await go.GetItemJsonString(scanedJancode);

                        if (scanedJancode != null) {//jsonの内容をチェック
                                                    //jancodeを元に情報を作成
                            SearchedInfo thingInfo = go.GetItemObjectFromJson(jsonString);
                            Bought_thing bt = new Bought_thing();
                            User userInfo = (User)Application.Current.Properties["user"];
                            bt.user_id = userInfo.id;
                            //数は最後に入れてもらうのでとりあえず1を入れる
                            bt.num = 1;
                            bt.thing_id = thingInfo.Id;

                            //登録に必要な情報を渡す
                            await Navigation.PushAsync(new ConcernBuyThingPage(bt), true);
                        } else {//jancode is null
                            DependencyService.Get<IMyFormsToast>().Show("サーバにデータがありません");
                            //手入力画面に移行する.手入力ページが未実装なのでコメントアウト
                            //await Navigation.PushAsync(new ManualInputBuyThingPage(), true);
                        }
                    });
                };

            } else {//閉じるかキャンセルが押されているので何もしない(留まる)
                DependencyService.Get<IMyFormsToast>().Show("追加をキャンセルします.");
            }

            //switch文
            //switch (nextPageResult) {
            //    case "リストから"://リストから選ぶページを表示
            //        await Navigation.PushAsync(new ChoiceFromBoughtThingPage(), true);
            //        break;
            //    case "スキャンする"://スキャンするページを表示
            //        //await Navigation.PushAsync(new ScanBuyThingPage(), true);
            //        //ZXingのスキャナーを呼ぶ
            //        string scanedJancode = "";
            //        var scanPage = new ZXingScannerPage() {
            //            DefaultOverlayTopText = "バーコードを読み取ります",
            //            DefaultOverlayBottomText = "",
            //        };
            //        await Navigation.PushAsync(scanPage);
            //        scanPage.OnScanResult += (result) => {
            //            //scanPage.OnScanResult += (result) => {
            //            scanPage.IsScanning = false;
            //            Device.BeginInvokeOnMainThread(async () => {
            //                scanedJancode = result.Text;
            //                await Navigation.PopAsync();
            //                DependencyService.Get<IMyFormsToast>().Show("Jancode: " + scanedJancode + "で問い合わせ中");
            //                //jancodeを元にサーバに商品情報を取得
            //                GetObjects go = new GetObjects();
            //                string jsonString = await go.GetItemJsonString(scanedJancode);
            //                if (scanedJancode != null) {//jsonの内容をチェック
            //                    //jancodeを元に情報を作成
            //                    SearchedInfo thingInfo = go.GetItemObjectFromJson(jsonString);
            //                    Bought_thing bt = new Bought_thing();
            //                    User userInfo = (User)Application.Current.Properties["user"];
            //                    bt.user_id = userInfo.id;
            //                    //数は最後に入れてもらうのでとりあえず1を入れる
            //                    bt.num = 1;
            //                    bt.thing_id = thingInfo.Id;
            //                    //登録に必要な情報を渡す
            //                    await Navigation.PushAsync(new ConcernBuyThingPage(bt), true);
            //                } else {//jancode is null
            //                    DependencyService.Get<IMyFormsToast>().Show("サーバにデータがありません");
            //                    //手入力画面に移行する.手入力ページが未実装なのでコメントアウト
            //                    //await Navigation.PushAsync(new ManualInputBuyThingPage(), true);
            //                }
            //            });
            //        };
            //        break;
            //    default:
            //        //閉じるかキャンセルが押されているので何もしない(留まる)
            //        DependencyService.Get<IMyFormsToast>().Show("追加をキャンセルします.");
            //        break;
            //}

        }

        //List更新ボタン
        void RefreshListBtnClicked(object sender, EventArgs s) {
            setBuyList();
        }

        //Listを取得してセットする処理を書く
        void setBuyList() {
            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }
    }
}
