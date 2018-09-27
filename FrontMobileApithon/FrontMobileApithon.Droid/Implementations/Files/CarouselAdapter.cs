﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using static Android.Support.V4.View.ViewPager;

namespace FrontMobileApithon.Droid.Implementations.Files
{
    public class CarouselAdapter : PagerAdapter, IOnPageChangeListener
    {
        Context context;
        int[] folderList = { };

        public CarouselAdapter(Context _context, int[] _folderList)
        {
            this.context = _context;
            this.folderList = _folderList;
        }

        public override int Count
        {
            get
            {
                return folderList.Length;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == ((ImageView)@object);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            ImageView imageView = new ImageView(context);
            imageView.SetScaleType(ImageView.ScaleType.FitCenter);
            imageView.SetImageResource(folderList[position]);

            Toast.MakeText(context, position + "", ToastLength.Short).Show();

            ((ViewPager)container).AddView(imageView, 0);

            return imageView;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            ((ViewPager)container).RemoveView((ImageView)@object);
        }

        public void OnPageScrollStateChanged(int state)
        {
            throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            throw new NotImplementedException();
        }
    }
}