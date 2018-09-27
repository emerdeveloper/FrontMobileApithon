using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using FrontMobileApithon.Droid.Implementations;

namespace FrontMobileApithon.Droid
{
	[Activity (Label = "FrontMobileApithon.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.activity_main);

            TextView conditionTxt = FindViewById<TextView>(Resource.Id.conditionTxt);

            conditionTxt.Click += ConditionTxt_Click;

        }

        private void ConditionTxt_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(termsAndConditionsActivity));
            StartActivity(intent);
        }
    }
}


