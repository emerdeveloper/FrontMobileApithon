using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FrontMobileApithon.Droid.Implementations.Files;

namespace FrontMobileApithon.Droid.Implementations.Notifications
{
    [Service]
    class NotificationService : Service
    {
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(10000);

                /*
                var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(AccountsActivity)), 0);
                Notification notification = new Notification.Builder(this)
                        .SetSmallIcon(Resource.Drawable.Icon)
                        .SetColor(0x81CBC4)
                        .SetContentTitle(GetString(Resource.String.app_name))
                        .SetContentText(String.Format("Declaración de renta lista", "Titulo"))
                        .SetContentIntent(pendingIntent).Build();

                NotificationManager notificationManager = (NotificationManager)GetSystemService(Service.NotificationService);

                notificationManager.Notify(9999, notification);*/

                var isForeground = intent.GetBooleanExtra("Notification", true);

                if (isForeground == true)
                {
                    var title = intent.GetStringExtra("Title");

                    var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(AccountsActivity)), 0);

                    var notification = new Notification.Builder(this)
                        .SetSmallIcon(Resource.Drawable.Icon)
                        .SetColor(0x81CBC4)
                        .SetContentTitle(GetString(Resource.String.app_name))
                        .SetContentText(String.Format("Declaración de renta lista", "Titulo"))
                        .SetContentIntent(pendingIntent).Build();

                    StartForeground(9999, notification);
                }
            });

            return StartCommandResult.RedeliverIntent;
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}