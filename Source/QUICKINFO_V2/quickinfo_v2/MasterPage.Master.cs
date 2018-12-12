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
using SignalR;
using SignalR.Infrastructure;
using SignalR.Hosting.AspNet;
using quickinfo_v2.CommonCLS;


namespace quickinfo_v2
{
    public partial class Master : System.Web.UI.MasterPage
    {
        String UserName;
        String DomainName;
        String UserBranch;
        String UserDepartment;
        String UserDisplay;
        CommonFunctions cmnFunctions;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserName = Context.User.Identity.Name;


                cmnFunctions = new CommonFunctions();


                try
                {


                    if (Left(UserName, 4) == "HNBA")
                    {
                        DomainName = cmnFunctions.Left(UserName, 4);
                        Session["DOMAIN"] = DomainName.ToString();
                        UserName = cmnFunctions.Right(UserName, (UserName.Length) - 5);
                        Session["USER_ID"] = UserName.ToString();
                        GetUser(UserName.ToString());
                    }
                    else if (Left(UserName, 5) == "HNBGI")
                    {
                        DomainName = cmnFunctions.Left(UserName, 5);
                        Session["DOMAIN"] = DomainName.ToString();
                        UserName = cmnFunctions.Right(UserName, (UserName.Length) - 6);
                        Session["USER_ID"] = UserName.ToString();
                        GetUser(UserName.ToString());
                    }
                    else
                    {
                        DomainName = cmnFunctions.Left(UserName, 5);
                        Session["DOMAIN"] = DomainName.ToString();
                        UserName = cmnFunctions.Right(UserName, (UserName.Length) - 6);
                        Session["USER_ID"] = UserName.ToString();
                        //GetUserBranch(UserName.ToString());
                    }

                    UserDisplay = getUserNameCurrentUser(UserName);
                    lblUserCode.Text = UserName;
                    lblUserName.Text = UserDisplay;
                    lblBranchCode.Text = UserBranch;

                    // chat.Attributes.Add("data-current-user", UserName);



                    //HttpCookie preWishMsgCookie = Request.Cookies["wishMsgCookie"];
                    //if (preWishMsgCookie == null)
                    //{
                    //    string msg = "";
                    //    msg = "Dear " + UserDisplay + ", Wish you a Happy Centralized New Year 2016, From Tharindu with IT Dept.";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + msg + "');", true);

                    //    HttpCookie wishMsgCookie = new HttpCookie("wishMsgCookie");
                    //    wishMsgCookie.Values["IsWished"] = "false";
                    //    wishMsgCookie.Expires = DateTime.Now.AddDays(1);
                    //    Response.Cookies.Add(wishMsgCookie);


                    //}


                }
                catch (Exception ee)
                {

                }
                BuildMenu();
                LoadChatList();
                createChatPopupFunction(UserName);
            }
        }
        public DirectoryEntry GetDirectoryObject()
        {
            DirectoryEntry oDE;
            if (DomainName == "HNBGI")
            {
                oDE = new DirectoryEntry("LDAP://192.168.10.211");
            }
            else
            {
                oDE = new DirectoryEntry("LDAP://192.168.10.251");
            }

            return oDE;
        }

        public DirectoryEntry GetUser(string UserName)
        {
            DirectoryEntry de = GetDirectoryObject();
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = de;

            deSearch.Filter = "(&(objectClass=user)(SAMAccountName=" + UserName + "))";
            deSearch.SearchScope = SearchScope.Subtree;
            SearchResult results = deSearch.FindOne();


            if (!(results == null))
            {

                de = new DirectoryEntry(results.Path);
                //hdnEmployeeEPF.Value = de.Properties["EmployeeID"][0].ToString();
                //// hdnDisplayName.Value = de.Properties["displayName"][0].ToString();
                //hdnBranch.Value = de.Properties["postalCode"].Value.ToString();
                //hdnUserCode.Value = UserName;



                Session["UserEPF"] = de.Properties["EmployeeID"][0].ToString();
                Session["UserCode"] = UserName;
                Session["UserBranch"] = de.Properties["postalCode"].Value.ToString();
                

                
                UserBranch = de.Properties["postalCode"].Value.ToString();

                string aaa = de.Properties["EmployeeID"][0].ToString();
                string aaba = UserName;
                string aanna = de.Properties["postalCode"].Value.ToString();





                HttpCookie aCookie = new HttpCookie("userInfo");
                aCookie.Values["UserEPF"] = de.Properties["EmployeeID"][0].ToString();
                aCookie.Values["UserCode"] = UserName;
                aCookie.Values["UserBranch"] = de.Properties["postalCode"].Value.ToString();
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);


                return de;
            }
            else
            {
                Session["UserEPF"] = "";
                Session["UserCode"] = "";
                Session["UserBranch"] = "";
                return null;
            }
        }



        private void createChatPopupFunction(string currentUserCode)
        {
            string script = "";
            //script = " <script type=\"text/javascript\">" +
            //           "  $(document).ready(function () { " +
            //           " var hub = $.connection.ChatHub; hub.showMessage = function (message, from, to, id) { " +
            //            " if(to=='" + currentUserCode + "') {" +
            //            " neonChat.showChat(true); " +
            //            "neonChat.pushMessage(id, message, from, new Date(), true, true); " +
            //             " neonChat.renderMessages(id, false); " +
            //             "}"+
            //             " }; " +
            //             "  $.connection.hub.start(); " +
            //             " }); " +
            //            " </script> ";


            //script = " <script type=\"text/javascript\">" +
            //             "      $(document).ready(function () { " +
            //             "          var hub = $.connection.ChatHub; hub.showMessage = function (message, from, to, id) { " +
            //             "              var searchStr=from.replace('.','');" +
            //             "              var receiverID='';" +
            //             "              var inputs = document.getElementsByTagName(\"a\"); " +
            //             "              for(var i=0; i<inputs.length; i++) { " +
            //             "                  if(inputs[i].getAttribute(\"data-conversation-history\")==('#'+searchStr)){ " +
            //             "                        receiverID=inputs[i].id; " +
            //             "                        break; " +
            //             "                   } " +
            //             "              } " +
            //             "              if(to=='" + currentUserCode + "') {" +
            //             "                  neonChat.showChat(true); " +
            //             "                  neonChat.pushMessage(receiverID, message, from, new Date(), true, true); " +
            //             "                  neonChat.renderMessages(receiverID, false); " +
            //             "              }" +
            //             "          }; " +
            //             "      $.connection.hub.start(); " +
            //             "      });" +
            //             " </script> ";



            ltrlChatPopupFunction.Text = script;
        }
        private void LoadChatList()
        {
            ltrlOnlineUsers.Text = getOnlineUserList(UserName);
        }


        private void BuildMenu()
        {
            StringBuilder menuHTMLCode = new StringBuilder();


            //For test purpose
            //menuHTMLCode.Append("<nav>");
            //menuHTMLCode.Append("<ul id=\"nav\">");


            //End For test purpose

            /////////////////////////////////////////
            try
            {
                DataTable dtMainMenu = new DataTable();
                DataTable dtSubMenu = new DataTable();
                dtMainMenu = FillMainMenuTable();
                dtSubMenu = FillSubMenuTable();
                DataSet ds = new DataSet();
                ds.Tables.Add(dtMainMenu);
                ds.Tables.Add(dtSubMenu);
                ds.Relations.Add("subMenus", dtMainMenu.Columns["MAIN_MENU_CODE"], dtSubMenu.Columns["MAIN_MENU_CODE"]);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow masterRow in ds.Tables[0].Rows)
                    {
                        if (masterRow.GetChildRows("subMenus").Length > 0)
                        {
                            //  menuHTMLCode.Append(" <ul id=\"main-menu\" class=\"\">");
                            menuHTMLCode.Append("<li><a href=\"#\"><i class=\"entypo-layout\"></i><span>" + (string)masterRow["MAIN_MENU_NAME"] + "</span></a>");
                            menuHTMLCode.Append("<ul>");
                            foreach (DataRow childRow in masterRow.GetChildRows("subMenus"))
                            {
                                //menuHTMLCode.Append("<li><a href=\"" + Page.ResolveUrl((string)childRow["PAGE_PATH"]) + "\">" + (string)childRow["SUB_MENU_NAME"] + "</a></li>");
                                menuHTMLCode.Append("<li><a href=\"" + Page.ResolveUrl((string)childRow["PAGE_PATH"]) + "?pagecode=" + childRow["SUB_MENU_CODE"].ToString() + "\">" + (string)childRow["SUB_MENU_NAME"] + "</a></li>");
                            }
                            menuHTMLCode.Append("</ul>");
                            menuHTMLCode.Append("</li>");
                            //   menuHTMLCode.Append("</ul>");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // throw new Exception("Unable to populate treeview" + ex.Message);

            }

            //////////////////////////////////////




            //menuHTMLCode.Append("</ul>");
            //menuHTMLCode.Append("</nav>");



            ltrlMenu.Text = menuHTMLCode.ToString();

        }

        private void UserDetails(String EPFNumber)
        {
            System.DirectoryServices.DirectoryEntry dirEntry;
            System.DirectoryServices.DirectorySearcher dirSearcher;
            // dirEntry = new System.DirectoryServices.DirectoryEntry("LDAP://192.168.10.251");


            // DirectoryEntry dirEntry;
            if (DomainName == "HNBGI")
            {
                dirEntry = new DirectoryEntry("LDAP://192.168.10.211");
            }
            else
            {
                dirEntry = new DirectoryEntry("LDAP://192.168.10.251");
            }




            dirSearcher = new System.DirectoryServices.DirectorySearcher(dirEntry);
            dirSearcher.Filter = "(&(objectClass=user)(SAMAccountName=" + EPFNumber + "))";


            SearchResult sr = dirSearcher.FindOne();

            System.DirectoryServices.DirectoryEntry de = sr.GetDirectoryEntry();

            if (sr == null)
            {
                UserBranch = "";
                UserDepartment = "";
                UserDisplay = "";
            }
            else
            {

                UserBranch = de.Properties["postalCode"].Value.ToString();
                UserDepartment = de.Properties["Department"].Value.ToString();
                UserDisplay = de.Properties["displayname"].Value.ToString();
            }
        }



        private DataTable FillMainMenuTable()
        {
            DataTable dtMainMenu = new DataTable();

            dtMainMenu.Columns.Add("MAIN_MENU_CODE", Type.GetType("System.Int32"));
            dtMainMenu.Columns.Add("MAIN_MENU_NAME", Type.GetType("System.String"));

            DataRow drMainMenu;


            ////
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            // selectQuery = "SELECT MAIN_MENU_CODE,MAIN_MENU_NAME FROM WF_ADMIN_MAIN_MENU ORDER BY MAIN_MENU_NAME";

            selectQuery = "SELECT MAIN_MENU_CODE,MAIN_MENU_NAME FROM WF_ADMIN_MAIN_MENU WHERE MAIN_MENU_CODE IN (2,27,28,29) ORDER BY MAIN_MENU_NAME";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    drMainMenu = dtMainMenu.NewRow();
                    drMainMenu[0] = Convert.ToInt32(dr[0].ToString());
                    drMainMenu[1] = dr[1].ToString();
                    dtMainMenu.Rows.Add(drMainMenu);

                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();


            ////

            return dtMainMenu;
        }


        private DataTable FillSubMenuTable()
        {
            DataTable dtSubMenu = new DataTable();

            dtSubMenu.Columns.Add("SUB_MENU_CODE", Type.GetType("System.Int32"));
            dtSubMenu.Columns.Add("MAIN_MENU_CODE", Type.GetType("System.Int32"));
            dtSubMenu.Columns.Add("SUB_MENU_NAME", Type.GetType("System.String"));
            dtSubMenu.Columns.Add("PAGE_PATH", Type.GetType("System.String"));


            DataRow drSubMenu;

            ////

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            //selectQuery = "SELECT SB.SUB_MENU_CODE,SB.MAIN_MENU_CODE,SB.SUB_MENU_NAME,SB.PAGE_PATH FROM WF_ADMIN_SUB_MENU SB "+
            //    " ORDER BY SB.SUB_MENU_CODE,SB.SUB_MENU_NAME";


            int userRoleCode = 0;

            userRoleCode = getUserRoleCodeOfCurrentUser(UserName);

            if (userRoleCode != 0)
            {

                //selectQuery = "SELECT SB.SUB_MENU_CODE,SB.MAIN_MENU_CODE,SB.SUB_MENU_NAME,SB.PAGE_PATH FROM WF_ADMIN_SUB_MENU SB " +
                //            " INNER JOIN WF_ADMIN_USEROLE_PRIVILEGE T ON SB.MAIN_MENU_CODE=T.MAIN_MENU_CODE AND SB.SUB_MENU_CODE=T.SUB_MENU_CODE " +
                //            " WHERE  T.USER_ROLE_CODE='" + userRoleCode + "' AND T.PRIVILEGE_GIVEN=1 " +
                //    " ORDER BY SB.SUB_MENU_NAME";


                selectQuery = "SELECT SB.SUB_MENU_CODE,SB.MAIN_MENU_CODE,SB.SUB_MENU_NAME,SB.PAGE_PATH FROM WF_ADMIN_SUB_MENU SB " +
                            " INNER JOIN WF_ADMIN_USEROLE_PRIVILEGE T ON SB.MAIN_MENU_CODE=T.MAIN_MENU_CODE AND SB.SUB_MENU_CODE=T.SUB_MENU_CODE " +
                            " WHERE  SB.MAIN_MENU_CODE IN (2,27,28,29) AND  T.USER_ROLE_CODE='" + userRoleCode + "' AND T.PRIVILEGE_GIVEN=1 " +
                    " ORDER BY SB.SUB_MENU_NAME";


                cmd.CommandText = selectQuery;

                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        drSubMenu = dtSubMenu.NewRow();
                        drSubMenu[0] = Convert.ToInt32(dr[0].ToString());
                        drSubMenu[1] = Convert.ToInt32(dr[1].ToString());
                        drSubMenu[2] = dr[2].ToString();
                        drSubMenu[3] = dr[3].ToString();
                        dtSubMenu.Rows.Add(drSubMenu);
                    }
                }
                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }

            //

            return dtSubMenu;
        }
        private int getUserRoleCodeOfCurrentUser(string UserName)
        {
            int userRoleCode = 0;

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT USER_ROLE_CODE FROM WF_ADMIN_USERS WHERE STATUS=1 AND USER_CODE='" + UserName + "'";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                userRoleCode = Convert.ToInt32(dr[0].ToString());
                //Response.Cookies.Add(USERROLE);
                //Response.Cookies["USERROLE"].Value = Convert.ToString(userRoleCode);

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return userRoleCode;

        }

        private string getUserNameCurrentUser(string UserName)
        {
            string userName = "";

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT USER_NAME FROM WF_ADMIN_USERS WHERE  USER_CODE='" + UserName + "'";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                userName = dr[0].ToString();

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return userName;

        }



        private string getOnlineUserList(string currentUserCode)
        {
            string OnlineUserList = "";

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT u.user_name,u.user_code FROM QUICKINFO_ONLINE_USERS QOU  INNER JOIN WF_ADMIN_USERS U ON QOU.USER_CODE=U.USER_CODE ";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {


                while (dr.Read())
                {
                    if (currentUserCode != dr[1].ToString())
                    {
                        string id = "";
                        id = dr[1].ToString().Replace(".", "");
                        OnlineUserList = OnlineUserList + "<a href=\"#\"  data-conversation-history=\"#" + id + "\"><span class=\"user-status is-online\"></span><em >" + dr[1].ToString() + "</em></a>";


                        ltrlChatHistory.Text = ltrlChatHistory.Text + getChatHistory(id, currentUserCode, dr[1].ToString());
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return OnlineUserList;

        }

        private string getChatHistory(string id, string currentUserCode, string chatWithUser)
        {
            string chatHistory = "";

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT QC.MESSAGE,QC.SENT_DATE,U.USER_NAME,UU.USER_NAME,QC.IS_READ,QC.MSG_FROM,QC.MSG_TO FROM QUICKINFO_CHAT QC " +
                          "INNER JOIN WF_ADMIN_USERS U ON QC.MSG_FROM=U.USER_CODE " +
                          "INNER JOIN WF_ADMIN_USERS UU ON QC.MSG_TO=UU.USER_CODE " +
                          "WHERE QC.MSG_FROM IN ('" + currentUserCode + "','" + chatWithUser + "') AND QC.MSG_TO IN ('" + currentUserCode + "','" + chatWithUser + "')  " +
                          " ORDER BY QC.SENT_DATE ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();

            chatHistory = " <ul class=\"chat-history\" id=\"" + id + "\">";

            if (dr.HasRows)
            {


                while (dr.Read())
                {
                    if (dr[6].ToString() == currentUserCode && dr[4].ToString() == "0")
                    {
                        chatHistory = chatHistory + "   <li class=\"odd unread\">";

                    }
                    else if (dr[6].ToString() == currentUserCode && dr[4].ToString() == "1")
                    {
                        chatHistory = chatHistory + "   <li>";
                    }
                    else
                    {
                        chatHistory = chatHistory + "   <li>";

                    }

                    chatHistory = chatHistory + " <span class=\"user\">" + dr[2].ToString() + "</span> ";
                    chatHistory = chatHistory + " <p>" + dr[0].ToString() + "</p>";
                    chatHistory = chatHistory + " <span class=\"time\">" + dr[1].ToString() + "</span> ";

                    chatHistory = chatHistory + "</li>";


                }
            }

            chatHistory = chatHistory + "</ul>";

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return chatHistory;

        }



        private string getUnreadMsgCount(string to)
        {
            string returnVal = "0";

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT count(QC.IS_READ) FROM QUICKINFO_CHAT QC " +
                          "WHERE  QC.MSG_TO ='" + to + "' ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                dr.Read();
                returnVal = dr[0].ToString();
            }


            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }









        [WebMethod]
        [ScriptMethod]
        public void RemoveLoggedUser()
        {

            string UserName = "";
            UserName = Context.User.Identity.Name;


            if (UserName == "")
            {
                return;
            }


            if (Left(UserName, 4) == "HNBA")
            {
                UserName = Right(UserName, (UserName.Length) - 5);
            }
            else
            {
                UserName = Right(UserName, (UserName.Length) - 7);
            }



            try
            {
                OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
                conProcess.Open();
                OracleCommand spProcess = null;

                spProcess = new OracleCommand("REMOVE_QUICKINFO_ONLINE_USER");

                spProcess.CommandType = System.Data.CommandType.StoredProcedure;
                spProcess.Connection = conProcess;
                spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar, 50).Value = UserName;


                spProcess.ExecuteNonQuery();
                conProcess.Close();



            }
            catch (Exception ex)
            {

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

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            RemoveLoggedUser();
        }
    }
}