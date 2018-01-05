using Android.App;
using Android.Content;
using Android.Media;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(madaarumk2.Droid.NotificationService))]
namespace madaarumk2.Droid {
    public class NotificationService : INotificationService {
        // 通知のID
        int id = 0;

        public void Regist() {
            //iOS用なので、何もしない
        }

        public void On(string title, string subTitle, string body) {
            Context context = Forms.Context;
            Intent intent = new Intent(context, typeof(MainActivity));
            PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, intent, 0);

            //デフォルトの通知音を取得
            Android.Net.Uri uri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);

            Notification notification = new Notification.Builder(context)
                    .SetContentTitle(title + ": " + subTitle)
                    .SetSmallIcon(Resource.Drawable.icon)
                    .SetContentText(body)
                    .SetOngoing(false) //常駐するかどうか
                    .SetContentIntent(pendingIntent)
                    .SetSound(uri)     //通知音の設定
                    .Build();

            NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            manager.Notify(id, notification);
        }

        public void Off() {
            Context context = Forms.Context;
            NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            manager.Cancel(id);
        }
    }
}
