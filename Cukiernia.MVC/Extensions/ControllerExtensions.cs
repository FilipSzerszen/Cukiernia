using Cukiernia.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cukiernia.MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static void SetNotification( this Controller controller, string Type, string Message)
        {
            var notification = new Notification(Type, Message);
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
        }
    }
}
