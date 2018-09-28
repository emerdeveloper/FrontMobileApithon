using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace FrontMobileApithon.Droid.Implementations
{
    [Activity(Label = "Downloads")]
    public class DownloadActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.download);

            TextView info = FindViewById<TextView>(Resource.Id.info);
            info.Text = "Recuerda que puedes alivios tributarios adquiriendo productos bancarios. Asesórate con nosotros y disfruta de estos tributarios que solo Bancolombia S.A te puede brindar";
            
            Button moreInfoBtn = FindViewById<Button>(Resource.Id.moreInfoBtn);
            Button downloadBtn = FindViewById<Button>(Resource.Id.downloadBtn);
            downloadBtn.Click += DownloadBtn_Click;

            ImageButton backBtn = FindViewById<ImageButton>(Resource.Id.backBtn);
            backBtn.Click += BackBtn_Click;

		}

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }
    }
}