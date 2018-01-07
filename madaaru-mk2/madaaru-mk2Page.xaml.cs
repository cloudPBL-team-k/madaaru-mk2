using System;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class madaaru_mk2Page : ContentPage {
        public madaaru_mk2Page() {
            //赤線は気にしない(VSの有名なバグっぽい?)
            InitializeComponent();

            //if (!App.IsUserLoggedIn) {
            //Loginページへ
            //Navigation.PushAsync(new LoginPage(), true);
            //} 

            usernametext.Text = App.user.name + "さん、こんにちは";
        }


        void BoughtListbtnClicked(object sender, EventArgs s){
            DependencyService.Get<IMyFormsToast>().Show("BoughtListPageへ遷移します");
            Navigation.PushAsync(new BoughtListPage(), true);
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
    }
}
