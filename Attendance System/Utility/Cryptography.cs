using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Attendance_System.Utility
{
    public class Cryptography
    {
        public static String getMD5(String str)
        {
            var data = Encoding.ASCII.GetBytes(str);
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            var hash = Convert.ToBase64String(md5data);
            return hash;
        }

        public static Boolean matchHash(String hash, String obj)
        {
            if (hash == getMD5(obj))
            {
                return true;
            }
            else
                return false;
        }

    }
}