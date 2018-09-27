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
    [Activity(Label = "termsAndConditionsActivity")]
    public class termsAndConditionsActivity : Activity
    {
        ImageButton backBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.termsConditions);

            backBtn = FindViewById<ImageButton>(Resource.Id.backBtn);
            backBtn.Click += BackBtn_Click;           
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }
    }
}