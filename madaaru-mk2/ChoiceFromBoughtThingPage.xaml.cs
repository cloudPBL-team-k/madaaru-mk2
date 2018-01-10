using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class ChoiceFromBoughtThingPage : ContentPage {
        public ChoiceFromBoughtThingPage() {
            InitializeComponent();

            setList();
        }


        void RegistBtnClicked(object sender, EventArgs s) {
            Bought_thing bt = new Bought_thing();
            User userInfo = (User)Application.Current.Properties["user"];
            bt.user_id = userInfo.id;
            //リストから選択してもらった物の数とthing_idを取得
            bt.num = 1;
            //Todo:リストからその商品のidを取得してくる
            bt.thing_id = 1;

            //登録に必要な情報を渡す
            Navigation.PushAsync(new ConcernBuyThingPage(bt), true);
        }

        //List更新ボタン
        async void RefreshListBtnClicked(object sender, EventArgs s) {
            await setList();
        }


        //Todo:Listを取得してセットする処理を書く
        async Task setList() {
            //リストに表示する情報を取得してくる
            User userInfo = (User)Application.Current.Properties["user"];
            int user_id = userInfo.id;
            GetObjects go = new GetObjects();
            List<G_Buy_Thing> BuyThingList = await go.GetBuyThingObjects(user_id);


            //リストにセット

            //Dictionary<string, string> item = new Dictionary<string, string>();

            //for (int n = 0; n < expendablesInfo.Count; n++) {
            //    item.Add(expendablesInfo[n].name, expendablesInfo[n].limit);
            //    //await DisplayAlert("商品名", expendablesInfo[n].name, "OK");
            //    //await DisplayAlert("次回購入予定日", expendablesInfo[n].limit, "OK");
            //}

            //foreach (var p in item) {
            //    Debug.WriteLine(string.Format("商品名：{0}, 次回購入予定日：{1}", p.Key, p.Value));
            //}

            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }



    }
}
