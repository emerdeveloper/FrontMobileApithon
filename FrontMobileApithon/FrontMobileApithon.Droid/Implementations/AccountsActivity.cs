using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FrontMobileApithon.Droid.Implementations.Files;
using FrontMobileApithon.Services;
using FrontMobileApithon.Utilities.Enums;

namespace FrontMobileApithon.Droid.Implementations
{
    [Activity(Label = "AccountsActivity")]
    public class AccountsActivity : Activity
    {
        Switch simpleSwitch;
        bool isCapable = true;
        TextView capable;
        private ApiConsumer ApiService;
        LinearLayout contentLinearLayout;
        LinearLayout progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.accounts);
            ApiService = new ApiConsumer();
            simpleSwitch = FindViewById<Switch>(Resource.Id.simpleSwitch);

            capable = FindViewById<TextView>(Resource.Id.capable);

            contentLinearLayout = FindViewById<LinearLayout>(Resource.Id.contentLinearLayout);
            progressBar = FindViewById<LinearLayout>(Resource.Id.ProgressBar);

            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);
            continueBtn.Click += ContinueBtn_Click;

            Button exitBtn = FindViewById<Button>(Resource.Id.exitBtn);
            exitBtn.Click += ExitBtn_Click;
            CallApi();                              
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
				Finish();
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

        public void CallApi()
        {
            progressBar.Visibility = ViewStates.Visible;
            contentLinearLayout.Visibility = ViewStates.Gone;              

            Task.Factory.StartNew(() =>
            {

                //Armando el objeto para consumir API movements
                //No borrar Declara o nó

                var header = new Models.Request.Movements.Header
                {
                    token = HomeActivity.GetInstance().access_token,
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
                                                Constants.Url.MovementsServicePrefix,
                                                requestModel).Result;

                if (!ResponseValiateStatement.IsSuccess)
                {
                    RunOnUiThread(() =>
                    {
                        progressBar.Visibility = Android.Views.ViewStates.Gone;
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("ALERTA");
                        alert.SetMessage("Hubo un error inesperado");
                        alert.SetButton("ACEPTAR", (c, ev) =>
                        { CallApi(); });
                        alert.SetButton2("CANCEL", (c, ev) => {
							Finish();
						});
                        alert.Show();
                        return;
                    });
                }

                RunOnUiThread(() =>
                {
                progressBar.Visibility = Android.Views.ViewStates.Gone;
                contentLinearLayout.Visibility = Android.Views.ViewStates.Visible;
                });
                var Movements = (Models.Responses.Movements.RootObject)ResponseValiateStatement.Result;
                if (Movements.data[0].header.Status == 200)
                {
                    if (Movements.data[0].declaration)
                    {
						//TODO: Crear intent para que salga que debe declarar
						RunOnUiThread(() =>
						{
							capable.Text = "Por la suma de tus ingresos anuales, eres contribuyente y debes hacer la declaración de renta ante la DIAN";
						});
						return;
                    }

					// TODO: No declara
					RunOnUiThread(() =>
					{
						capable.Text = "Por la suma de tus ingresos anuales, no eres contribuyente y no debes hacer la declaración de renta ante la DIAN";
			});
		}
            });
        }

    }
}
 