using TaskManagement.Domain.DTOs;
using TaskManagement.Domain.ResponseModel;

namespace TaskManagement.ServiceIntegration
{
    public interface IEmailManager
    {
        ResultModel SendMessage(MailMessageDto mailMessageDto);
    }
}