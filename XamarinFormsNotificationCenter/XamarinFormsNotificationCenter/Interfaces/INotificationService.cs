using System;

namespace XamarinFormsNotificationCenter
{
    public interface INotificationService
    {
        event EventHandler NotificationReceived;
        void Initialize();
        int ScheduleNotification(string title, string message);
        void ReceiveNotification(string title, string message);
    }
}
