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

namespace FrontMobileApithon.Droid.Implementations
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        bool isUpdated = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.home);

            TextView grattings, info;
            
            grattings = FindViewById<TextView>(Resource.Id.grattings);
            grattings.Text = "Bienvenido" + " " + "Nombre de individuo";

            info = FindViewById<TextView>(Resource.Id.info);
            info.Text = "¿Sabías que la declaración de renta es un informe, de aquellos contribuyentes considerados como personas naturales, empleados o independientes, que se le presenta a la DIAN; que detalla la situación financiera de los colombianos?";

            Button nextBtn = FindViewById<Button>(Resource.Id.nextBtn);
            nextBtn.Click += NextBtn_Click;

            Button closeBtn = FindViewById<Button>(Resource.Id.closeBtn);
            closeBtn.Click += CloseBtn_Click;

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
            });
            alert.SetButton2("CANCEL", (c, ev) => { });
            alert.Show();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (isUpdated)
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