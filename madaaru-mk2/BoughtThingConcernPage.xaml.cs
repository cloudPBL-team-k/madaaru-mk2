using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class BoughtThingConcernPage : ContentPage {
        public BoughtThingConcernPage() {
            InitializeComponent();
        }


        void OkBtnClicked(object sender, EventArgs s){
            Navigation.PushAsync(new CompleteBoughtThingPage(), true);
        }

        void EditAgainBtnClicked(object sender, EventArgs s){
            //前のページに戻って編集してもらいたいけど、データの受け渡しがわからない
        }
    }
}
