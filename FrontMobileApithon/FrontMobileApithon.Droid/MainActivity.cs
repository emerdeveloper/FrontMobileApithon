using System;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using FrontMobileApithon.Droid.Implementations;
using Android.Views;
using FrontMobileApithon.Services;
using eBooks.Services;
using Android.Webkit;
using FrontMobileApithon.Models.Responses;
using System.Collections.Generic;
using FrontMobileApithon.Utilities.Enums;
using FrontMobileApithon.Models;

namespace FrontMobileApithon.Droid
{
    [Activity(Label = "Apithon", MainLauncher = true, Icon = "@drawable/icon")]
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
        //private ApiConsumer ApiService;
        private CheckConnection CheckConnection;
        private LinearLayout contentMessageLayout;
        private ImageView statusImageView;
        private TextView messagetextView;
        TextView conditionTxt;
        private LinearLayout progressBar;
        //public string access_token { get; set; }
        WebView webViewAPI;



        #region Constructor
        public MainActivity()
        {
            /* ApiService = new ApiConsumer();*/
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);

            //subscribePush();
            // IsPlayServicesAvailable();
            // CreateNotificationChannel();
            InitControls();
        }

        private void CreateStatusTurnNoternetConnection(string message)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Lo sentimos");
            alert.SetMessage(message);
            alert.SetButton("ACEPTAR", (c, ev) =>
            { });
            alert.Show();
        }

        private void InitControls()
        {
            contentWebview = FindViewById<LinearLayout>(Resource.Id.contentWebview);
            contentSplash = FindViewById<LinearLayout>(Resource.Id.contentSplash);
            conditionTxt = FindViewById<TextView>(Resource.Id.conditionTxt);
            webViewAPI = (WebView)FindViewById(Resource.Id.webViewAPI);
            progressBar = FindViewById<LinearLayout>(Resource.Id.ProgressBar);
            webViewAPI.SetWebViewClient(new WebViewClientClass(this, webViewAPI, progressBar, contentWebview));
            webViewAPI.LoadUrl(urlLogin);

            WebSettings websettings = webViewAPI.Settings;
            websettings.JavaScriptEnabled = true;

            contentWebview.Visibility = ViewStates.Gone;
            contentSplash.Visibility = ViewStates.Visible;
            progressBar.Visibility = Android.Views.ViewStates.Gone;
            conditionTxt.Click += ConditionTxt_Click;
            LoadConfiguration();
        }


        private async void LoadConfiguration()
        {
            /*var Connection = await CheckConnection.Check();
            if (!Connection.IsSuccess)
            {
                CreateStatusTurnNoternetConnection(Connection.Message);
                return;
            }*/

            await Task.Delay(2000);
            contentWebview.Visibility = ViewStates.Visible;
            contentSplash.Visibility = ViewStates.Gone;
        }

        private void ConditionTxt_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(termsAndConditionsActivity));
            StartActivity(intent);
        }

    }
    internal class WebViewClientClass : WebViewClient
    {
        Activity mActivity;
        WebView webviewApi;
        Intent intent;
        LinearLayout progressbar;
        LinearLayout contentWebview;
        private ApiConsumer ApiService;
        private CheckConnection CheckConnection;
        string access_token { get; set; }
        ClientInfo clientInfo { get; set; }
        static Android.Support.V7.App.AlertDialog dialog;

        public WebViewClientClass(Activity mActivity, WebView _webviewApi, LinearLayout progressbar, LinearLayout contentWebview)
        {
            this.mActivity = mActivity;
            this.webviewApi = _webviewApi;
            this.progressbar = progressbar;
            this.contentWebview = contentWebview;
            ApiService = new ApiConsumer();
            CheckConnection = new CheckConnection();
        }

        //Give the host application a chance to take over the control when a new URL is about to be loaded in the current WebView.  
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {

            view.LoadUrl(url);
            if (url.Contains("http://localhost:3000/code?code="))
            {
                string token = url.Substring(url.IndexOf("=") + 1);
                Toast.MakeText(mActivity, token + "", ToastLength.Short).Show();
                webviewApi.Visibility = ViewStates.Invisible;
                CallApi(token);
                var response = ApiService.PostGetToken(token);

            }
            return true;
        }

        public void CallApi(string code)
        {
            progressbar.Visibility = ViewStates.Visible;
            contentWebview.Visibility = ViewStates.Gone;
            Task.Factory.StartNew(() =>
            {
                var response = ApiService.PostGetToken(code);

                if (!response.Result.IsSuccess)
                {
                    mActivity.RunOnUiThread(() =>
                    {
                        progressbar.Visibility = ViewStates.Gone;
                        contentWebview.Visibility = ViewStates.Visible;
                        AlertDialog.Builder dialog = new AlertDialog.Builder(mActivity);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("ALERTA");
                        alert.SetMessage("Hubo un error inesperado");
                        alert.SetButton("ACEPTAR", (c, ev) =>
                        { });
                        alert.SetButton2("CANCEL", (c, ev) => { });
                        alert.Show();
                    });
                }

                var access_token = ((GetTokenResponse)response.Result.Result).access_token;


                //GetClient
                var header = new Models.Request.Client.Header
                {
                    token = access_token,
                };

                var datum = new Models.Request.Client.Datum
                {
                    header = header,
                };

                var requestModel = new Models.Request.Client.getClientRequest
                {
                    data = new List<Models.Request.Client.Datum>()
                };
                requestModel.data.Add(datum);

                var ResponseClientInfo = ApiService.GetClientInfo(
                                                access_token,
                                                Constants.Url.MovementsServicePrefix,
                                                requestModel);

                if (!ResponseClientInfo.Result.IsSuccess)
                {
                    mActivity.RunOnUiThread(() =>
                    {
                        progressbar.Visibility = Android.Views.ViewStates.Gone;
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(mActivity);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("ALERTA");
                        alert.SetMessage("Hubo un error inesperado");
                        alert.SetButton("ACEPTAR", (c, ev) =>
                        { });
                        alert.SetButton2("CANCEL", (c, ev) => { });
                        alert.Show();
                        return;
                    });
                }

                mActivity.RunOnUiThread(() =>
                {
                    progressbar.Visibility = Android.Views.ViewStates.Gone;
                    contentWebview.Visibility = Android.Views.ViewStates.Visible;
                });
                var Client = (Models.Responses.Client.getClientResponse)ResponseClientInfo.Result.Result;
               /* clientInfo = new ClientInfo
                {
                    address = Client.data[0].address,
                    cellPhone = Client.data[0].cellPhone,
                    declarationReady = Client.data[0].declarationReady,
                    email = Client.data[0].email,
                    firstName = Client.data[0].firstName,
                    address = Client.data[0].address,
                }; */


                //Armando el objeto para consumir API movements
                //No borrar Declara o nó
                /*
                var header = new Models.Request.Movements.Header
                {
                    token = access_token,
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





                var ResponseValiateStatement = ApiService.PostGetMovements(
                                                access_token,
                                                Constants.Url.MovementsServicePrefix,
                                                requestModel);

                if (!ResponseValiateStatement.Result.IsSuccess)
                {
                    mActivity.RunOnUiThread(() =>
                    {
                        progressbar.Visibility = Android.Views.ViewStates.Gone;
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(mActivity);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("ALERTA");
                        alert.SetMessage("Hubo un error inesperado");
                        alert.SetButton("ACEPTAR", (c, ev) =>
                        { });
                        alert.SetButton2("CANCEL", (c, ev) => { });
                        alert.Show();
                        return;
                    });
                }

                mActivity.RunOnUiThread(() =>
                {
                    progressbar.Visibility = Android.Views.ViewStates.Gone;
                contentWebview.Visibility = Android.Views.ViewStates.Visible;
                });
                var Movements = (Models.Responses.Movements.RootObject)ResponseValiateStatement.Result.Result;
                if (Movements.data[0].header.Status.Equals("200"))
                {
                    if (Movements.data[0].declaration)
                    {
                        //TODO: Crear intent para que salga que debe declarar

                        intent = new Intent(mActivity, typeof(AccountsActivity));
                        mActivity.StartActivity(intent);
                        return;
                    }

                    // TODO: No declara
                }*/
            });

        }
    }
}


