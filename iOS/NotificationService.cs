using System;
using Foundation;
using UIKit;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(madaarumk2.iOS.NotificationService))]
namespace madaarumk2.iOS {
    public class NotificationService : INotificationService {
        UILocalNotification _notification;

        public void Regist() {

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0)) {
                // 許可をもらう通知タイプの種類を定義
                UNAuthorizationOptions types = UNAuthorizationOptions.Badge | // アイコンバッチ
                                                UNAuthorizationOptions.Sound | // サウンド
                                                UNAuthorizationOptions.Alert;  // テキスト

                UNUserNotificationCenter.Current.RequestAuthorization(types, (granted, err) => {
                    // Handle approval
                    if (err != null) {
                        System.Diagnostics.Debug.WriteLine(err.LocalizedFailureReason + System.Environment.NewLine + err.LocalizedDescription);
                    }
                    if (granted) {
                    }
                });
            } else {//iOS9まで向け
                    // 許可をもらう通知タイプの種類を定義
                UIUserNotificationType types = UIUserNotificationType.Badge | // アイコンバッチ
                                               UIUserNotificationType.Sound | // サウンド
                                               UIUserNotificationType.Alert;  // テキスト
                                                                              // UIUserNotificationSettingsの生成
                UIUserNotificationSettings nSettings = UIUserNotificationSettings.GetSettingsForTypes(types, null);
                // アプリケーションに登録
                UIApplication.SharedApplication.RegisterUserNotificationSettings(nSettings);

            }

        }

        public void On(string title, string subTitle, string body) {

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0)) {
                UIApplication.SharedApplication.InvokeOnMainThread(delegate {
                    var content = new UNMutableNotificationContent();
                    content.Title = title;
                    content.Subtitle = title;
                    content.Body = body;
                    content.Sound = UNNotificationSound.Default;

                    var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);
                    //５秒後に通知、リピートしない
                    //日時を指定する場合は以下の方法
                    //NSDateComponents components = new NSDateComponents();
                    //components.TimeZone = NSTimeZone.DefaultTimeZone;
                    //components.Year = _notifyDate.LocalDateTime.Year;
                    //components.Month = _notifyDate.LocalDateTime.Month;
                    //components.Day = _notifyDate.LocalDateTime.Day;
                    //components.Hour = _notifyDate.LocalDateTime.Hour;
                    //components.Minute = _notifyDate.LocalDateTime.Minute;
                    //components.Second = _notifyDate.LocalDateTime.Second;
                    //var calendarTrigger = UNCalendarNotificationTrigger.CreateTrigger(components, false);

                    var requestID = "notifyKey";
                    content.UserInfo = NSDictionary.FromObjectAndKey(new NSString("notifyValue"), new NSString("notifyKey"));
                    var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                    UNUserNotificationCenter.Current.Delegate = new LocalNotificationCenterDelegate();

                    // ローカル通知を予約する
                    UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                        if (err != null) {
                            // Do something with error...
                            //LogUtility.OutPutError(err.LocalizedFailureReason + System.Environment.NewLine + err.LocalizedDescription);
                        }
                    });
                    UIApplication.SharedApplication.ApplicationIconBadgeNumber += 1; // アイコン上に表示するバッジの数値
                });

            } else {//iOS9まで向け
                UIApplication.SharedApplication.InvokeOnMainThread(delegate {
                    _notification = new UILocalNotification();
                    _notification.Init();
                    _notification.FireDate = NSDate.FromTimeIntervalSinceNow(10); //メッセージを通知する日時
                    _notification.TimeZone = NSTimeZone.DefaultTimeZone;
                    //_notification.RepeatInterval = NSCalendarUnit.Day; // 日々繰り返しする場合
                    _notification.AlertTitle = title;
                    _notification.AlertBody = body;
                    _notification.AlertAction = @"Open"; //ダイアログで表示されたときのボタンの文言
                    _notification.UserInfo = NSDictionary.FromObjectAndKey(new NSString("NotificationValue"), new NSString("NotificationKey"));
                    _notification.SoundName = UILocalNotification.DefaultSoundName;
                    // アイコン上に表示するバッジの数値
                    UIApplication.SharedApplication.ApplicationIconBadgeNumber += 1;
                    //通知を登録
                    UIApplication.SharedApplication.ScheduleLocalNotification(_notification);
                });
            }


        }
        public void Off() {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate {

                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0)) {
                    //全ての送信待ちの通知を削除する場合
                    //UNUserNotificationCenter.Current.RemoveAllDeliveredNotifications();
                    //通知時に設定したキーを元に通知情報をキャンセル
                    UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { "notifyKey" });

                } else {//iOS9まで向け
                    //通知時に設定したUserInfoを元に通知情報をキャンセルする
                    if (_notification != null &&
                        (NSString)(_notification.UserInfo.ObjectForKey(new NSString("NotificationKey"))) == new NSString("NotificationValue")) {
                        UIApplication.SharedApplication.CancelLocalNotification(_notification);
                    }
                }
            });
        }
    }
}
