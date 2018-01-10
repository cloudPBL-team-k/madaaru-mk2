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


        void RegistBtnClickedBtnClicked(object sender, EventArgs s) {
            Bought_thing bt = new Bought_thing();
            User userInfo = (User)Application.Current.Properties["user"];
            bt.user_id = userInfo.id;
            //リストから選択してもらった物の数とthing_idを取得
            bt.num = 1;
            bt.thing_id = 1;

            //登録に必要な情報を渡す
            Navigation.PushAsync(new ConcernBuyThingPage(bt), true);
        }

        //List更新ボタン
        async void RefreshListBtnClicked(object sender, EventArgs s) {
            await setList();
        }

        //Listを取得してセットする処理を書く
        async Task setList() {
            //リストに表示する情報を取得してくる
            User userInfo = (User)Application.Current.Properties["user"];
            int user_id = userInfo.id;
            GetObjects go = new GetObjects();
            List<G_Buy_Thing> BuyThingList = await go.GetBuyThingObjects(user_id);

            //リストにセット

            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }



    }
}
