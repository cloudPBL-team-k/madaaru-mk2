using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class BoughtListPage : ContentPage {
        public BoughtListPage() {
            InitializeComponent();
            //List表示する処理
            setBoughtList();
        }


        void addBtnClicked(object sender, EventArgs s){
            Navigation.PushAsync(new ChoiceShopPage(), true);
        }

        //List更新ボタン
        void RefreshListBtnClicked(object sender, EventArgs s){
            setBoughtList();
        }

        //Listを取得してセットする処理を書く
        void setBoughtList(){
            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }
    }
}
