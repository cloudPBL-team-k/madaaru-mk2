using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class BoughtThingConcernPage : ContentPage {
        SearchedInfo item;
        int itemNum = 0;
        string shopName = "";

        public BoughtThingConcernPage(SearchedInfo thingInfo, int itemNum, string shopName) {
            InitializeComponent();
            shopNameLabel.Text = shopName;
            itemNumLabel.Text = itemNum.ToString();
            itemNameLabel.Text = thingInfo.Name;
            this.item = thingInfo;
            this.itemNum = itemNum;
            this.shopName = shopName;
        }

        async void OkBtnClicked(object sender, EventArgs s) {
            //サーバーに登録情報を送る
            //購入品情報を作成
            //userIdはLoginが実装でき次第書き換える
            //userIdはとりあえず固定
            int userId = 1;

            Bought_thing bt = new Bought_thing();
            //書き換える
            bt.user_id = userId;
            bt.thing_id = item.Id;
            bt.num = itemNum;

            PostJson pj = new PostJson();
            //Postして購入済みリストに追加、次の購入日を取得
            Next_buy_date nextBuyDate = await pj.PostBoughtThingInfo(bt);

            DependencyService.Get<IMyFormsToast>().Show("次の購入日を取得:" + nextBuyDate.next_buy_date);

            //消耗品リスト作成
            Bought_expendable be = new Bought_expendable();
            //書き換える
            be.user_id = userId;
            be.thing_id = item.Id;
            be.limit = nextBuyDate.next_buy_date;
            //Postして消耗品リストに登録
            Expendables postedEx = await pj.PostExpendablesInfo(be);

            DependencyService.Get<IMyFormsToast>().Show("消耗品リストに登録しました: " + postedEx.created_at);

            //登録完了したので完了したことを伝えるページに遷移
            //shopName(店名)、item.Name(商品名)、bt(bt.numで個数)、nextBuyDate(limit)
            await Navigation.PushAsync(new CompleteBoughtThingPage(shopName, item, bt, nextBuyDate), true);


            //店名、個数(bt.num)、postedEx(商品名、次回購入リミット)を渡して表示する
            //await Navigation.PushAsync(new CompleteBoughtThingPage(shopName, bt, postedEx), true);
        }

        void EditAgainBtnClicked(object sender, EventArgs s) {
            //前のページに戻って編集してもらいたいけど、データの受け渡しがわからない
        }
    }
}
