using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FrontMobileApithon.Models;
using Newtonsoft.Json;

namespace FrontMobileApithon.Droid.Implementations
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        #region Constructor
        public HomeActivity()
        {
            /* ApiService = new ApiConsumer();*/
            //CheckConnection = new CheckConnection();
            Init(this);
        }
        #endregion

        #region Singleton
        static HomeActivity instance = null;

        public static HomeActivity GetInstance()
        {
            if (instance == null)
            {
                return instance = new HomeActivity();
            }

            return instance;
        }

        static void Init(HomeActivity context)
        {
            instance = context;
        }
        #endregion

        public Models.Responses.Client.getClientResponse clientInfo { get; set; }
        public string access_token  { get; set; }

        bool showNotification;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.home);
            clientInfo = JsonConvert.DeserializeObject<Models.Responses.Client.getClientResponse>(
            this.Intent.GetStringExtra("ClientInfo"));
            access_token = this.Intent.GetStringExtra("token");

            TextView grattings, info;
            ImageView notification = FindViewById<ImageView>(Resource.Id.notification);

            notification.Click += Notification_Click;
            
            grattings = FindViewById<TextView>(Resource.Id.grattings);
            grattings.Text = "Hola" + " " + clientInfo.data[0].fullName;

            info = FindViewById<TextView>(Resource.Id.info);
            info.Text = "¿Sabías que la declaración de renta es un informe, de aquellos contribuyentes considerados como personas naturales, empleados o independientes, que se le presenta a la DIAN; que detalla la situación financiera de los colombianos?";

            Button nextBtn = FindViewById<Button>(Resource.Id.nextBtn);
            nextBtn.Click += NextBtn_Click;

            Button closeBtn = FindViewById<Button>(Resource.Id.closeBtn);
            closeBtn.Click += CloseBtn_Click;

            showNotification = clientInfo.data[0].declarationReady;

            if (showNotification)
            {
                notification.Visibility = ViewStates.Visible;
				Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
				AlertDialog alert = dialog.Create();
				alert.SetTitle("Notificación");
				alert.SetMessage("Tu declaración esta lista" + "\n" + "Descárgala tocando la notificación");
				alert.SetButton("ACEPTAR", (c, ev) =>
				{
				});
				alert.Show();
			}
            else
            {
                notification.Visibility = ViewStates.Gone;
            }
        }

        private void Notification_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DownloadActivity));
            StartActivity(intent);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("ALERTA");
            alert.SetMessage("¿Estás seguro de querer cerrar sesión?");
            alert.SetButton("OK", (c, ev) =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
				Finish();
            });
            alert.SetButton2("CANCEL", (c, ev) => { });
            alert.Show();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (!clientInfo.data[0].isUpdated)
            {
                Intent intent = new Intent(this, typeof(UpdateDataActivity));
                StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(AccountsActivity));
                StartActivity(intent);
            }
        }
    }
}