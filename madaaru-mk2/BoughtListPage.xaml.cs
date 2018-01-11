using System;
using System.Diagnostics;
using System.Collections.Generic;


using Xamarin.Forms;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;

namespace madaarumk2 {
    public partial class BoughtListPage : ContentPage {
        public BoughtListPage() {
            InitializeComponent();
            //List表示する処理
            setBoughtList();
        }

        //Listを取得してセットする処理を書く
        async Task setBoughtList(){
            User user = (User)Application.Current.Properties["user"];
            int userId = user.id;

            GetObjects go = new GetObjects();
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
                    BackgroundColor = Color.Orange,
                    TextColor = Color.White,
                    Command = new Command(async () => { 
                        string scanedJancode = "";

                        var scanPage = new ZXingScannerPage() {
                            DefaultOverlayTopText = "バーコードを読み取ります",
                            DefaultOverlayBottomText = "",
                        };
                        await Navigation.PushAsync(scanPage);
                        scanPage.OnScanResult += (result) => {
                            scanPage.IsScanning = false;
                            Device.BeginInvokeOnMainThread(async () => {
                                scanedJancode = result.Text;
                                await Navigation.PopAsync();

                                //選択した店名をchosenShopNameに入れる
                                string chosenShopName = "ShopA";

                                //DependencyService.Get<IMyFormsToast>().Show("Jancode: " + scanedJancode + "で問い合わせ中");

                                //jancodeを元にサーバに商品情報を取得
                                //GetObjects go = new GetObjects();
                                string jsonString2 = await go.GetItemJsonString(scanedJancode);

                                DependencyService.Get<IMyFormsToast>().Show("Json: " + jsonString2);

                                if (jsonString2 != null) {//jsonの内容をチェック
                                                            //SearchedInfo thingInfo = new SearchedInfo();
                                    SearchedInfo thingInfo = go.GetItemObjectFromJson(jsonString2);

                                    //chosenShopName,SearchedInfoを渡す
                                    await Navigation.PushAsync(new BoughtThingResultEditPage(chosenShopName, thingInfo), true);
                                } else {//jancode is null
                                    DependencyService.Get<IMyFormsToast>().Show("サーバにデータがありません");
                                    //DependencyService.Get<IMyFormsToast>().Show("サーバにデータがありません。商品名を手入力してください");
                                    //できればdisplayactionsheetで再スキャンか
                                    //入力画面に移行するかを選べるようにする

                                    //手入力画面に移行する.手入力ページが未実装なのでコメントアウト
                                    //await Navigation.PushAsync(new ManualInputBoughtThingPage(), true);
                                }
                            });
                        };
                    })
                };

                var RefreshList = new Button{
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = "更新",
                    FontSize = 20,
                    BackgroundColor = Color.Tan,
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
