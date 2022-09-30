using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EM.InsurePlus.Services.Contacts;

namespace EM.InsurePlus.Services
{
    public class HelperService : IHelperService
    {
        public bool CheckEmail(string value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
