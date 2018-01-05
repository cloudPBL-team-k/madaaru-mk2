using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(madaarumk2.Droid.MyFormsToast))]
namespace madaarumk2.Droid {
    class MyFormsToast : IMyFormsToast {
        public void Show(string message) {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}