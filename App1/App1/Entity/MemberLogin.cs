using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Constant;

namespace App1.Entity
{
    class MemberLogin
    {
        public string email { get; set; }
        public string password { get; set; }

        public Dictionary<string,string> errors = new Dictionary<string, string>();

        public Dictionary<string, string> Validate()
        {
            RegexCheck regexCheck = new RegexCheck();
            //Email:
            if (string.IsNullOrEmpty(email))
            {
                errors.Add("Email", "Email is required!");
            }
            else if (!regexCheck.MailCheck(email))
            {
                errors.Add("Email", "Email is not valid!");
            }
            //password:
            if (string.IsNullOrEmpty(password))
            {
                errors.Add("Password", "Password is required!");
            }

            return errors;
        }
    }
}
