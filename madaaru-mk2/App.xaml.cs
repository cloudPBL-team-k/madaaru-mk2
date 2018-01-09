using Plugin.Notifications;
using Xamarin.Forms;

namespace madaarumk2 {
    public partial class App : Application {

        public static bool IsInBackgrounded { get; private set; }
        public static bool IsUserLoggedIn { get; set; }

        public App() {
            InitializeComponent();

            DependencyService.Get<IMyFormsToast>().Show("Notification Initialize shart@AppClass");

            //Notification.DefaultTitle = "Test Title";
            //var btnPermission = new Button { Text = "Request Permission" };
            //btnPermission.Command = new Command(async () => {
            //    var result = await CrossNotifications.Current.RequestPermission();
            //    btnPermission.Text = result ? "Permission Granted" : "Permission Denied";
            //});

            //async () => { var result = await CrossNotifications.Current.RequestPermission(); }

            //通知設定をiOSに登録
            DependencyService.Get<INotificationService>().Regist();

            // ここでSign In or Log In 済みか判定
            if(isLogin()) {
                MainPage = new NavigationPage(new madaaru_mk2Page());
            } else { 
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        private bool isLogin() {
            // Applicatino.Current.Prooerties辞書オブジェクトにuserをkeyとするデータが入っていれば一度ログインしている
            // TODO: なぜかアプリを終了するとデータが保存されていない(Emulatorだから？)
            if(Application.Current.Properties.ContainsKey("user")) {
                return true;
            } else {
                return false;
            }
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
            App.IsInBackgrounded = true;
        }

        protected override void OnResume() {
            // Handle when your app resumes
            App.IsInBackgrounded = false;
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
