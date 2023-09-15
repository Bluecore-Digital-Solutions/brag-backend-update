



using Brag.Domain.Exceptions;
using Brag.Domain.Model.Configuration;
using Brag.SharedServices.Extensions;
using Brag.SharedServices.Helpers;
using Brag.SharedServices.Statics;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Brag.SharedServices.Services.Messages
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        private string SendGridAPIKEY;
        private readonly SmtpClient _smtpServer;
        private List<EmailAddress> emailAddresses = new List<EmailAddress>();

        public EmailService(IOptions<AppSettings> appSettings)
        {

            _appSettings = appSettings.Value;
            SendGridAPIKEY =
            ConvertersHelper.ConvertByteToString(ConvertersHelper.ConvertStringToByte(_appSettings.SendGridKey));

        }
        public async Task<bool> SendMail_SendGrid(string mailTo, string subject, string body, string displayName)
        {
            if (!mailTo.IsValidEmail())
            {
                throw new ApiGenericException(GenericStrings.InvalidEmail);

            }
            bool isSent = false;

            try
            {

                var client = new SendGridClient(SendGridAPIKEY);

                string[] Emails = mailTo.Split(',');
                int le = Emails.Length;
                var from = new EmailAddress(_appSettings.EmailFrom, displayName);
                var to = new EmailAddress(mailTo, "");
          
                for (int i = 0; i <= le - 1; i++)
                {


                    if (Emails[i].Trim().IsValidEmail())
                    {

                        var email = new EmailAddress(Emails[i].Trim(), "");
                        emailAddresses.Add(email);
                    }

                }

                if (le <= 1)
                {
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");

                    var response = client.SendEmailAsync(msg);
                    isSent = true;
                }
                else
                {
                    var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emailAddresses, subject, body, "");
                    var response = await client.SendEmailAsync(msg);
                    isSent = true;
                }


            }
            catch (Exception ex)
            {


                var errorMessage = ex.Message.ToString();

                return isSent;
            }

            return isSent;

        }

        public bool SendMail_Attachment_SendGrid(string mailTo, string subject, string body, string displayName, byte[] attachment_file, string ReceiverName)
        {
            if (!mailTo.IsValidEmail())
            {
                throw new ApiGenericException(GenericStrings.InvalidEmail);

            }
            bool isSent = false;

            try
            {

                var client = new SendGridClient(SendGridAPIKEY);


                string[] Emails = mailTo.Split(',');
                int le = Emails.Length;

                var from = new EmailAddress(_appSettings.EmailFrom, displayName);
                var to = new EmailAddress(mailTo, "");
                var plainTextContent = body;

                for (int i = 0; i <= le - 1; i++)
                {
                    if (Emails[i].Trim().IsValidEmail())
                    {


                        var email = new EmailAddress(Emails[i].Trim(), "");
                        emailAddresses.Add(email);
                    }

                }

                if (le <= 1)
                {
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, "");
                    //Attach File
                    if (attachment_file != null)
                    {

                        var Statement = new SendGrid.Helpers.Mail.Attachment()
                        {
                            Content = Convert.ToBase64String(attachment_file),
                            Type = "application/pdf",
                            Filename = subject + ".pdf",
                            Disposition = "inline",
                            ContentId = "SOA"
                        };
                        msg.AddAttachment(Statement);
                    }
                    var response = client.SendEmailAsync(msg);
                    isSent = true;
                }
                else
                {
                    var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emailAddresses, subject, plainTextContent, "");
                    //attach File
                    var response = client.SendEmailAsync(msg);
                    if (attachment_file != null)
                    {

                        var Statement = new SendGrid.Helpers.Mail.Attachment()
                        {
                            Content = Convert.ToBase64String(attachment_file),
                            Type = "application/pdf",
                            Filename = subject + ".pdf",
                            Disposition = "inline",
                            ContentId = "SOA"
                        };
                        msg.AddAttachment(Statement);
                    }
                    isSent = true;
                }


            }
            catch (Exception ex)
            {



                return isSent;
            }

            return isSent;

            throw new NotImplementedException();
        }

        public bool IsValidKeys(string XKey)
        {
            if(string.IsNullOrEmpty(XKey)) return false;
            var apiKey = _appSettings.APIKEY;
            if(XKey ==apiKey ) return true;
            return false;
            
        }
    }
}
