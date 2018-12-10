using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.App;
using Android;
using Android.Webkit;
using Java.Interop;
using Android.Locations;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            if (SupportActionBar != null)
            {
                SupportActionBar.Hide();
            }
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Android.Content.PM.Permission.Denied &&
                ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) == Android.Content.PM.Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, new string[] {
                    Manifest.Permission.AccessCoarseLocation,
                    Manifest.Permission.AccessFineLocation
                }, 2);
            }
            LaunchWeb("http://192.168.0.149/MZ.GisClient/default.aspx?f=m");
        }

        public void LaunchWeb(string url)
        {
            WebView mainWV = FindViewById<WebView>(Resource.Id.mainActivityWebView);
            mainWV.LoadUrl(url);

            mainWV.AddJavascriptInterface(new AndroidJavascriptObject(this, mainWV), "android");

            mainWV.Settings.JavaScriptEnabled = true;
            mainWV.Settings.DomStorageEnabled = true;


            mainWV.SetWebViewClient(new NewWebViewClient());


            //http://192.168.0.149/MZ.GisClient/default.aspx?f=m
        }
    }

    public class NewWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            return base.ShouldOverrideUrlLoading(view, url);
        }
    }



    public class AndroidJavascriptObject : Java.Lang.Object
    {
        Activity Activity { get; set; } = null;
        WebView WebView { get; set; } = null;
        public AndroidJavascriptObject(Activity activity, WebView webView)
        {
            Activity = activity;
            WebView = webView;
        }

        [Export("FunA")]
        [JavascriptInterface]
        public void FunA(string lon, string lat)
        {
            //Toast.MakeText(Activity.ApplicationContext, $"( {lon} , {lat} )", ToastLength.Short).Show();

        }

        [Export("FunTryLocation")]
        [JavascriptInterface]
        public void FunTryLocation()
        {
            Toast.MakeText(Activity.ApplicationContext, "Invoke FunTryLocation", ToastLength.Short).Show();


            


            //this.WebView.SetNetworkLocation( );

        }




    }


    public static class WebViewExtension
    {
        public static void SetNetworkLocation(this WebView webView, double longitude, double latitude)
        {


        }

    }

}