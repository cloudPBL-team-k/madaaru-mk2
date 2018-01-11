using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Notifications;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class CompleteBoughtThingPage : ContentPage {
        public CompleteBoughtThingPage(string shopName, SearchedInfo item, Bought_thing bt, Next_buy_date nbd) {
            InitializeComponent();
            //shopNameLabel.Text = "店名: " + shopName;
            itemNameLabel.Text = "商品名: " + item.Name;
            itemNumLabel.Text = "個数: " + bt.num;
            nextBuyDateLabel.Text = "次の購入予定日: " + nbd.next_buy_date;

            //ここに一定タイマーを仕込む処理を書く
            //CreateNotify();

        }

        //async Task CreateNotify(){
        //}

        async void AddOtherOneBtnClicked(object sender, EventArgs s) {
            if (Device.RuntimePlatform == Device.Android) {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            } else {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 3]);
                //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 3]);
            }

            await Navigation.PopAsync();
        }

        async void FinishBtnClicked(object sender, EventArgs s) {
            if (Device.RuntimePlatform == Device.Android) {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 3]);
            } else {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 3]);
                //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 4]);
            }
            await Navigation.PopAsync();
        }
    }
}
