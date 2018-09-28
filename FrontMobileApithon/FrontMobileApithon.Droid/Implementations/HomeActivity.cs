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
        ClientInfo clientInfo;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.home);
            clientInfo = JsonConvert.DeserializeObject<ClientInfo>(
            this.Intent.GetStringExtra("ClientInfo"));


            Button nextBtn = FindViewById<Button>(Resource.Id.nextBtn);
        }
    }
}