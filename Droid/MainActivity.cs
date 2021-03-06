﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace madaarumk2.Droid {
    [Activity(Label = "madaaru-mk2.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //未処理の例外を捕まえる.未処理なのでアプリが終了
            AppDomain.CurrentDomain.UnhandledException += (s, e) => { };
            //強制的に補足済みの例外にしてしまう.終了しない
            //AndroidEnvironment.UnhandledExceptionRaiser += (s, e) => { e.Handled = true; };


            LoadApplication(new App());

            Android.Util.Log.Debug("MainActivity", "Start BackgroundService");
            // バックグラウンドサービス起動
            var intent = new Intent(this, typeof(BackgroundService));
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
                // After Lollipop(?)
                Context context = Android.App.Application.Context;
                string packageName = context.PackageManager.GetPackageInfo(context.PackageName, 0).PackageName;
                intent.SetPackage(packageName);
                intent.SetClassName(context, packageName + ".BackgroundService");
            } else {
                intent.AddFlags(ActivityFlags.NewTask);
            }
            intent.PutExtra("userid", 1);
            StartService(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) {
            global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
