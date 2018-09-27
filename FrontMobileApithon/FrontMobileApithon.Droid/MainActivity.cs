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

namespace FrontMobileApithon.Droid
{
	[Activity (Label = "Apithon", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
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

        public MainActivity()
        {
            ApiService = new ApiConsumer();
            CheckConnection = new CheckConnection();
        }

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_main);

            //subscribePush();
            // IsPlayServicesAvailable();
            // CreateNotificationChannel();


            LoadConfiguration();
            TextView conditionTxt = FindViewById<TextView>(Resource.Id.conditionTxt);
            conditionTxt.Click += ConditionTxt_Click;

        }

        private async void LoadConfiguration()
        {
            var Connection = await CheckConnection.Check();
            if (!Connection.IsSuccess)
            {
                CreateStatusTurnNoternetConnection(Connection.Message);
                return;
            }

            await Task.Delay(1500);

            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
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
            contentWebview.Visibility = ViewStates.Gone;
            contentSplash.Visibility = ViewStates.Invisible;

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
}


