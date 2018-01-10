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

        //void addBtnClicked(object sender, EventArgs s)
        //{
        //    Navigation.PushAsync(new ChoiceShopPage(), true);
        //}

        ////List更新ボタン
        //async void RefreshListBtnClicked(object sender, EventArgs s)
        //{
        //    await setBoughtList();
        //}

        //Listを取得してセットする処理を書く
        async Task setBoughtList(){
            GetObjects go = new GetObjects();
            int userId = 1;
            string jsonString = await go.GetExpendablesInfo(userId);

            if (jsonString != "null"){
                List<Expendables> expendablesInfo = go.GetAllItemsObjectFromJson(jsonString);
                Dictionary<string, string> item = new Dictionary<string, string>();
               
                for (int n = 0; n < expendablesInfo.Count; n++){
                    item[expendablesInfo[n].name]="次回購入予定日："+expendablesInfo[n].limit;
                    //item.Add(expendablesInfo[n].name, expendablesInfo[n].limit);
                    //await DisplayAlert("商品名", expendablesInfo[n].name, "OK");
                    //await DisplayAlert("次回購入予定日", expendablesInfo[n].limit, "OK");
                }

               
                var cell = new DataTemplate(typeof(ImageCell));        
                cell.SetBinding(ImageCell.TextProperty, "Key");       
                cell.SetBinding(ImageCell.DetailProperty, "Value");  
               
                var listView = new ListView{
                    ItemsSource = item,
                    ItemTemplate = cell
                };

                var addbutton = new Button{
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = "+",
                    FontSize = 30,
                    BackgroundColor = Color.Red,
                    TextColor = Color.White,
                    Command = new Command(() => { Navigation.PushAsync(new ChoiceShopPage(), true); })
                };

                var RefreshList = new Button{
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = "更新",
                    FontSize = 20,
                    BackgroundColor = Color.Blue,
                    TextColor = Color.White,
                    Command = new Command(() => { setBoughtList(); })
                };

                Content = new StackLayout{
                    Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0), // iOSのみ上部にマージンをとる
                    Children = {
                        new StackLayout {              
                            Orientation = StackOrientation.Horizontal,
                            Children = {
                                RefreshList,
                                addbutton
                            }
                        },
                        listView
                    }
                };

                //foreach (var p in item){
                //     Debug.WriteLine(string.Format("商品名：{0}, 次回購入予定日：{1}", p.Key, p.Value));
                //}
            }
            else{//json null
                DependencyService.Get<IMyFormsToast>().Show("商品情報はありません!");
            }
            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }
    }
}
