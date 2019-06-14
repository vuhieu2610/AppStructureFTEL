/*
 *  **********************************************************************************
 Company: FTEL
 Author: Lương Đức Nguyên
 Create Date: 12/12/2017
 Purpose: Cung cấp hàm xử lý xác thực qua Active Directory
 *  **********************************************************************************  
*/

using System;
using System.DirectoryServices;

namespace FTEL.Common.Utilities
{
    public class ADHelper
    {
        public ADHelper()
        {

        }
        /// <summary>
        /// Xác thực người dùng thông qua userName/password được cung cấp bởi hệ thống Active Directory
        /// Sử dụng cơ chế gọi Asynchronuos
        /// </summary>
        /// <param name="userName">AD UserName</param>
        /// <param name="password">AD Password</param>
        /// <param name="ldapDomain">LDAP Domain</param>
        /// <returns>True/False</returns>
        public /*async*/ static bool Authenticate(string userName, string password, string ldapDomain)
        {
            var authentic = false;
            DirectoryEntry de = null;
            DirectorySearcher dsearch = null;
            //DirectoryEntry entry = new DirectoryEntry(ldapDomain, userName, password,AuthenticationTypes.Secure);
            //object nativeObject = entry.NativeObject;
            //authentic = true;
            try
            {
                
                de = new DirectoryEntry(ldapDomain, userName, password, AuthenticationTypes.Secure);
                dsearch = new DirectorySearcher(de)
                {
                    ClientTimeout = new TimeSpan(0, 0, 30),
                    SearchScope = SearchScope.Subtree
                };
                //dsearch.PropertiesToLoad.Add("cn");
                var sr = dsearch.FindOne();
                var deReturn = sr.GetDirectoryEntry();
                authentic = true;

                var sPropertyName = string.Empty;
                foreach (string s in deReturn.Properties.PropertyNames)
                {
                    Libs.WriteLog("[AD test] Property Value : ", deReturn.Properties[s].Value.ToString());

                }
                Libs.WriteLog("[AD test] Property Value : ", deReturn.Properties["displayname"].Value.ToString());

            }
            catch (Exception ex)
            {
                //Libs.WriteLog("vv", ex.ToString());
                throw;
            }
            finally
            {
                if (de != null)
                    de.Dispose();
                if (dsearch != null)
                    dsearch.Dispose();
            }
            return true;
        }

        public static bool AuthenticateUser(string domain, string username, string password, string ldapPath, out string errmsg)
        {
            errmsg = "";           
            var domainAndUsername = domain + @"\" + username;
            var entry = new DirectoryEntry(ldapPath, domainAndUsername, password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                //Object obj = entry.NativeObject;
                var search = new DirectorySearcher(entry);

                //search.Filter = "(SAMAccountName=" + username + ")";
                //search.PropertiesToLoad.Add("cn");                               
                var result = search.FindOne();

                if (result != null)
                {                   
                    //var _filterAttribute = (String)result.Properties["cn"][0];
                    //Libs.WriteLog("xxx", _filterAttribute);
                    return true;
                }
                // Update the new path to the user in the directory
                //ldapPath = result.Path;                
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
                //Libs.WriteLog("ee", errmsg);
                return false;
            }
            return false;
        }

    }
}
