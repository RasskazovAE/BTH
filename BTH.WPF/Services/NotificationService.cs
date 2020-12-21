using BTH.Core.ViewModels.Interfaces;
using MaterialDesignThemes.Wpf;
using System;

namespace BTH.WPF.Services
{
    public class NotificationService : INotificationService
    {
        public static ISnackbarMessageQueue MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));

        public void Notify(string message)
        {
            MessageQueue.Enqueue(message);
        }
    }
}
