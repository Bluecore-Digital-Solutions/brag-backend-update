using System;
using System.Collections.Generic;
using System.Text;

namespace Brag.SharedServices.Statics
{
    public class GenericStrings
    {
        public static readonly string InvalidEmail = "Invalid email address.";
        public static readonly string InvalidPhoneNumber = "Invalid phone number";
        public static readonly string InvalidPhoneNumberOrEmail = "Invalid phone number or email address";
        public static readonly string InvalidNumber = "Invalid number. The number's value should be whole numbers (digits)";
        public static readonly string SendVerificationSuccessful = "Sendig verification code successful";
        public static readonly string SendVerificationFailed = "Sendig verification code failed";

        public static readonly string InvalidEmailOrPhoneNumber = "Invalid Email or Phone number. Check it and try again";
        public static readonly string InvalidLoginCredentials = "Invalid login credentials";

        public static readonly string IdentityNotVerified = "Your identity is not verified";

        public static readonly string DeviceOrBrowserChange = "$Your device or browser has changed.Kindly verify new device or browser";
        public static readonly string InvalidToken = "$Invalid tokens ! Token expired or invalid token credentials";

        public static readonly string AccountLockOut = "";

        public static readonly string InValidCode = "Invalid verification credential";
        public static readonly string SomethingWrong = "Something went wrong.Try it again";

        public static string InvalidCourseType = "Invalid Course Type";

        public static string InvalidUserRequest = "Unathorized user. Contact support team";
        // await _appConfig.SendEmailAsync(user.Email, null,null, EmailFormatter.ResetPassword(uetails.FirstName, callback/*resetUrl.OriginalString*/));
    }
    public class StringsDictionary
    {

    }
}
