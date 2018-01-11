using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace madaarumk2 {
    public partial class DateTimeInputPage : ContentPage {
        DateTime nowDT = new DateTime();
        public DateTimeInputPage() {
            InitializeComponent();
            nowDT = DateTime.Now;

            //Placeholderにセット
            //yearEntry.Placeholder = nowDT.Year.ToString();
            //monthEntry.Placeholder = nowDT.Month.ToString();
            //dayEntry.Placeholder = nowDT.Day.ToString();
            //Textにセット
            yearEntry.Placeholder = nowDT.Year.ToString();
            monthEntry.Placeholder = nowDT.Month.ToString();
            dayEntry.Placeholder = nowDT.Day.ToString();

        }


        async void OkBtnClicked(object sender, EventArgs s) {
            int year = 2018;
            int month = 1;
            int day = 1;

            if (int.TryParse(yearEntry.Text, out year)) {//数値に変換できた場合yearに入る
                if (int.TryParse(monthEntry.Text, out month)) {//数値に変換できた場合yearに入る
                    if (int.TryParse(dayEntry.Text, out day)) {//数値に変換できた場合yearに入る
                        Application.Current.Properties["inputDateTime"] = new DateTime(year,month,day);

                        //DependencyService.Get<IMyFormsToast>().Show("");
                        //戻る
                        await Navigation.PopAsync();
                    } else {//Inputが数字以外//正しい入力を促す
                        DependencyService.Get<IMyFormsToast>().Show("Number ERROR: 正しい日を入力してください");
                    }
                } else {//Inputが数字以外//正しい入力を促す
                    DependencyService.Get<IMyFormsToast>().Show("Number ERROR: 正しい月を入力してください");
                }
            } else {//Inputが数字以外//正しい入力を促す
                DependencyService.Get<IMyFormsToast>().Show("Number ERROR: 正しい年を入力してください");
            }


        }

        async void CancelBtnClicked(object sender, EventArgs s) {
            await Navigation.PopAsync();
        }
    }
}
