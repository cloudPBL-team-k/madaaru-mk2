using Xamarin.Forms;

namespace madaarumk2 {
    public partial class App : Application {
        public App() {
            //InitializeComponent();

            //MainPage = new madaaru_mk2Page();
            MainPage = new NavigationPage(new madaaru_mk2Page());
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
}
