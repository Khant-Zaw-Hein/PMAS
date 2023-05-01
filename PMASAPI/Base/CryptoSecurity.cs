using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PMASAPI.Base
{
    public class CryptoSecurity
    {
        private static StringBuilder errorMessages;

        public static StringBuilder ErrorMessages
        {
            get { return errorMessages; }
            set { errorMessages = value; }
        }

        public CryptoSecurity()
        {
            ErrorMessages = new StringBuilder();
        }

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

        public static string Decrypt(byte[] SymmetricKey, byte[] Vector, string CipherText)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = 256;

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] KeyArrBytes32Value = new byte[32];
            Array.Copy(SymmetricKey, KeyArrBytes32Value, 32);

            byte[] IVBytes16Value = new byte[16];
            Array.Copy(Vector, IVBytes16Value, 16);

            aes.Key = KeyArrBytes32Value;
            aes.IV = IVBytes16Value;

            ICryptoTransform decrypto = aes.CreateDecryptor();

            byte[] encryptedBytes = Convert.FromBase64CharArray(CipherText.ToCharArray(), 0, CipherText.Length);
            byte[] decryptedData = decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return ASCIIEncoding.UTF8.GetString(decryptedData);
        }

        public static string Encrypt(byte[] SymmetricKey, byte[] Vector, string PlainText)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = 256;

            // It is equal in java 
            /// Cipher _Cipher = Cipher.getInstance("AES/CBC/PKCS5PADDING");    
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] KeyArrBytes32Value = new byte[32];
            Array.Copy(SymmetricKey, KeyArrBytes32Value, 32);

            byte[] IVBytes16Value = new byte[16];
            Array.Copy(Vector, IVBytes16Value, 16);

            aes.Key = KeyArrBytes32Value;
            aes.IV = IVBytes16Value;

            ICryptoTransform encrypto = aes.CreateEncryptor();

            byte[] plainTextByte = ASCIIEncoding.UTF8.GetBytes(PlainText);
            byte[] CipherText = encrypto.TransformFinalBlock(plainTextByte, 0, plainTextByte.Length);
            return Convert.ToBase64String(CipherText);
        }

        /// <summary>
        /// SHA 2 (512) 
        /// Salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string HashCode(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA512Managed();
            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return Convert.ToBase64String(algorithm.ComputeHash(plainTextWithSaltBytes));
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static byte[] GetSaltKey(int length)
        {
            byte[] data = new byte[length];
            using (RNGCryptoServiceProvider _RNGCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                _RNGCryptoServiceProvider.GetBytes(data);
            }
            return data;
        }
    }
}
