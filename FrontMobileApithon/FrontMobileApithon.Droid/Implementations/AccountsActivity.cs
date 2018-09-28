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
using FrontMobileApithon.Droid.Implementations.Files;

namespace FrontMobileApithon.Droid.Implementations
{
    [Activity(Label = "AccountsActivity")]
    public class AccountsActivity : Activity
    {
        Switch simpleSwitch;
        bool isCapable = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.accounts);

            simpleSwitch = FindViewById<Switch>(Resource.Id.simpleSwitch);

            TextView capable = FindViewById<TextView>(Resource.Id.capable);

            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);
            continueBtn.Click += ContinueBtn_Click;

            Button exitBtn = FindViewById<Button>(Resource.Id.exitBtn);
            exitBtn.Click += ExitBtn_Click;

            if (isCapable)
            {
                capable.Text = "Por la suma de tus ingresos anuales, eres contribuyente y debes hacer la declaración de renta ante la DIAN";
            }
            else
            {
                capable.Text = "Por la suma de tus ingresos anuales, no eres contribuyente y no debes hacer la declaración de renta ante la DIAN";
            }            
        }

        private void ExitBtn_Click(object sender, EventArgs e)
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

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            if (simpleSwitch.Checked)
            {
                Intent intent = new Intent(this, typeof(DataFileActivity));
                StartActivity(intent);
            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("ALERTA");
                alert.SetMessage("Para continuar, debes aceptar que Bancolombia S.A procese tu información");
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task  
                });
                alert.Show();
            }
        }
    }
}