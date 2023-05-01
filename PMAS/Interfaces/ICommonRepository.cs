using PMAS.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Interfaces
{
    public interface ICommonRepository
    {
        RestResponse CommonApiCall(string apiurl, string encryptString, string handshakeToken, bool isCloud);
        string GetAuthenticationCode(ref string returnMessage, bool isCloud, PMAS_Backend_WebEntities _context);
    }
}