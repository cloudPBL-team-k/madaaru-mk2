using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Notifications;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace madaarumk2 {
    public partial class DevPage : ContentPage {
        public DevPage() {
            InitializeComponent();
        }


        private string scanedcode = "0";

        async void ScanButtonClicked(object sender, EventArgs s) {
            var scanPage = new ZXingScannerPage() {
                DefaultOverlayTopText = "バーコードを読み取ります",
                DefaultOverlayBottomText = "",
            };
            await Navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) => {
                scanPage.IsScanning = false;
                string jancode = result.Text;
                Device.BeginInvokeOnMainThread(async () => {
                    scanedcode = result.Text;
                    await Navigation.PopAsync();
                    await DisplayAlert("Scan Done!", result.Text, "OK");
                });
            };
        }

        //for debug
        async void ShowJancodeButtonClicked(object sender, EventArgs s) {
            GetObjects gj = new GetObjects();
            if (scanedcode != "0") {
                string jsonString = await gj.GetItemJsonString(scanedcode);
                DependencyService.Get<IMyFormsToast>().Show(jsonString);
            } else {//Not Scaned
                DependencyService.Get<IMyFormsToast>().Show("Jancodeがスキャンされていません");
            }
        }

        async void ShowItemNameButtonClicked(object sender, EventArgs s) {
            GetObjects gj = new GetObjects();

            string jsonString = await gj.GetItemJsonString(scanedcode);
            if (jsonString != "null") {
                SearchedInfo thingInfo = await gj.GetItemInfo(scanedcode);
                DependencyService.Get<IMyFormsToast>().Show("商品名!!" + thingInfo.Name);
            } else {//json null
                DependencyService.Get<IMyFormsToast>().Show("該当の商品情報がありません!");
            }
        }


        void ToastDevBtnClicked(object sender, EventArgs s) {
            DependencyService.Get<IMyFormsToast>().Show("Toast Dev Page Test!");
        }


        //async void ScanButtonClicked(object sender, EventArgs s) {
        //    var scanPage = new ZXingScannerPage() {
        //        DefaultOverlayTopText = "バーコードを読み取ります",
        //        DefaultOverlayBottomText = "",
        //    };
        //    await Navigation.PushAsync(scanPage);
        //    scanPage.OnScanResult += (result) => {
        //        scanPage.IsScanning = false;
        //        Device.BeginInvokeOnMainThread(async () => {
        //            scanedcode = result.Text;
        //            await Navigation.PopAsync();
        //            //await DisplayAlert("Scan Done!", result.Text, "OK");//これは消して良い
        //            //スキャンして得た情報を使って何かしたい場合
        //            //ここに処理を書く
        //        });
        //    };
        //}

        async void LightScanBtnClicked(object sender, EventArgs s) {
            var scanPage = new ZXingScannerPage() {
                DefaultOverlayTopText = "バーコードを読み取ります",
                DefaultOverlayBottomText = "",
            };
            await Navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) => {
                scanedcode = result.Text;
                scanPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () => {
                    await Navigation.PopAsync();

                    DependencyService.Get<IMyFormsToast>().Show("LightWeghtTest:" + scanedcode);
                    GetObjects gj = new GetObjects();
                    string jsonString = await gj.GetItemJsonString(scanedcode);
                    if (jsonString != "null") {
                        SearchedInfo thingInfo = gj.GetItemObjectFromJson(jsonString);
                        //userIdはとりあえず1の人固定
                        int userId = 1;
                        //int itemId = thingInfo[0].Id;
                        int itemId = thingInfo.Id;
                        //個数はとりあえず1個固定
                        int itemNum = 1;

                        //購入品情報を作成
                        Bought_thing bt = new Bought_thing();
                        bt.user_id = userId;
                        bt.thing_id = itemId;
                        bt.num = itemNum;

                        PostJson pj = new PostJson();
                        //Postして購入済みリストに追加、次の購入日を取得
                        Next_buy_date nextBuyDate = await pj.PostBoughtThingInfo(bt);

                        DependencyService.Get<IMyFormsToast>().Show("次の購入日を取得:" + nextBuyDate.next_buy_date);

                        //消耗品リスト作成
                        Bought_expendable be = new Bought_expendable();
                        be.user_id = userId;
                        be.thing_id = thingInfo.Id;
                        be.limit = nextBuyDate.next_buy_date;
                        //Postして消耗品リストに登録
                        Expendables postedEx = await pj.PostExpendablesInfo(be);

                        DependencyService.Get<IMyFormsToast>().Show("消耗品リストに登録しました: " + postedEx.created_at);
                    } else {//json null
                        DependencyService.Get<IMyFormsToast>().Show("該当の商品情報がありません!");
                    }
                });
            };
        }


        void NotificationBtnClicked(object sender, EventArgs e) {
            //通知を実行
            DependencyService.Get<INotificationService>().On("タイトル", "SubTitleです", "本文");
        }

        void LoginBtnClicked(object sender, EventArgs s) {
            Navigation.PushAsync(new LoginPage(), true);
        }


        async void ScheduledNotificationBtnClicked(object sender, EventArgs e) {
            //通知を実行
            var i = 1;
            var seconds = i * 5;

            await CrossNotifications.Current.Send(new Notification { Title = "Title desu", Message = "I sent this a long time ago", When = TimeSpan.FromSeconds(seconds) });

            //DependencyService.Get<INotificationService>().On("タイトル", "SubTitleです", "本文");
        }

        async void NotScheduledNotificationBtnClicked(object sender, EventArgs e) {

            await CrossNotifications.Current.Send(new Notification { Title = "Title desu", Message = "I sent this a long time ago"});

            //DependencyService.Get<INotificationService>().On("タイトル", "SubTitleです", "本文");
        }

        void NotAsyncNotificationBtnClicked(object sender, EventArgs e) {

            CrossNotifications.Current.Send(new Notification { Title = "Title desu", Message = "I sent this a long time ago" });

            //DependencyService.Get<INotificationService>().On("タイトル", "SubTitleです", "本文");
        }

        void BackMainPageBtnClicked(object sender, EventArgs s) {
            Navigation.PopAsync(true);
        }
    }
}
