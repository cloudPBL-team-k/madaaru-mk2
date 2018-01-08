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
                    //chosenShopName,jancodeを渡す
                    await Navigation.PushAsync(new BoughtThingResultEditPage(chosenShopName, scanedJancode), true);

                });
            };

            DependencyService.Get<IMyFormsToast>().Show("ScanedJancode: " + scanedJancode);

            ////選択した店名をchosenShopNameに入れる
            //string chosenShopName = shopANameLabel.Text;
            ////chosenShopName,jancodeを渡す
            //await Navigation.PushAsync(new BoughtThingResultEditPage(chosenShopName, scanedJancode), true);

        }
    }
}
