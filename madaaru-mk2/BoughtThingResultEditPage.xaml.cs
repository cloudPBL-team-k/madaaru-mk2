using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class BoughtThingResultEditPage : ContentPage {
        private string jancode = "";
        private string shopName = "";
        SearchedInfo thingInfo = new SearchedInfo();

        public BoughtThingResultEditPage(string shopName,string jancode) {
            InitializeComponent();
            this.shopName = " 選択された店名:" + shopName; 
            this.jancode = jancode;
            shopNameLabel.Text = shopName;
            //コンストラクタでawaitが必要な非同期メソッドを呼び出しているので
            //この処理は前のページで行うべきかも
            SetInfo();

        }

        private async Task SetInfo(){
            //jancodeをサーバに送って情報を取得
            GetObjects go = new GetObjects();
            string jsonString = await go.GetItemJsonString(jancode);

            if(jancode != null){
                thingInfo = go.GetItemObjectFromJson(jsonString);
                //取得した情報をUIにセット
                itemNameLabel.Text = thingInfo.Name;
            }else{//jancode is null
                DependencyService.Get<IMyFormsToast>().Show("サーバにデータがありません。商品名を手入力してください");
            //手入力画面に移行する
            }

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
    }
}
