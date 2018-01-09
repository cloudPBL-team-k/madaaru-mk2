using System;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class madaaru_mk2Page : ContentPage {
        public madaaru_mk2Page() {
            //赤線は気にしない(VSの有名なバグっぽい)
            InitializeComponent();
        }


        void BoughtListbtnClicked(object sender, EventArgs s){
            DependencyService.Get<IMyFormsToast>().Show("BoughtListPageへ遷移します");
            Navigation.PushAsync(new BoughtListPage(), true);
        }

        void BuyThingListbtnClicked(object sender, EventArgs s) {
            DependencyService.Get<IMyFormsToast>().Show("BuyThingListPageへ遷移します");
            Navigation.PushAsync(new BuyThingListPage(), true);
        }



        void DevPagebtnClicked(object sender, EventArgs s) {
            DependencyService.Get<IMyFormsToast>().Show("DevPageへ遷移します");
            Navigation.PushAsync(new DevPage(), true);
        }
    }
}
