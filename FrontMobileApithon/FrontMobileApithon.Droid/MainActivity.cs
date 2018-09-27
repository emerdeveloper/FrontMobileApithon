using System;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Firebase.Iid;
using FrontMobileApithon.Droid.Implementations;
using Android.Gms.Common;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Android.Views;
using FrontMobileApithon.Services;
using eBooks.Services;
using FrontMobileApithon.Droid.Utilities;
using Android.Webkit;
using Java.Net;

namespace FrontMobileApithon.Droid
{
	[Activity (Label = "Apithon", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        string urlLogin = "https://sbapi.bancolombia.com/security/oauth-otp/oauth2/authorize?client_id=16ebf6cc-38f7-497f-b064-7ca1d562727a&response_type=code&scope=Customer-financial:read:user Customer-ubication:read:user Customer-basic:read:user  Customer-document:write:user&redirect_uri=http://localhost:3000/code";
        static readonly string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        LinearLayout contentWebview;
        LinearLayout contentSplash;
        TextView appVersionTextView;
        TextView loadingTextView;
        private ApiConsumer ApiService;
        private CheckConnection CheckConnection;
        private LinearLayout contentMessageLayout;
        private ImageView statusImageView;
        private TextView messagetextView;
        public string access_token { get; set; }
        WebView webViewAPI;



        #region Constructor
        public MainActivity()
        {
            ApiService = new ApiConsumer();
            CheckConnection = new CheckConnection();
            Init(this);
        }
        #endregion

        #region Singleton
        static MainActivity instance = null;

        public static MainActivity GetInstance()
        {
            if (instance == null)
            {
                return instance = new MainActivity();
            }

            return instance;
        }

        static void Init(MainActivity context)
        {
            instance = context;
        }
        #endregion

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            
			SetContentView (Resource.Layout.activity_main);

            //subscribePush();
            // IsPlayServicesAvailable();
            // CreateNotificationChannel();
            InitControls();
            LoadConfiguration();
            
            

            //Enabled Javascript in Websettings  
            WebSettings websettings = webViewAPI.Settings;
            websettings.JavaScriptEnabled = true;
        }

        private async void LoadConfiguration()
        {
            var Connection = await CheckConnection.Check();
            if (!Connection.IsSuccess)
            {
                CreateStatusTurnNoternetConnection(Connection.Message);
                return;
            }

            webViewAPI.LoadUrl(urlLogin);
            await Task.Delay(1500);
            contentWebview.Visibility = ViewStates.Visible;
            contentSplash.Visibility = ViewStates.Gone;
        }

        private void CreateStatusTurnNoternetConnection(string message)
        {
            Utils.ShowDialogMessage(
                "Lo sentimos",
                message,
                "Acceptar",
                "",
                false,
                () =>
                {
                },
                () =>
                {
                    LoadConfiguration();
                });
        }

        private void InitControls()
        {
            contentWebview = FindViewById<LinearLayout>(Resource.Id.contentWebview);
            contentSplash = FindViewById<LinearLayout>(Resource.Id.contentSplash);
            webViewAPI = (WebView)FindViewById(Resource.Id.webViewAPI);
            webViewAPI.SetWebViewClient(new WebViewClientClass(this, webViewAPI));
            contentWebview.Visibility = ViewStates.Gone;
            contentSplash.Visibility = ViewStates.Visible;

        }

        private void ConditionTxt_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(TAG +"InstanceID token: " + FirebaseInstanceId.Instance.Token);

            /*Intent intent = new Intent(this, typeof(termsAndConditionsActivity));
            StartActivity(intent);*/
        }
        
        private void subscribePush()
        {
            /* if (!GetString(Resource.String.google_app_id).Equals("1:40631837524:android:0467f3be03ea856a"))
                 throw new System.Exception("Invalid Json file");

             Task.Run(() => {
                 var instanceId = FirebaseInstanceId.Instance;
                 instanceId.DeleteInstanceId();
                 Android.Util.Log.Debug("TAG", "{0} {1}", instanceId.Token, instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));


             });*/

            AppCenter.Start("6ff88f3a-e2a8-4f05-a4ab-ce4d8a3baa7c", typeof(Push));
        }

        private void IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (ConnectionResult.Success == 0)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(TAG + " *** " + "Success");
#endif
            }
            else
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(TAG + " *** " + "Error");
#endif
            }
        }

        void CreateNotificationChannel()
        {

           /* if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }*/
            /*
            var channel = new NotificationChannel(MyFirebaseMessagingService.CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);*/
        }
    }
    internal class WebViewClientClass :  WebViewClient
    {
        Activity mActivity;
        WebView webviewApi;
        Intent intent;

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
                CallApi();
                intent = new Intent(mActivity, typeof(AccountsActivity));
                mActivity.StartActivity(intent);
            }
            return true;
        }

        public async Task CallApi(string code)
        {
            var response = await ApiService.PostGetToken(code);

            if (!response.IsSuccess)
            {
                Utils.ShowDialogMessage(
                "Lo sentimos",
                message,
                "Acceptar",
                "",
                false,
                () =>
                {
                },
                () =>
                {
                });
                return; 
            }

            access_token = response.Result.GetTokenResponse.access_token;

            //Armando el objeto para consumir API movements
            var header = new Models.Request.Movements.Header
            {
                token = accessToken,
            };

            var datum = new Models.Request.Movements.Datum
            {
                header = header,
            };

            var requestModel = new Models.Request.Movements.RootObject
            {
                data = new List<Models.Request.Movements.Datum>()
            };
            requestModel.data.Add(datum);

            var ResponseValiateStatement = await ApiService.PostGetToken(
                                            access_token, 
                                            Constants.Url.MovementsServicePrefix, 
                                            requestModel);

            if (!response.IsSuccess)
            {
                Utils.ShowDialogMessage(
                "Lo sentimos",
                message,
                "Acceptar",
                "",
                false,
                () =>
                {
                },
                () =>
                {
                });
                return;
            }

            var Movements = (RootObject)response.Result;
            if (Movements[0].header.Status.Equals("200"))
            {
                if (Movements[0].declaration)
                {
                    //TODO: Crear intent para que salga que debe declarar
                    return;
                }

                // TODO: No declara
            }
        }
    }
}


