using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Net;
using System.DirectoryServices;
using System.Net.Mail;
using System.Text;
using System.Data;
using System.Configuration;
using System.Security;
using System.Web.Services;
using System.Web.Script.Services;

namespace quickinfo_v2.CommonCLS
{
    public class CommonFunctions
    {
        public string getCurrentUserCode( String UserName)
        {
           
            String UserCode;



            try
            {
                if (Left(UserName, 4) == "HNBA")
                {
                    UserCode = Right(UserName, (UserName.Length) - 5);

                }
                else
                {
                    UserCode = Right(UserName, (UserName.Length) - 7);

                }

                return UserCode;
            }
            catch (Exception ee)
            {
                return "";
            }
        }

        public string Left(string text, int length)
        {
            return text.Substring(0, length);
        }

        public string Right(string text, int length)
        {
            return text.Substring(text.Length - length, length);
        }

        public string Mid(string text, int start, int end)
        {
            return text.Substring(start, end);
        }

        public string Mid(string text, int start)
        {
            return text.Substring(start, text.Length - start);
        }
    }
}