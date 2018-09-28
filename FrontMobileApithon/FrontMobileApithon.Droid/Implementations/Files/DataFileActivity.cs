using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

namespace FrontMobileApithon.Droid.Implementations.Files
{
    [Activity(Label = "FrontMobileApithon.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class DataFileActivity : Activity
    {
        ViewPager foldersCarousel;
        string file;

        public int[] folderCarousel = {
                Resource.Drawable.folder,
                Resource.Drawable.folder,
                Resource.Drawable.folder,
                Resource.Drawable.folder,
                Resource.Drawable.folder
            };

        CarouselAdapter carouselAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.chooseFiles);

            foldersCarousel = FindViewById<ViewPager>(Resource.Id.cardCarousel);
            ImageButton next = FindViewById<ImageButton>(Resource.Id.next);
            ImageButton previous = FindViewById<ImageButton>(Resource.Id.previous);
            carouselAdapter = new CarouselAdapter(this, folderCarousel);
            foldersCarousel.Adapter = carouselAdapter;
            CirclePageIndicator indicator = FindViewById<CirclePageIndicator>(Resource.Id.indicator);
            indicator.SetViewPager(foldersCarousel);

            next.Click += Next_Click;
            previous.Click += Previous_Click;

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
            }
        }

        void Next_Click(object sender, EventArgs e)
        {
            foldersCarousel.SetCurrentItem(foldersCarousel.CurrentItem + 1, true);
            
        }

        void Previous_Click(object sender, EventArgs e)
        {
            foldersCarousel.SetCurrentItem(foldersCarousel.CurrentItem - 1, true);
        }
    }
}