using Xamarin.Forms;

namespace madaarumk2 {
    public partial class App : Application {

        public static bool IsUserLoggedIn { get; set; }

        public App() {
            //InitializeComponent();

            //通知設定をiOSに登録
            DependencyService.Get<INotificationService>().Regist();

            if (!IsUserLoggedIn) {
                //Loginページへ
                MainPage = new NavigationPage(new LoginPage());
            } else {
                MainPage = new NavigationPage(new madaaru_mk2Page());
            }


        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }


    //「依存処理」のインターフェース定義
    //インターフェイスの定義はIから始まる習慣があるっぽい
    //インターフェイスの記述は別ファイルに分けるとベターか
    public interface IMyFormsToast {
        void Show(string message);
    }

    //DependencyServiceから利用する
    public interface INotificationService {
        //iOS用の登録
        void Regist();
        //通知する
        void On(string title, string subTitle, string body);
        //通知を解除する
        void Off();
    }
}
