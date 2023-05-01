using PgpCore;
using PMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Base
{
    public class PGPHelper
    {
        public static string PGP_EncryptString(string plainText, string publicKey, string privateKey, string passPhrase)
        {
            string _encryptedString = string.Empty;

            EncryptionKeys encryptionKeys = new EncryptionKeys(publicKey, privateKey, passPhrase);
            PGP pgp = new PGP(encryptionKeys);
            _encryptedString = pgp.EncryptArmoredStringAndSign(plainText);

            return _encryptedString;
        }

        public static string PGP_DecryptString(string decryptedString, string publicKey, string privateKey, string passPhrase)
        {
            string _PlainString = string.Empty;

            EncryptionKeys encryptionKeys = new EncryptionKeys(publicKey, privateKey, passPhrase);
            PGP pgp = new PGP(encryptionKeys);
            _PlainString = pgp.DecryptArmoredStringAndVerify(decryptedString);

            return _PlainString;

        }
        public static string PGP_DataEncryptForCloud(string plainText)
        {
            PMAS_Backend_WebEntities entities = new PMAS_Backend_WebEntities();
            string encryptString = string.Empty;
            string web_private_key = string.Empty;
            string cloud_public_key = string.Empty;
            string web_passphase = string.Empty;
            var data_cloud = entities.tbl_CryptoKey.Where(x => x.KeyName == "WebAPI.PGP.Cloud.Public").FirstOrDefault();
            if (data_cloud != null)
            {
                cloud_public_key = data_cloud.KeyValue;
            }
            var data_web = entities.tbl_CryptoKey.Where(x => x.KeyName == "Web.PGP.Backend.Private").FirstOrDefault();
            if (data_web != null)
            {
                web_private_key = data_web.KeyValue;
                web_passphase = data_web.Passphrase;
            }
            encryptString = PGP_EncryptString(plainText, cloud_public_key, web_private_key, web_passphase);

            return encryptString;

        }
        public static string PGP_DataDecryptForCloud(string encryptString)
        {
            PMAS_Backend_WebEntities entities = new PMAS_Backend_WebEntities();
            string decryptString = string.Empty;
            string web_private_key = string.Empty;
            string cloud_public_key = string.Empty;
            string web_passphase = string.Empty;
            var data_cloud = entities.tbl_CryptoKey.Where(x => x.KeyName == "WebAPI.PGP.Cloud.Public").FirstOrDefault();
            if (data_cloud != null)
            {
                cloud_public_key = data_cloud.KeyValue;
            }
            var data_web = entities.tbl_CryptoKey.Where(x => x.KeyName == "Web.PGP.Backend.Private").FirstOrDefault();
            if (data_web != null)
            {
                web_private_key = data_web.KeyValue;
                web_passphase = data_web.Passphrase;
            }


            decryptString = PGP_DecryptString(encryptString, cloud_public_key, web_private_key, web_passphase);

            return decryptString;

        }
        public static string PGP_DataEncryptForBackend(string plainText)
        {
            PMAS_Backend_WebEntities entities = new PMAS_Backend_WebEntities();
            string encryptString = string.Empty;
            string web_private_key = string.Empty;
            string backend_public_key = string.Empty;
            string web_passphase = string.Empty;
            var data_backend = entities.tbl_CryptoKey.Where(x => x.KeyName == "WebAPI.PGP.Database.Public").FirstOrDefault();
            if (data_backend != null)
            {
                backend_public_key = data_backend.KeyValue;
            }
            var data_web = entities.tbl_CryptoKey.Where(x => x.KeyName == "Web.PGP.Backend.Private").FirstOrDefault();
            if (data_web != null)
            {
                web_private_key = data_web.KeyValue;
                web_passphase = data_web.Passphrase;
            }
            encryptString = PGP_EncryptString(plainText, backend_public_key, web_private_key, web_passphase);

            return encryptString;

        }
        public static string PGP_DataDecryptForBackend(string encryptString)
        {
            PMAS_Backend_WebEntities entities = new PMAS_Backend_WebEntities();
            string decryptString = string.Empty;
            string web_private_key = string.Empty;
            string backend_public_key = string.Empty;
            string web_passphase = string.Empty;
            var data_backend = entities.tbl_CryptoKey.Where(x => x.KeyName == "WebAPI.PGP.Database.Public").FirstOrDefault();
            if (data_backend != null)
            {
                backend_public_key = data_backend.KeyValue;
            }
            var data_web = entities.tbl_CryptoKey.Where(x => x.KeyName == "Web.PGP.Backend.Private").FirstOrDefault();
            if (data_web != null)
            {
                web_private_key = data_web.KeyValue;
                web_passphase = data_web.Passphrase;
            }
            decryptString = PGP_DecryptString(encryptString, backend_public_key, web_private_key, web_passphase);
            return decryptString;
        }
    }
}