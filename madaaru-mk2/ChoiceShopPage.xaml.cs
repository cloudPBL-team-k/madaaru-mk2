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
                    //スキャンして得た情報を使って何かしたい場合
                    //ここに処理を書く
                });
            };

            DependencyService.Get<IMyFormsToast>().Show("ScanedJancode: " + scanedJancode);

            await Navigation.PushAsync(new BoughtThingResultEditPage(scanedJancode), true);

        }
    }
}
