using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App1.Constant
{
    class RegexCheck
    {
        public static string emailPattern = @" ^ ([\w\.\-] +)@([\w\-]+)((\.(\w){2,3})+)$";
        public static string phonePattern = @"^[0-9]+$";

        public Boolean CheckPattern(string st, string Pattern)
        {
            Regex rgx = new Regex(Pattern);
            if (rgx.IsMatch(st))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean MailCheck(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
