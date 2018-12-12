using quickinfo_v2.Controllers.ITWorkflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using quickinfo_v2.Models.ITWorkflow;


namespace quickinfo_v2.Views.ITWorkflow
{
    public partial class UserRequestView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         

        }




        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    UserRequest userRequest=new UserRequest();
        //    userRequest.RefNo=txtRefNo.Text;
        //    userRequest.JobRemarks=txtRefNo.Text;
        //  //  userRequest.Screenshot=txtRefNo.Text;
        //    userRequest.RequestedUser=txtRefNo.Text;


        //    UserRequestDB userRequestDB = new UserRequestDB();
        //    userRequestDB.InsertUserRequest(userRequest);


      
        
        //}

        //public String GetCurrentUserCode()
        //{


        //    String UserName = Context.User.Identity.Name;
        //    try
        //    {
        //        if (Left(UserName, 4) == "HNBA")
        //        {
        //            UserName = Right(UserName, (UserName.Length) - 5);
        //        }
        //        else
        //        {
        //            UserName = Right(UserName, (UserName.Length) - 7);
        //        }



        //    }
        //    catch (Exception ee)
        //    {
        //        return false;
        //    }
        //}




        protected void btnAddNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnAlter_Click(object sender, EventArgs e)
        {
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}