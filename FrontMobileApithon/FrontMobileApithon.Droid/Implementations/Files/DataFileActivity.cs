using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Com.Viewpagerindicator;

namespace FrontMobileApithon.Droid.Implementations.Files
{
    [Activity(Label = "FrontMobileApithon.Droid", MainLauncher = false, Icon = "@drawable/icon")]
    public class DataFileActivity : Activity, Android.Support.V4.View.ViewPager.IOnPageChangeListener
    {
        ViewPager cardCarousel;

        public int[] cardList = {
                Resource.Drawable.carpeta,
                Resource.Drawable.carpeta,
                Resource.Drawable.carpeta,
                Resource.Drawable.carpeta,
                Resource.Drawable.carpeta
            };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.chooseFiles);

            cardCarousel = FindViewById<ViewPager>(Resource.Id.cardCarousel);
            ImageButton next = FindViewById<ImageButton>(Resource.Id.next);
            ImageButton previous = FindViewById<ImageButton>(Resource.Id.previous);
            CarouselAdapter carouselAdapter = new CarouselAdapter(this, cardList);
            cardCarousel.Adapter = carouselAdapter;
            cardCarousel.AddOnPageChangeListener(this);
            CirclePageIndicator indicator = FindViewById<CirclePageIndicator>(Resource.Id.indicator);
            indicator.SetViewPager(cardCarousel);

            next.Click += Next_Click;
            previous.Click += Previous_Click;

        }

        void Next_Click(object sender, EventArgs e)
        {
            cardCarousel.SetCurrentItem(cardCarousel.CurrentItem + 1, true);
        }

        void Previous_Click(object sender, EventArgs e)
        {
            cardCarousel.SetCurrentItem(cardCarousel.CurrentItem - 1, true);
        }

        public void OnPageScrollStateChanged(int state)
        {
            //throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            //throw new NotImplementedException();
        }
    }
}