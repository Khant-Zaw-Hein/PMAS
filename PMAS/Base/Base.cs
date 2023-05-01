using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Base
{
    public static class APIURL
    {
        //main
        public static string MainApiDomain = "http://localhost:61518/";

        public static string GetRequestAuthCode = "api/Authenticate/GetRequestAuthCode";

        //login
        public static string login = "api/Login/Login";

        //useraccount
        public static string GetUserAccountData = "api/UserAccount/GetUserAccountData";
        public static string GetUserAccountById = "api/UserAccount/GetUserAccountById";
        public static string UserAccountEdit = "api/UserAccount/UserAccountEdit";
        public static string UserAccountCreate = "api/UserAccount/UserAccountCreate";
        public static string UserAccountDelete = "api/UserAccount/UserAccountDelete";
        //Department
        public static string GetDepartmentData = "api/Department/GetDepartmentData";
        //Designation
        public static string GetDesignationData = "api/Designation/GetDesignationData";
        //rank
        public static string GetRankData = "api/Rank/GetRankData";
        //role
        public static string GetRoleData = "api/Role/GetRoleData";

        //userrole
        public static string GetUserRoleData = "api/UserRole/GetUserRoleData";
        public static string GetUserRoleById = "api/UserRole/GetUserRoleById";
        public static string UserRoleEdit = "api/UserRole/UserRoleEdit";
        public static string UserRoleCreate = "api/UserRole/UserRoleCreate";
        public static string UserRoleDelete = "api/UserRole/UserRoleDelete";

        //menu

        public static string GetMenuData = "api/Menu/GetMenuData";
        public static string GetMenuById = "api/Menu/GetMenuById";
        public static string GetMenuByLevel = "api/Menu/GetMenuByLevel";
        public static string MenuEdit = "api/Menu/MenuEdit";
        public static string MenuCreate = "api/Menu/MenuCreate";
        public static string MenuDelete = "api/Menu/MenuDelete";

        //program
        public static string GetMenuNProgramData = "api/Program/GetMenuNProgramData";
        public static string GetParentProgram = "api/Program/GetParentProgram";
        public static string GetProgramData = "api/Program/GetProgramData";
        public static string GetProgramById = "api/Program/GetProgramById";
        public static string ProgramEdit = "api/Program/ProgramEdit";
        public static string ProgramCreate = "api/Program/ProgramCreate";
        public static string ProgramDelete = "api/Program/ProgramDelete";

        //code table
        public static string CreateCodeTable = "api/CodeTable/CreateCodeTable";
        public static string UpdateCodeTable = "api/CodeTable/UpdateCodeTable";
        public static string DeleteCodeTable = "api/CodeTable/DeleteCodeTable";
        public static string GetCodeTableData = "api/CodeTable/GetCodeTableData";
        public static string GetCodeTableById = "api/CodeTable/GetCodeTableById";

    }
    public class ErrorStatusCode
    {
        public static int SessionTimeOut = 101;
        public static int ApiRequestFail = 102;
        public static int AuthenticationFailed = 103;
        public static int SomethingWentWrong = 104;

    }
    public static class PasswordChangeType
    {
        public static string ForcedChange = "ForcedChange";
        public static string SelfChange = "SelfChange";
        public static string AdminChange = "AdminChange";
    }

    public class APIDataSendModel
    {
        public string RequestPGPEncryptString { get; set; }
        public string HandShakeToken { get; set; }
    }

    public class SearchFilterModel
    {
        public string keyword { get; set; }
    }

    public class RequestPublicKeyModel
    {
        public string APIHandshakeKey { get; set; }
    }
    public class IDModel
    {
        public Guid ID { get; set; }
    }
}