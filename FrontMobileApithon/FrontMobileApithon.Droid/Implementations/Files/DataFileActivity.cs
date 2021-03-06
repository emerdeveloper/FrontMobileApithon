﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Com.Viewpagerindicator;
using FrontMobileApithon.Droid.Implementations.Notifications;
using FrontMobileApithon.Services;
using FrontMobileApithon.Utilities.Enums;

namespace FrontMobileApithon.Droid.Implementations.Files
{
    [Activity(Label = "FrontMobileApithon.Droid", MainLauncher = false, Icon = "@drawable/icon")]
    public class DataFileActivity : Activity
    {
        ViewPager foldersCarousel;
        string file;
        int count = 0;
		private ApiConsumer ApiService;

		public int[] folderCarousel = {
                Resource.Drawable.folder,
                Resource.Drawable.folder,
                Resource.Drawable.folder,
                Resource.Drawable.folder,
                Resource.Drawable.folder
            };

        CarouselAdapter carouselAdapter;
        ImageView one, two, three, four, five;
        TextView oneFile, secondFile, thirthFile, fourFile, fiveFile;
        Button continueBtn, othersBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.chooseFiles);
			ApiService = new ApiConsumer();
			foldersCarousel = FindViewById<ViewPager>(Resource.Id.cardCarousel);
            ImageButton next = FindViewById<ImageButton>(Resource.Id.next);
            ImageButton previous = FindViewById<ImageButton>(Resource.Id.previous);

            continueBtn = FindViewById<Button>(Resource.Id.continueBtn);
            continueBtn.Click += ContinueBtn_Click;

            othersBtn = FindViewById<Button>(Resource.Id.othersBtn);
            othersBtn.Click += OthersBtn_Click;

            carouselAdapter = new CarouselAdapter(this, folderCarousel);
            foldersCarousel.Adapter = carouselAdapter;
            CirclePageIndicator indicator = FindViewById<CirclePageIndicator>(Resource.Id.indicator);
            indicator.SetViewPager(foldersCarousel);

            one = FindViewById<ImageView>(Resource.Id.one);
            two = FindViewById<ImageView>(Resource.Id.two);
            three = FindViewById<ImageView>(Resource.Id.three);
            four = FindViewById<ImageView>(Resource.Id.four);
            five = FindViewById<ImageView>(Resource.Id.five);

            oneFile = FindViewById<TextView>(Resource.Id.oneFile);
            secondFile = FindViewById<TextView>(Resource.Id.secondFile);
            thirthFile = FindViewById<TextView>(Resource.Id.thirthFile);
            fourFile = FindViewById<TextView>(Resource.Id.fourFile);
            fiveFile = FindViewById<TextView>(Resource.Id.fiveFile);

