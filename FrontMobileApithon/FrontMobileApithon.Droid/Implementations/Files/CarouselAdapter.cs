using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using static Android.Support.V4.View.ViewPager;

namespace FrontMobileApithon.Droid.Implementations.Files
{
    public class CarouselAdapter : PagerAdapter
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
            ImageButton imageView = new ImageButton(context);
            imageView.SetBackgroundColor(Color.Transparent);
            imageView.SetScaleType(ImageButton.ScaleType.FitCenter);
            imageView.SetImageResource(folderList[position]);
           
            imageView.Click += (o, e) =>
            {
                string[] permissions = {
                Manifest.Permission.ReadExternalStorage
                };

                if (ActivityCompat.ShouldShowRequestPermissionRationale((Activity)context, Manifest.Permission.ReadExternalStorage))
                {
                    /*
                    ScrollView scrollView = this.FindViewById<ScrollView>(Resource.Id.scrollView);

                    Snackbar.Make(scrollView, AppSettings.GetResourceMessage(Constants.Permissions.AndroidExternalWriteMessage, Language.ES),
                        Snackbar.LengthIndefinite).SetAction(AppSettings.GetResourceMessage(Constants.ResoursesText.OkButton, Language.ES), new Action<View>(delegate (View obj) {
                            ActivityCompat.RequestPermissions(this, permissions, (int)PermissionsRequestCode.ReceiptCreated);
                        })).Show();*/
                }
                else
                {
                    // Contact permissions have not been granted yet. Request them directly.
                    ActivityCompat.RequestPermissions((Activity)context, permissions, 0);
                }
                
            }; 

            Toast.MakeText(context, position + "", ToastLength.Short).Show();

            ((ViewPager)container).AddView(imageView, 0);

            return imageView;
        }

        public void Files()
        {
            Intent intent = new Intent();
            intent.SetAction(Intent.ActionGetContent);

            //intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("*/*");
            ((Activity)context).StartActivityForResult(Intent.CreateChooser(intent, "Select file"), 0);
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            ((ViewPager)container).RemoveView((ImageView)@object);
        }
    }
}