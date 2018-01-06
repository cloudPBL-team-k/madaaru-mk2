using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class BoughtListPage : ContentPage {
        public BoughtListPage() {
            InitializeComponent();
        }


        void addBtnClicked(object sender, EventArgs s){
            Navigation.PushAsync(new ChoiceShopPage(), true);
        }
    }
}
