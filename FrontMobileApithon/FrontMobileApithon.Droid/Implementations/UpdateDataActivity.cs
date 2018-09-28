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
using FrontMobileApithon.Models;
using FrontMobileApithon.Services;
using FrontMobileApithon.Utilities.Enums;

namespace FrontMobileApithon.Droid.Implementations
{
    [Activity(Label = "UpdateDataActivity")]
    public class UpdateDataActivity : Activity
    {
        LinearLayout contentLinearLayout;
        LinearLayout progressBar;

        EditText name, lastname, documentId, email, address, city, cellphone, work;
        ApiConsumer ApiService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.updateData);
            ApiService = new ApiConsumer();
            name = FindViewById<EditText>(Resource.Id.name);
            lastname = FindViewById<EditText>(Resource.Id.lastname);
            documentId = FindViewById<EditText>(Resource.Id.documentId);
            email = FindViewById<EditText>(Resource.Id.email);
            address = FindViewById<EditText>(Resource.Id.address);
            city = FindViewById<EditText>(Resource.Id.city);
            city.Text = "Medellín";
            cellphone = FindViewById<EditText>(Resource.Id.cellphone);
            work = FindViewById<EditText>(Resource.Id.work);
            work.Text = "Empleado";

            Button updateBtn = FindViewById<Button>(Resource.Id.updateBtn);
            updateBtn.Click += UpdateBtn_Click;

            contentLinearLayout = FindViewById<LinearLayout>(Resource.Id.contentLinearLayout);
            progressBar = FindViewById<LinearLayout>(Resource.Id.ProgressBar);

            //SEtear Datos a campos
            name.Text = HomeActivity.GetInstance().clientInfo.data[0].fullName;
            lastname.Text = HomeActivity.GetInstance().clientInfo.data[0].lastName;
            email.Text = HomeActivity.GetInstance().clientInfo.data[0].email;
            address.Text = HomeActivity.GetInstance().clientInfo.data[0].address;
            cellphone.Text = HomeActivity.GetInstance().clientInfo.data[0].cellPhone;
			progressBar.Visibility = ViewStates.Gone;
			contentLinearLayout.Visibility = ViewStates.Visible;
		}

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
			if (!String.IsNullOrEmpty(email.Text) && !String.IsNullOrEmpty(address.Text) && !String.IsNullOrEmpty(city.Text) && !String.IsNullOrEmpty(cellphone.Text) && !String.IsNullOrEmpty(city.Text) && !String.IsNullOrEmpty(work.Text))
			{
				/*AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetMessage("Tu información ha sido actualizada exitosamente");
                alert.SetButton("OK", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(AccountsActivity));
                    StartActivity(intent);
                });
                alert.Show();*/
				CallApi();

			}
			else
			{
				RunOnUiThread(() =>
				{

					AlertDialog.Builder dialog = new AlertDialog.Builder(this);
					AlertDialog alert = dialog.Create();
					alert.SetMessage("Debes completar todo el formulario para poder continuar");
					alert.SetButton("ACEPTAR", (c, ev) =>
					{
					});
					alert.Show();
				});
				//Toast.MakeText(this, "Debes completar todo el formulario para poder continuar", ToastLength.Long).Show();
			}
        }

        public void CallApi()
        {
            progressBar.Visibility = ViewStates.Visible;
            contentLinearLayout.Visibility = ViewStates.Gone;

            Task.Factory.StartNew(() =>
            {
                //UpdateClient
                var clientInfo = new Models.Request.UpdateClient.Datum
                {
                    address = address.Text,
                    cellPhone = cellphone.Text,
                    email = email.Text,
                    firstName = name.Text,
                    lastName = lastname.Text,
                    phone = cellphone.Text
                };

                var arrayListDataFotUpdate = new Models.Request.UpdateClient.UpdateClientRequest
                {
                    data = new List<Models.Request.UpdateClient.Datum>(),
                    _id = HomeActivity.GetInstance().clientInfo._id,
                    _rev = HomeActivity.GetInstance().clientInfo._rev,
                };
                arrayListDataFotUpdate.data.Add(clientInfo);

                var response = ApiService.UpdateInfo(Constants.Url.UpdateClientInfoServicePrefix, arrayListDataFotUpdate).Result;

                if (!response.IsSuccess)
                {
                    RunOnUiThread(() =>
                    {
						progressBar.Visibility = ViewStates.Gone;
						contentLinearLayout.Visibility = ViewStates.Visible;
						Android.App.AlertDialog.Builder dialo = new AlertDialog.Builder(this);
                        AlertDialog aler = dialo.Create();
                        aler.SetTitle("ALERTA");
                        aler.SetMessage("Hubo un error inesperado");
                        aler.SetButton("ACEPTAR", (c, ev) =>
                        { });
                        aler.SetButton2("CANCEL", (c, ev) => { });
                        aler.Show();
					});
					return;
                }

               
                var Client = (Models.Responses.UpdateClient.UpdateClientResponse)response.Result;

                if (Client.data[0].header.status.Equals("200"))
                {
					RunOnUiThread(() =>
					{

						progressBar.Visibility = Android.Views.ViewStates.Gone;
						contentLinearLayout.Visibility = Android.Views.ViewStates.Visible;
						Android.App.AlertDialog.Builder dialogs = new AlertDialog.Builder(this);
                    AlertDialog alerts = dialogs.Create();
                    alerts.SetTitle("Operación Exitosa");
                    alerts.SetMessage("Sui información ha sido actualizada");
                    alerts.SetButton("ACEPTAR", (c, ev) =>
                    {
                        var intent = new Intent(this, typeof(AccountsActivity));
                        StartActivity(intent);
                        Finish();
                    });
                    alerts.Show();
					});
					return;
                }

				RunOnUiThread(() =>
				{

					progressBar.Visibility = Android.Views.ViewStates.Gone;
					contentLinearLayout.Visibility = Android.Views.ViewStates.Visible;
					Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("ALERTA");
                alert.SetMessage("Hubo un error inesperado");
                alert.SetButton("ACEPTAR", (c, ev) =>
                { });
                alert.SetButton2("CANCEL", (c, ev) => { });
                alert.Show();
				});
				return;
            });

         
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
                    progressBar.Visibility = Android.Views.ViewStates.Gone;
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
                progressBar.Visibility = Android.Views.ViewStates.Gone;
            contentLinearLayout.Visibility = Android.Views.ViewStates.Visible;
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
        }

    }
}