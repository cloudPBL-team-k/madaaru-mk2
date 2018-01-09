using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Notifications;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class CompleteBoughtThingPage : ContentPage {
        public CompleteBoughtThingPage(string shopName, SearchedInfo item, Bought_thing bt, Next_buy_date nbd) {
            InitializeComponent();
            shopNameLabel.Text = "店名: " + shopName;
            itemNameLabel.Text = "商品名: " + item.Name;
            //itemNameLabel.Text = "商品名: " + expendable.name;
            itemNumLabel.Text = "個数: " + bt.num;
            nextBuyDateLabel.Text = "次の購入予定日: " + nbd.next_buy_date;
            //nextBuyDateLabel.Text = "次の購入予定日: " + expendable.limit;

            //ここに一定タイマーを仕込む処理を書く
            //CreateNotify();

        }

        async Task CreateNotify(){
            //await CrossNotifications.Current.Send("My Title", "My message for the notification");

            await CrossNotifications.Current.Send(new Notification{Title = "Title desu", Message = "I sent this a long time ago", When = TimeSpan.FromSeconds(5)});

            //await CrossNotifications.Current.Send("Happy Birthday", "I sent this a long time ago", when = TimeSpan.FromDays(50));
        }

        void AddOtherOneBtnClicked(object sender, EventArgs s){
            
        }

        void FinishBtnClicked(object sender, EventArgs s){
            
        }
    }
}
