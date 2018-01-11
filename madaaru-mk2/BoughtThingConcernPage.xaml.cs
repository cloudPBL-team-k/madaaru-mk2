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

            User user = (User)Application.Current.Properties["user"];
            int userId = user.id;

            Bought_thing bt = new Bought_thing();
            //書き換える
            bt.user_id = userId;
            bt.thing_id = item.Id;
            bt.num = itemNum;

            PostJson pj = new PostJson();
            //Postして購入済みリストに追加、次の購入日を取得
            Next_buy_date nextBuyDate = await pj.PostBoughtThingInfo(bt);

            //1990-01-01だった場合
            //nextBuyDate.next_buy_dateはstring型なのでDateTimeに変換する
            DateTime defaultDT = new DateTime(1990, 1, 1);
            DateTime inputDateTime = defaultDT;
            DateTime nextBuyDT;
            DateTime nextBuyDTymd;
            string sendDTstring = nextBuyDate.next_buy_date;
            //DateTime inputDate;

            //defaultDTでプロパティを初期化
            Application.Current.Properties["inputDateTime"] = inputDateTime;


            if(DateTime.TryParse(nextBuyDate.next_buy_date, out nextBuyDT)){
                nextBuyDTymd = new DateTime(nextBuyDT.Year, nextBuyDT.Month, nextBuyDT.Day);

                if (nextBuyDTymd.CompareTo(defaultDT) == 0) {//サーバから帰ってきた日付が1990-01-01
                    //入力してもらう
                    await Navigation.PushAsync(new DateTimeInputPage(), true);

                    inputDateTime = (DateTime)Application.Current.Properties["inputDateTime"];
                    DependencyService.Get<IMyFormsToast>().Show("入力した日付: " + inputDateTime.ToString());


                    if (inputDateTime.CompareTo(defaultDT) == 0) {//1990-01-01なので入力してくれてない
                        DependencyService.Get<IMyFormsToast>().Show("もう一度日付入力してください");
                        return;
                    }else{
                        sendDTstring = inputDateTime.ToString("yyyy-MM-dd");
                    }
                }//1990-01-01ではないので通常通り続ける


            //DependencyService.Get<IMyFormsToast>().Show("次の購入日を取得:" + nextBuyDate.next_buy_date);

                //消耗品リスト作成
                Bought_expendable be = new Bought_expendable();
                //書き換える
                be.user_id = userId;
                be.thing_id = item.Id;
                be.limit = sendDTstring;
                DependencyService.Get<IMyFormsToast>().Show("登録する日付: " + inputDateTime.ToString("yyyy-MM-dd"));

                //be.limit = nextBuyDate.next_buy_date;
                //Postして消耗品リストに登録
                Expendables postedEx = await pj.PostExpendablesInfo(be);

                DependencyService.Get<IMyFormsToast>().Show("消耗品リストに登録しました: " + postedEx.created_at);

                //登録完了したので完了したことを伝えるページに遷移
                //shopName(店名)、item.Name(商品名)、bt(bt.numで個数)、nextBuyDate(limit)
                //Page cbtPage = new CompleteBoughtThingPage(shopName, item, bt, nextBuyDate);
                //await Navigation.PushAsync(cbtPage, true);
                await Navigation.PushAsync(new CompleteBoughtThingPage(shopName, item, bt, nextBuyDate), true);


                //店名、個数(bt.num)、postedEx(商品名、次回購入リミット)を渡して表示する
                //await Navigation.PushAsync(new CompleteBoughtThingPage(shopName, bt, postedEx), true);

            }else{//帰ってきた日付が変換できないエラー
                DependencyService.Get<IMyFormsToast>().Show("帰ってきた日付を変換できません:" + nextBuyDate.next_buy_date);
                //Todo:BoughtListPageに戻る処理
                //Navigation.RemovePage();
            }
        }

        void EditAgainBtnClicked(object sender, EventArgs s) {
            //前のページに戻って編集してもらいたいけど、データの受け渡しがわからない
            //とりあえず、前の個数入力ページに戻るだけ
            //数のみの修正になる
            Navigation.PopAsync();
        }
    }
}
