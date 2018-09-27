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
using Plugin.CurrentActivity;
using Android.Graphics.Drawables;
using Android.Graphics;
using static Android.App.ActionBar;

namespace FrontMobileApithon.Droid.Utilities
{
    public static class Utils
    {
        #region Attributes
        static Android.Support.V7.App.AlertDialog dialog;
        static Android.Support.V7.App.AlertDialog MessageDialogBuild;
        #endregion


        public static void ShowProgress()
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(() =>
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(CrossCurrentActivity.Current.Activity);
                var dialogView = CrossCurrentActivity.Current.Activity.LayoutInflater.Inflate(Resource.Layout.progressDialog, null);
                builder.SetView(dialogView);
                dialog = builder.Show();
                dialog.SetCancelable(false);
                dialog.Window.SetLayout(Utils.ConvertDpToPixels(200), LayoutParams.WrapContent);

                var progressText = dialogView.FindViewById<TextView>(Resource.Id.progressText);
                progressText.Text = "cargando...";
            });

        }

        public static void HideProgress()
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(() =>
            {
                if (dialog != null)
                {
                    dialog.Hide();

                    dialog = null;
                }

            });
        }
        public static void ShowDialogMessage(
           string title,
           string message,
           string okMessage,
           string secondaryOkMessage = "",
           bool hasSecondaryButton = false,
           Action action = null,
           Action okAction = null)
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(() =>
            {
                if (MessageDialogBuild != null)
                {
                    MessageDialogBuild.Hide();
                    MessageDialogBuild = null;
                }

                var builder = new Android.Support.V7.App.AlertDialog.Builder(CrossCurrentActivity.Current.Activity);
                View dialogView = CrossCurrentActivity.Current.Activity.LayoutInflater.Inflate(Resource.Layout.messageDialog, null);

                builder.SetView(dialogView);
                MessageDialogBuild = BuilMessageDialog(builder);

                var dialogImageView = dialogView.FindViewById<ImageView>(Resource.Id.dialogImageView);

                var titleMessageTextView = dialogView.FindViewById<TextView>(Resource.Id.titleMessageTextView);

                var messageTextView = dialogView.FindViewById<TextView>(Resource.Id.messageTextView);
                messageTextView.Text = message;

                var acceptButton = dialogView.FindViewById<TextView>(Resource.Id.acceptButton);
                acceptButton.Text = okMessage;

                var secondaryAcceptButton = dialogView.FindViewById<TextView>(Resource.Id.secondaryAcceptButton);
                secondaryAcceptButton.Text = secondaryOkMessage;

                dialogImageView.SetImageResource(Resource.Drawable.ic_notification_bell_black);

                if (hasSecondaryButton)
                    secondaryAcceptButton.Visibility = ViewStates.Visible;

                titleMessageTextView.Text = title;

                acceptButton.Click += (sender, e) =>
                {
                    MessageDialogBuild.Cancel();
                    okAction?.Invoke();
                };

                secondaryAcceptButton.Click += (sender, e) =>
                {
                    MessageDialogBuild.Cancel();
                    action?.Invoke();
                };
            });
        }

        private static Android.Support.V7.App.AlertDialog BuilMessageDialog(Android.Support.V7.App.AlertDialog.Builder builder)
        {
            Android.Support.V7.App.AlertDialog MessageDialog = builder.Show();
            MessageDialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            MessageDialog.SetCancelable(false);
            MessageDialog.SetCanceledOnTouchOutside(false);
            return MessageDialog;
        }

        public static int ConvertDpToPixels(int dpValue)
        {
            var pixels = (int)(dpValue * CrossCurrentActivity.Current.Activity.Resources.DisplayMetrics.Density);
            return pixels;
        }
    }
}