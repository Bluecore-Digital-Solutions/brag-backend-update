using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Brag.SharedServices.Helpers
{
    public class Validations
    {
        public static bool IsValidEmail(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            bool isPhoneNo = false;

            isPhoneNo = Regex.Match(phoneNumber, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$").Success;

            return isPhoneNo;
        }

        public static bool IsValidNumber(string text)
        {
            bool isNumber = Regex.IsMatch(text, @"^[1-9]\d{0,2}(\.\d{3})*(,\d+)?$", RegexOptions.IgnoreCase);
            return isNumber;
        }
        
    }
}
