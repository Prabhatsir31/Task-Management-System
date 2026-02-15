using TaskManagementSystem.Domain.DTOs;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.ServiceIntegration
{
    public interface IEmailManager
    {
        ResultModel SendMessage(MailMessageDto mailMessageDto);
    }
}