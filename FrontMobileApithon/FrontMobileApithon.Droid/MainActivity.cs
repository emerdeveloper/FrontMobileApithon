using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using FrontMobileApithon.Droid.Implementations;
using Android.Webkit;
using Java.Net;

namespace FrontMobileApithon.Droid
{
	[Activity (Label = "FrontMobileApithon.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
    {
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            Intent intent;

            string urlLogin = "https://sbapi.bancolombia.com/security/oauth-otp/oauth2/authorize?client_id=16ebf6cc-38f7-497f-b064-7ca1d562727a&response_type=code&scope=Customer-financial:read:user Customer-ubication:read:user Customer-basic:read:user  Customer-document:write:user&redirect_uri=http://localhost:3000/code";

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.activity_main);

            TextView conditionTxt = FindViewById<TextView>(Resource.Id.conditionTxt);

            WebView webViewAPI = (WebView)FindViewById(Resource.Id.webViewAPI);

            webViewAPI.SetWebViewClient(new WebViewClientClass(this, webViewAPI));
            webViewAPI.LoadUrl(urlLogin);

            //Enabled Javascript in Websettings  
            WebSettings websettings = webViewAPI.Settings;
            websettings.JavaScriptEnabled = true;
            conditionTxt.Click += ConditionTxt_Click;
        }

        private void ConditionTxt_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(termsAndConditionsActivity));
            StartActivity(intent);
        }
    }
    internal class WebViewClientClass :  WebViewClient
    {
        Activity mActivity;
        WebView webviewApi;
        Intent intent;
        public event EventHandler OnPageLoad;

        public WebViewClientClass(Activity mActivity, WebView _webviewApi)
        {
            this.mActivity = mActivity;
            this.webviewApi = _webviewApi;
         }

        //Give the host application a chance to take over the control when a new URL is about to be loaded in the current WebView.  
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {

            view.LoadUrl(url);
            if (url.Contains("http://localhost:3000/code?code="))
            {
                string token = url.Substring(url.IndexOf("=") + 1);
                //Toast.MakeText(mActivity, token + "", ToastLength.Short).Show();
                webviewApi.Visibility = ViewStates.Gone;

                intent = new Intent(mActivity, typeof(AccountsActivity));
                mActivity.StartActivity(intent);
            }
            return true;
        }

    }
}


