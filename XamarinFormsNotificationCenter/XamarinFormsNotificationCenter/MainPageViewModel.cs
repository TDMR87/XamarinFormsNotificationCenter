using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormsNotificationCenter
{
    public class MainPageViewModel
    {
        public string NotificationText { get; set; }

        INotificationService notificationService;

        public MainPageViewModel()
        {
            notificationService = DependencyService.Get<INotificationService>();
        }

        public ICommand SendNotification
        {
            get => new Command(() => notificationService.ScheduleNotification("Bling!", NotificationText));
        }
    }
}
