using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class ChoiceFromBoughtThingPage : ContentPage {
        //public List<string> ItemNameList { get; set; } = Enumerable.Range(1, 5)
        //.Select((x, index) => $"{index}番目")
        //.ToList();
        public List<string> ItemNameList = new List<string>();
        List<G_Buy_Thing> BuyThingList = new List<G_Buy_Thing>();
        int chosenNum = -1;

        public ChoiceFromBoughtThingPage() {
            InitializeComponent();
            //itemListView.ItemsSource = ItemNameList;
            setList();
        }


        void RegistBtnClicked(object sender, EventArgs s) {
            Bought_thing bt = new Bought_thing();
            User userInfo = (User)Application.Current.Properties["user"];
            bt.user_id = userInfo.id;
            //リストから選択してもらった物の数とthing_idを取得
            bt.num = 1;

            if (chosenNum != -1) {
                // Todo:コメントアウトを変える
                bt.thing_id = BuyThingList[chosenNum].thing_id;
                //bt.thing_id = chosenNum;

                //登録に必要な情報を渡す
                Navigation.PushAsync(new ConcernBuyThingPage(bt), true);
            } else {//chosenNum == -1 なので選択されていない
                DependencyService.Get<IMyFormsToast>().Show("商品を選択してください");
            }
        }

        //List更新ボタン
        async void RefreshListBtnClicked(object sender, EventArgs s) {
            await setList();
            this.itemListView.IsRefreshing = false;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs s) {
            ResistBtn.Text = s.SelectedItem.ToString() + "を登録する";
            var index = (itemListView.ItemsSource as List<string>).IndexOf(s.SelectedItem as string);
            chosenNum = index;
        }


        //リストに表示する情報を取得してくる
        async Task setList() {
            User userInfo = (User)Application.Current.Properties["user"];
            int user_id = userInfo.id;

            GetObjects go = new GetObjects();
            //List<G_Buy_Thing> BuyThingList = await go.GetBuyThingObjects(user_id);
            BuyThingList = await go.GetBuyThingObjects(user_id);
            for (int i = 0; i < BuyThingList.Count; i++) {
                ItemNameList.Add(BuyThingList[i].name);
            }

            //Todo:削除する
            //this.ItemNameList.Add("0");//[0]
            //this.ItemNameList.Add("1");//[1]
            //this.ItemNameList.Add("2");//[2]
            //this.ItemNameList.Add("3");
            //this.ItemNameList.Add("4");
            //this.ItemNameList.Add("5");

            itemListView.ItemsSource = ItemNameList;
        }



    }
}
