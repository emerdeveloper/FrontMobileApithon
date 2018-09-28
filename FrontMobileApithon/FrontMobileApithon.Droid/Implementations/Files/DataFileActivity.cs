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
        int count = 0;

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
                    one.SetImageResource(Resource.Drawable.yellow_circle);
                    oneFile.Text = file;
                    oneFile.Visibility = ViewStates.Visible;
                    break;
                case 2:
                    two.SetImageResource(Resource.Drawable.yellow_circle);
                    secondFile.Text = file;
                    secondFile.Visibility = ViewStates.Visible;
                    break;
                case 3:
                    three.SetImageResource(Resource.Drawable.yellow_circle);
                    thirthFile.Text = file;
                    thirthFile.Visibility = ViewStates.Visible;
                    break;
                case 4:
                    four.SetImageResource(Resource.Drawable.yellow_circle);
                    fourFile.Text = file;
                    fourFile.Visibility = ViewStates.Visible;
                    break;
                case 5:
                    five.SetImageResource(Resource.Drawable.yellow_circle);
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
            
        }

        void Previous_Click(object sender, EventArgs e)
        {
            foldersCarousel.SetCurrentItem(foldersCarousel.CurrentItem - 1, true);
        }
    }
}