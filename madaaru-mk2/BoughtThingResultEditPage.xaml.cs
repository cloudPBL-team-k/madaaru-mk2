using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class BoughtThingResultEditPage : ContentPage {
        private string shopName = "";
        SearchedInfo thingInfo = new SearchedInfo();

        public BoughtThingResultEditPage(string shopName, SearchedInfo thingInfo) {
            InitializeComponent();
            //this.shopName = " 選択された店名:" + shopName; 
            if (thingInfo.Name != null) {
                itemNameLabel.Text = thingInfo.Name;
            }
            //shopNameLabel.Text = shopName;
            //this.shopName = shopName;
            this.thingInfo = thingInfo;
        }

        void OkNextPageBtnClicked(object sender, EventArgs s){
            //入力した個数を取得
            //thingsNumは個数
            int itemNum = 1;

            if (int.TryParse(numInput.Text, out itemNum)) {//数値に変換できた場合itemNumに入る
                //引数で情報を渡す
                Navigation.PushAsync(new BoughtThingConcernPage(thingInfo, itemNum, shopName), true);
            }else{//Inputが数字以外
                //正しい入力を促す
                DependencyService.Get<IMyFormsToast>().Show("Number ERROR: 数字を入力してください");
            }
        }


        void CancelBtnClicked(object sender, EventArgs s){
            Navigation.PopAsync();    
        }
    }
}
