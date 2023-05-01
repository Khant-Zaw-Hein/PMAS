using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Base
{
    public class CryptoSecurity
    {
        public static string getHashData(string reqeustHushString)
        {
            System.Security.Cryptography.SHA512 sha512 = new System.Security.Cryptography.SHA512Managed();

            byte[] sha512Bytes = System.Text.Encoding.Default.GetBytes(reqeustHushString);

            byte[] cryString = sha512.ComputeHash(sha512Bytes);

            string hashedPwd = string.Empty;

            for (int i = 0; i < cryString.Length; i++)
            {
                hashedPwd += cryString[i].ToString("X2");
            }

            return hashedPwd;
        }
    }
}