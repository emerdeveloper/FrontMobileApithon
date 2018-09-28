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
        bool isUpdated = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.home);
            clientInfo = JsonConvert.DeserializeObject<Models.Responses.Client.getClientResponse>(
            this.Intent.GetStringExtra("ClientInfo"));


            TextView grattings, info;
            
            grattings = FindViewById<TextView>(Resource.Id.grattings);
            grattings.Text = "Bienvenido" + " " + clientInfo.data[0].fullName;

            info = FindViewById<TextView>(Resource.Id.info);
            info.Text = "¿Sabías que la declaración de renta es un informe, de aquellos contribuyentes considerados como personas naturales, empleados o independientes, que se le presenta a la DIAN; que detalla la situación financiera de los colombianos?";

            Button nextBtn = FindViewById<Button>(Resource.Id.nextBtn);
            nextBtn.Click += NextBtn_Click;

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