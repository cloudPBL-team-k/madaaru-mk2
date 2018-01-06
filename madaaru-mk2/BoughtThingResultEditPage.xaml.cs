using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class BoughtThingResultEditPage : ContentPage {
        private string jancode = "";
        public BoughtThingResultEditPage(string jancode) {
            InitializeComponent();
            this.jancode = jancode;
        }


        void OkNextPageBtnClicked(object sender, EventArgs s){
            //引数で情報を渡す
            Navigation.PushAsync(new BoughtThingConcernPage(), true);

        }
    }
}
