using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Brag.SharedServices.Services.Messages
{
    public interface IEmailService
    {
        Task<bool> SendMail_SendGrid(string mailTo, string subject, string body, string displayName);
        bool SendMail_Attachment_SendGrid(string mailTo, string subject, string body, string displayName, byte[] attachment_file, string ReceiverName);
        bool IsValidKeys(string XKey);
    }
}
