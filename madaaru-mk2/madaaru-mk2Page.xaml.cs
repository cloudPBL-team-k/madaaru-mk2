using System;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class madaaru_mk2Page : ContentPage {
        public madaaru_mk2Page() {
            //赤線は気にしない(VSの有名なバグっぽい)
            InitializeComponent();
        }

        //Notification.DefaultTitle = "Test Title";
        //var btnPermission = new Button { Text = "Request Permission" };
        //btnPermission.Command = new Command(async () => {
            //var result = await CrossNotifications.Current.RequestPermission();
        //    btnPermission.Text = result ? "Permission Granted" : "Permission Denied";
        //});

        void NotifyPermissionbtnClicked(object sender, EventArgs s){
            
        }

        void BoughtListbtnClicked(object sender, EventArgs s){
            Navigation.PushAsync(new BoughtListPage(), true);
        }

        void BuyThingListbtnClicked(object sender, EventArgs s) {
            Navigation.PushAsync(new BuyThingListPage(), true);
        }

        void DevPagebtnClicked(object sender, EventArgs s) {
            Navigation.PushAsync(new DevPage(), true);
        }
    }
}
