using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application_Layer.Validation
{
    internal class clsValidation
    {
       
            public static bool ValidateEmail(string emailAddress)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(emailAddress);
                    return addr.Address == emailAddress;
                }
                catch
                {
                    return false;
                }
            }

            public static bool ValidatePassword(string password)
            {
                if (string.IsNullOrWhiteSpace(password))
                    return false;

                return Regex.IsMatch(password,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$");
            }
        
    }
    
}
