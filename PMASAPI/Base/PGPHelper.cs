using PgpCore;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMASAPI.Base
{
    public class PGPHelper
    {
        public static async Task<string> PGP_EncryptString(string plainText, string publicKey, string privateKey, string passPhrase)
        {
            string _encryptedString = string.Empty;

            EncryptionKeys encryptionKeys = new EncryptionKeys(publicKey, privateKey, passPhrase);
            PGP pgp = new PGP(encryptionKeys);
            _encryptedString = await pgp.EncryptArmoredStringAndSignAsync(plainText);

            return _encryptedString;
        }

        public static async Task<string> PGP_DecryptString(string decryptedString, string publicKey, string privateKey, string passPhrase)
        {
            string _PlainString = string.Empty; 
            EncryptionKeys encryptionKeys = new EncryptionKeys(publicKey, privateKey, passPhrase);
            PGP pgp = new PGP(encryptionKeys);
            _PlainString = await pgp.DecryptArmoredStringAndVerifyAsync(decryptedString);

            return _PlainString;
        }

        public static async Task<string> PGP_DataEncrypt(string plainText, PMASEntities _entities,
            PMAS_Backend_KeyServerEntities _KeyServerContext)
        {

            string web_public_key = string.Empty;
            string api_private_key = string.Empty;
            string api_passphase = string.Empty;
            var data_web = _entities.tbl_CryptoKey.Where(x => x.KeyName == "Web.PGP.Backend.Public"
            && x.ValidFrom <= DateTime.Now && x.ValidTo >= DateTime.Now).FirstOrDefault();
            if (data_web != null)
            {
                web_public_key = data_web.KeyValue;
            }
            var data_api = _entities.tbl_CryptoKey.Where(x => x.KeyName == "WebAPI.PGP.Database.Private"
            && x.ValidFrom <= DateTime.Now && x.ValidTo >= DateTime.Now).FirstOrDefault();
            if (data_api != null)
            {
                api_private_key = data_api.KeyValue;
            }

            var data_keyphase = _KeyServerContext.tbl_CryptoSecret.Where(x => x.KeyName == "WebAPI.PGP.Database.Private"
            && x.ValidFrom <= DateTime.Now && x.ValidTo >= DateTime.Now).FirstOrDefault();
            if (data_keyphase != null)
            {
                api_passphase = data_keyphase.KeyPassphrase;
            }
            string encryptString = string.Empty;
            encryptString = await PGP_EncryptString(plainText, web_public_key, api_private_key, api_passphase);

            return encryptString;

        }
        public static async Task<string> PGP_DataDecrypt(string encryptString, PMASEntities _entities,
            PMAS_Backend_KeyServerEntities _KeyServerContext)
        {

            string web_public_key = string.Empty;
            string api_private_key = string.Empty;
            string api_passphase = string.Empty;
            var data_web = _entities.tbl_CryptoKey.Where(x => x.KeyName == "Web.PGP.Backend.Public"
            && x.ValidFrom <= DateTime.Now && x.ValidTo >= DateTime.Now).FirstOrDefault();
            if (data_web != null)
            {
                web_public_key = data_web.KeyValue;
            }
            var data_api = _entities.tbl_CryptoKey.Where(x => x.KeyName == "WebAPI.PGP.Database.Private"
            && x.ValidFrom <= DateTime.Now && x.ValidTo >= DateTime.Now).FirstOrDefault();
            if (data_api != null)
            {
                api_private_key = data_api.KeyValue;
            }

            var data_keyphase = _KeyServerContext.tbl_CryptoSecret.Where(x => x.KeyName == "WebAPI.PGP.Database.Private"
            && x.ValidFrom <= DateTime.Now && x.ValidTo >= DateTime.Now).FirstOrDefault();
            if (data_keyphase != null)
            {
                api_passphase = data_keyphase.KeyPassphrase;
            }
            string decryptString = await PGP_DecryptString(encryptString, web_public_key, api_private_key, api_passphase);

            return decryptString;

        }
    }
}
