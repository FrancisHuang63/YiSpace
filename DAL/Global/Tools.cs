using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    public class Tools
    {
        public static string GetSHA256(string inputString)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(inputString);
            byte[] crypto = sha256.ComputeHash(source);
            string result = Convert.ToBase64String(crypto);
            return result;
        }

        public static long GetNewSN()
        {
            string tmpSN = DateTime.Now.ToString("yyMMddHHmmssfff");
            return long.Parse(tmpSN);
        }
    }
}
