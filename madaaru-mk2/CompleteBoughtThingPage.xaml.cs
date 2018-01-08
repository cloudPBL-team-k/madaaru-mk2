using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class CompleteBoughtThingPage : ContentPage {
        //public CompleteBoughtThingPage(string shopName, Bought_thing bt, Expendables expendable) {
        public CompleteBoughtThingPage(string shopName, SearchedInfo item, Bought_thing bt, Next_buy_date nbd) {
            InitializeComponent();
            shopNameLabel.Text = "店名: " + shopName;
            itemNameLabel.Text = "商品名: " + item.Name;
            //itemNameLabel.Text = "商品名: " + expendable.name;
            itemNumLabel.Text = "個数: " + bt.num;
            nextBuyDateLabel.Text = "次の購入予定日: " + nbd.next_buy_date;
            //nextBuyDateLabel.Text = "次の購入予定日: " + expendable.limit;
                   
        }

        void AddOtherOneBtnClicked(object sender, EventArgs s){
            
        }

        void FinishBtnClicked(object sender, EventArgs s){
            
        }
    }
}
