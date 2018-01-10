using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class ConcernBuyThingPage : ContentPage {
        Bought_thing bt = new Bought_thing();

        public ConcernBuyThingPage(Bought_thing bt) {
            InitializeComponent();
            this.bt = bt;
        }

        //

    }
}
