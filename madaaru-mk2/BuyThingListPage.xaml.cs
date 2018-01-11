using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Threading.Tasks;

namespace madaarumk2 {
    public partial class BuyThingListPage : ContentPage {
        public BuyThingListPage() {
            InitializeComponent();
            setBuyList();
        }

        //Listを取得してセットする処理を書く
        async Task setBuyList(){
            User user = (User)Application.Current.Properties["user"];
            int userId = user.id;
            GetObjects go = new GetObjects();
            string jsonString = await go.GetBuythingInfo(userId);
            
        async void addBtnClicked(object sender, EventArgs s) {

            DependencyService.Get<IMyFormsToast>().Show("NavigationStuck :" + Navigation.NavigationStack.Count);

            string nextPageResult = await DisplayActionSheet(
                    "追加方法を選ぶ", "閉じる", "キャンセル",
                    new string[] { "リストから", "スキャンする" });

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
                Command = new Command(async () => {
                    string nextPageResult = await DisplayActionSheet("追加方法を選ぶ", "閉じる", "キャンセル", new string[] { "リストから", "スキャンする" });

                    if (nextPageResult == "リストから"){//リストから選ぶページを表示
                        await Navigation.PushAsync(new ChoiceFromBoughtThingPage(), true);
                    }else if (nextPageResult == "スキャンする"){//スキャンするページを表示
                         //await Navigation.PushAsync(new ScanBuyThingPage(), true);
                         //ZXingのスキャナーを呼ぶ
                        string scanedJancode = "";
                        var scanPage = new ZXingScannerPage(){
                            DefaultOverlayTopText = "バーコードを読み取ります",
                            DefaultOverlayBottomText = "",
                        };
                        await Navigation.PushAsync(scanPage);
                        scanPage.OnScanResult += (result) => {
                            //scanPage.OnScanResult += (result) => {
                            scanPage.IsScanning = false;
                            Device.BeginInvokeOnMainThread(async () => {
                                scanedJancode = result.Text;
                                await Navigation.PopAsync();

                                DependencyService.Get<IMyFormsToast>().Show("Jancode: " + scanedJancode + "で問い合わせ中");
                                //GetObjects go = new GetObjects();
                                string json = await go.GetItemJsonString(scanedJancode);

                                if (scanedJancode != null){//jsonの内容をチェック
                                     //jancodeを元に情報を作成
                                    SearchedInfo thingInfo = go.GetItemObjectFromJson(json);
                                    Bought_thing bt = new Bought_thing();
                                    User userInfo = (User)Application.Current.Properties["user"];
                                    bt.user_id = userInfo.id;
                                    //数は最後に入れてもらうのでとりあえず1を入れる
                                    bt.num = 1;
                                    bt.thing_id = thingInfo.Id;

                                    //登録に必要な情報を渡す
                                    await Navigation.PushAsync(new ConcernBuyThingPage(bt), true);
                                }else{//jancode is null
                                    DependencyService.Get<IMyFormsToast>().Show("サーバにデータがありません");
                                    //手入力画面に移行する.手入力ページが未実装なのでコメントアウト
                                    //await Navigation.PushAsync(new ManualInputBuyThingPage(), true);
                                }
                              });
                            };

                        }else{//閉じるかキャンセルが押されているので何もしない(留まる)
                            DependencyService.Get<IMyFormsToast>().Show("追加をキャンセルします.");
                        }
                    })
                };

            var RefreshList = new Button{
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "更新",
                FontSize = 20,
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                Command = new Command(() => { setBuyList(); })
            };

            for (int n = 0; n < buythingInfo.Count; n++){
                item[buythingInfo[n].name] = buythingInfo[n].alias;
                //item.Add(expendablesInfo[n].name, expendablesInfo[n].limit);
                //await DisplayAlert("商品名", expendablesInfo[n].name, "OK");
                //await DisplayAlert("次回購入予定日", expendablesInfo[n].limit, "OK");
            }

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
            DependencyService.Get<IMyFormsToast>().Show("リストを更新しました！");
        }
    }
}
