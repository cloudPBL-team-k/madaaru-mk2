using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
//using madaarumk2;

[Service(Name ="com.kyutech.klab.and.ksl.madaaru_mk2.BackgroundService", Exported= false, Process =":BackgroundService")]
public class BackgroundService : Service {
    public override IBinder OnBind(Intent intent) {
        return null;
    }

    public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId) {
        Android.Util.Log.Debug("BackgroundService", "Started BackgroundService");
        Task.Run(async () => {
            while(true) {
                await Task.Delay(10000);
                // 開発用。ずっとサーバにアクセスし続けるので一旦何もしないようにする
                continue;

                var bundle = new Bundle();
                global::Xamarin.Forms.Forms.Init(this, bundle);

                // PCLのクラス実行
                madaarumk2.GetObjects go = new madaarumk2.GetObjects();
                int userId = 1;
                string jsonString = await go.GetExpendablesInfo(userId);
                if (jsonString == "null") {
                    continue;
                }
                List<madaarumk2.Expendables> expInfo = go.GetAllItemsObjectFromJson(jsonString);
                Dictionary<string, string> item = new Dictionary<string, string>();

                for (int n = 0; n < expInfo.Count; n++){
                    madaarumk2.BackgroundNotification.Main(expInfo[n].name);
                }
            }
        });

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
