using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinFormsNotificationCenter.Droid.NotificationService))]
namespace XamarinFormsNotificationCenter.Droid
{
    public class NotificationService : INotificationService
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";
        const int pendingIntentId = 0;

        bool channelInitialized = false;
        int messageId = -1;

        public const string TitleKey = "title";
        public const string MessageKey = "message";
        public event EventHandler NotificationReceived;

        NotificationManager manager;

        public void Initialize()
        {
            CreateNotificationChannel();
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);

                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }

        public void ReceiveNotification(string title, string message)
        {
            throw new NotImplementedException();
        }

        public int ScheduleNotification(string title, string message)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++;

            Intent intent = new Intent(Application.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.speech_bubble_white))
                .SetSmallIcon(Resource.Drawable.speech_bubble_white)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            var notification = builder.Build();
            manager.Notify(messageId, notification);

            return messageId;
        }
    }
}