            next.Click += Next_Click;
            previous.Click += Previous_Click;

        }

        private void OthersBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
			/*  Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
			  AlertDialog alert = dialog.Create();
			  alert.SetTitle("CARGA EXITOSA");
			  alert.SetMessage("El envío de tus documentos ha sido exitoso");
			  alert.SetButton("OK", (c, ev) =>
			  {
				  Intent intent = new Intent(this, typeof(HomeActivity));
				  StartActivity(intent);
			  });
			  alert.Show();*/

			CallApi();
		}

		public void CallApi()
		{
			/*
                 * progressbar.Visibility = ViewStates.Visible;
            contentWebview.Visibility = ViewStates.Gone;
                */

			Task.Factory.StartNew(() =>
			{

				//Armando el objeto para consumir API movements
				//No borrar Declara o nó

				var header = new Models.Request.Aggregation.Header
				{
					token = HomeActivity.GetInstance().access_token,
				};

				var documentFile1 = new Models.Request.Aggregation.Document
				{
					bank_id = "0001",
					financialProduct_type = "Credit Card",
					data_file = System.Text.Encoding.UTF8.GetBytes(oneFile.Text).ToString(),
				};

				var documentFile2 = new Models.Request.Aggregation.Document
				{
					bank_id = "0001",
					financialProduct_type = "Credit Card",
					data_file = System.Text.Encoding.UTF8.GetBytes(secondFile.Text).ToString(),
				};

				var documentsFileArray = new List<Models.Request.Aggregation.Document>();
				documentsFileArray.Add(documentFile1);
				documentsFileArray.Add(documentFile2);

				var datum = new Models.Request.Aggregation.Datum
				{
					header = header,
					document = documentsFileArray
				};

				
				var requestModel = new Models.Request.Aggregation.RootObject
				{
					data = new List<Models.Request.Aggregation.Datum>()
				};
				requestModel.data.Add(datum);





				var ResponseValiateStatement = ApiService.AggreationUploadFile(
												Constants.Url.AggregationServicePrefix,
												requestModel).Result;

				if (!ResponseValiateStatement.IsSuccess)
				{
					RunOnUiThread(() =>
					{
						/*progressbar.Visibility = Android.Views.ViewStates.Gone;*/
						Android.App.AlertDialog.Builder dialog1 = new AlertDialog.Builder(this);
						AlertDialog alert1 = dialog1.Create();
						alert1.SetTitle("Lo sentimos");
						alert1.SetMessage("Hubo un error inesperado");
						alert1.SetButton("Reintentar", (c, ev) =>
						{ CallApi(); });
						alert1.SetButton2("CANCEL", (c, ev) => {
							var intent2 = new Intent(this, typeof(DataFileActivity));
							StartActivity(intent2);
							Finish();
						});
						alert1.Show();
						return;
					});
				}

				/*RunOnUiThread(() =>
				{
					progressbar.Visibility = Android.Views.ViewStates.Gone;
                contentWebview.Visibility = Android.Views.ViewStates.Visible;
				});*/

				var ResponseAgregation = (Models.Responses.Aggregation.RootObject)ResponseValiateStatement.Result;
				if (ResponseAgregation.data[0].header.status == 200)
				{
					RunOnUiThread(() =>
					{
						Android.App.AlertDialog.Builder dialogs = new AlertDialog.Builder(this);
					AlertDialog alerts = dialogs.Create();
					alerts.SetTitle("Operación exitosa");
					alerts.SetMessage("Tus datos se han enviado exitosamente. En pocos minutos recibiras respuesta");
					alerts.SetButton("Aceptar", (c, ev) =>
					{
						/*var intent2 = new Intent(this, typeof(DataFileActivity));
						StartActivity(intent2);*/
						Finish();
					});

					alerts.Show();
					});
					return;
				}
				RunOnUiThread(() =>
				{
					Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
				AlertDialog alert = dialog.Create();
				alert.SetTitle("Lo sentimos");
				alert.SetMessage("Hubo un error inesperado");
				alert.SetButton("Reintentar", (c, ev) =>
				{ CallApi(); });
				alert.SetButton2("CANCEL", (c, ev) => {
					var intent2 = new Intent(this, typeof(DataFileActivity));
					StartActivity(intent2);
					Finish();
				});
				alert.Show();
				});
			});
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
            {
                carouselAdapter.Files();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (data != null)
            {
                file = data.DataString;
                count++;
            }

            switch (count)
            {
                case 1:
                    one.SetImageResource(Resource.Drawable.blue_circle);
                    oneFile.Text = file;
                    oneFile.Visibility = ViewStates.Visible;
                    break;
                case 2:
                    two.SetImageResource(Resource.Drawable.blue_circle);
                    secondFile.Text = file;
                    secondFile.Visibility = ViewStates.Visible;
                    break;
                case 3:
                    three.SetImageResource(Resource.Drawable.blue_circle);
                    thirthFile.Text = file;
                    thirthFile.Visibility = ViewStates.Visible;
                    break;
                case 4:
                    four.SetImageResource(Resource.Drawable.blue_circle);
                    fourFile.Text = file;
                    fourFile.Visibility = ViewStates.Visible;
                    break;
                case 5:
                    five.SetImageResource(Resource.Drawable.blue_circle);
                    fiveFile.Text = file;
                    fiveFile.Visibility = ViewStates.Visible;
                    break;
                default:
                    {
                        one.SetImageResource(Resource.Drawable.gray_circle);
                        two.SetImageResource(Resource.Drawable.gray_circle);
                        three.SetImageResource(Resource.Drawable.gray_circle);
                        four.SetImageResource(Resource.Drawable.gray_circle);
                        five.SetImageResource(Resource.Drawable.gray_circle);

                        break;
                    }
            }
        }

        void Next_Click(object sender, EventArgs e)
        {
            foldersCarousel.SetCurrentItem(foldersCarousel.CurrentItem + 1, true);
            //var intent = new Intent(this, typeof(NotificationService));
            //intent.PutExtra("Notification", true);
            //StartService(intent);

            /*
            Bundle sendNot = new Bundle();
            sendNot.PutString("SecondContent", "This message is sent");
            Intent intent = new Intent(this, typeof(AccountsActivity));
            intent.PutExtras(sendNot);

            Android.App.TaskStackBuilder stackBuilder = Android.App.TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(AccountsActivity)));
            stackBuilder.AddNextIntent(intent);

            PendingIntent pendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            //NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
            Notification notification = new Notification.Builder(this)
                         .SetSmallIcon(Resource.Drawable.Icon)
                         .SetColor(0x81CBC4)
                         .SetContentTitle(GetString(Resource.String.app_name))
                         .SetContentText(String.Format("Declaración de renta lista", "Titulo"))
                         .SetContentIntent(pendingIntent).Build();

            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(1000, notification);*/

        }

        void Previous_Click(object sender, EventArgs e)
        {
            foldersCarousel.SetCurrentItem(foldersCarousel.CurrentItem - 1, true);
        }
    }
}