using System;
using System.Diagnostics;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

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
        async void RefreshListBtnClicked(object sender, EventArgs s){
            await setBoughtList();
        }

        //Listを取得してセットする処理を書く
        async Task setBoughtList(){
            GetObjects go = new GetObjects();
            int userId = 1;
            string jsonString = await go.GetExpendablesInfo(userId);

            if (jsonString != "null"){
                List<Expendables> expendablesInfo = go.GetAllItemsObjectFromJson(jsonString);
                Dictionary<string, string> item = new Dictionary<string, string>();

                for (int n = 0; n < expendablesInfo.Count; n++){
                    item.Add(expendablesInfo[n].name, expendablesInfo[n].limit);
                    //await DisplayAlert("商品名", expendablesInfo[n].name, "OK");
                    //await DisplayAlert("次回購入予定日", expendablesInfo[n].limit, "OK");
                }

                foreach (var p in item){
                       Debug.WriteLine(string.Format("商品名：{0}, 次回購入予定日：{1}", p.Key, p.Value));
                }
            }
            else{//json null
                DependencyService.Get<IMyFormsToast>().Show("商品情報はありません!");
            }
            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }
    }
}
