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
    [Activity(Label = "UpdateDataActivity")]
    public class UpdateDataActivity : Activity
    {
        EditText name, lastname, documentId, email, address, city, cellphone, work;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.updateData);

            name = FindViewById<EditText>(Resource.Id.name);
            lastname = FindViewById<EditText>(Resource.Id.lastname);
            documentId = FindViewById<EditText>(Resource.Id.documentId);
            email = FindViewById<EditText>(Resource.Id.email);
            address = FindViewById<EditText>(Resource.Id.address);
            city = FindViewById<EditText>(Resource.Id.city);
            cellphone = FindViewById<EditText>(Resource.Id.cellphone);
            work = FindViewById<EditText>(Resource.Id.work);

            Button updateBtn = FindViewById<Button>(Resource.Id.updateBtn);
            updateBtn.Click += UpdateBtn_Click;

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(email.Text) && !String.IsNullOrEmpty(address.Text) && !String.IsNullOrEmpty(city.Text) && !String.IsNullOrEmpty(cellphone.Text) && !String.IsNullOrEmpty(city.Text) && !String.IsNullOrEmpty(work.Text))
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetMessage("Tu información ha sido actualizada exitosamente");
                alert.SetButton("OK", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(AccountsActivity));
                    StartActivity(intent);
                });
                alert.Show();
            }
            else
            {
                Toast.MakeText(this, "Debes completar todo el formulario para poder continuar", ToastLength.Long).Show();
            }
        }
    }
}