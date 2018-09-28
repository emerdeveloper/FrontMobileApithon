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
    [Activity(Label = "AccountsActivity")]
    public class AccountsActivity : Activity
    {
        Switch simpleSwitch; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.accounts);

            simpleSwitch = FindViewById<Switch>(Resource.Id.simpleSwitch);

            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);
            continueBtn.Click += ContinueBtn_Click;

            Button exitBtn = FindViewById<Button>(Resource.Id.exitBtn);
            exitBtn.Click += ExitBtn_Click;
            
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("ALERTA");
            alert.SetMessage("¿Estás seguro de querer salir de la aplicación?");
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
                Intent intent = new Intent(this, typeof(UpdateDataActivity));
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