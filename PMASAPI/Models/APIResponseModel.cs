using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Models
{
    public class RequestPublicKeyModel
    {
        public string APIHandshakeKey { get; set; }
    }
    public class APIResponseModel
    {

        public bool Success => Errors == null || Errors.Count == 0;

        public List<string> Errors { get; set; }


        public void AddError(string error)
        {
            if (Errors == null)
                Errors = new List<string>();

            Errors.Add(error);
        }
    }

    public class APIResponseModel<T> : APIResponseModel
         where T : new()
    {
        public T Response { get; set; }

    }
    public class APIPGPResponseModel
    {
        public string ResponsePGPEncryptString { get; set; }
        public int TotalCount { get; set; }
    }
    public class APIRequestModel
    {
        public string HandShakeToken { get; set; }
        public string RequestPGPEncryptString { get; set; }
    }
    public class IDModel
    {
        public Guid ID { get; set; }
    }
}