using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

[Service(Name ="com.kyutech.klab.and.ksl.madaaru_mk2.BackgroundService", Exported= false, Process =":BackgroundService")]
public class BackgroundService : Service {
    public override IBinder OnBind(Intent intent) {
        return null;
    }

    public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId) {
        Android.Util.Log.Debug("BackgroundService", "Started BackgroundService");
        Thread t = new Thread(() => {
            while (true) {
                System.Threading.Thread.Sleep(10000);
                var bundle = new Bundle();
                global::Xamarin.Forms.Forms.Init(this, bundle);

                // PCLのクラス実行
                madaarumk2.BackgroundNotification.Main();
            }
        });
        t.Start();

        return StartCommandResult.Sticky;
    }

    public void StartbackgroundService() {
        // サービス起動
        Intent serviceIntent = new Intent(this, typeof(BackgroundService));
        serviceIntent.AddFlags(ActivityFlags.NewTask);
        serviceIntent.SetPackage(this.PackageManager.GetPackageInfo(this.PackageName, 0).PackageName);
        base.StartService(serviceIntent);
    }

    public override void OnDestroy() {
        Android.Util.Log.Debug("BackgroundService", "Destroied BackgroundService");
        base.OnDestroy();
        // killされてもサービスを再起動する
        this.StartbackgroundService();
    }
}
