using System;
using System.Collections.Generic;
using System.Text;

namespace Brag.Domain.Model.Configuration
{
    public class AppSettings
    {
        public readonly string APIKEY;

        //Define your AppSetting keys here 
        public string E_Key { get; set; }
        public string E_IV { get; set; }
        public string EfcKey { get; set; }
        public string SendGridKey { get; set; }
        public string WhatsAppKey { get; set; }
        public string WhatsAppUri { get; set; }
        public string EmailFrom { get; set; }
        public string MailDisplayName { get; set; }
        public string JwtKey { get; set; }
        public string JwtAudience { get; set; }
        public string IV { get; set; }
        public string CloudinaryApiKey { get; set; }
        public string CloudinarySecreteKey { get; set; }
        public string CloudinaryUsername { get; set; }




    }
    public class PaymentConfig
    {
        public string Pay_SecretKey { get; set; }
        public string Pay_publicKey { get; set; }
        public string InitiateTransaction { get; set; }
        public string VerifyTransactionUrl { get; set; }
        public string ListTransaction { get; set; }
        public string ChargeAuthorization { get; set; }
        public string TransactionTotals { get; set; }
        public string ListBanks { get; set; }
    }
    //public class Urls //From AppSettingd
    //{
    //    public string ForgetPassWordUrl { get; set; }
    //}
}
