using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class ChoiceFromBoughtThingPage : ContentPage {
        public ChoiceFromBoughtThingPage() {
            InitializeComponent();

            setList();
        }


        void RegistBtnClickedBtnClicked(object sender, EventArgs s) {
            //url
            //bought_things/exists_bought? user_id = 1


            //登録に必要な情報を渡す
            Navigation.PushAsync(new ConcernBuyThingPage(), true);
        }

        //List更新ボタン
        void RefreshListBtnClicked(object sender, EventArgs s) {
            setList();
        }

        //Listを取得してセットする処理を書く
        void setList() {
            //リストに表示する情報を取得してくる
            GetObjects go = new GetObjects();


            //リストにセット

            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }



    }
}
