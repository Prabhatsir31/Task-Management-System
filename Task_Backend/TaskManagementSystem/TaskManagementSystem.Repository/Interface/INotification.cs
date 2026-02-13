using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface INotification
    {
        Task<ResultModel> AddNewNotification(NotificationRequest notification);
        Task<List<NotificationResponse>> GetMyNotification(int Userid);
        Task<int> GetMyNotificationCount(int Userid);
        Task<NotificationCount> GetMyNotificationCountAsync(int Userid);
        Task<List<NotificationResponse>> GetCurrentNotifications(int Userid,int count);
        Task<ResultModel> CloseNotificationByUser(int notificationId);
        Task<List<NotificationResponse>> GetDesktopNotifications(int Userid);
        Task<List<NotificationResponse>> GetMobileNotifications(int Userid);
        Task<ResultModel> CloseNotificationByUserForDesktop(int notificationId);
        Task<ResultModel> CloseNotificationByUserForMobile(int notificationId);
        Task<ResultModel> CloseAllNotificationByUser(int userId);

    }
}
