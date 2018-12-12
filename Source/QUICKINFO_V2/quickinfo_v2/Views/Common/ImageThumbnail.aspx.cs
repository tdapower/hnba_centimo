using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace quickinfo_v2.Views.Common
{
    public partial class ImageThumbnail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["userCode"] != null)
            {
                if (Request.Params["userCode"] != "")
                {
                    loadImage(Request.Params["userCode"].ToString());
                }
            }

        }

        //private void loadImage(string userCode)
        //{
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());


        //    con.Open();
        //    OracleDataReader dr;

        //    //con.Open();

        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = con;
        //    String selectQuery = "";

        //    selectQuery = "select t.epic_picture from hnbhrm.hs_hr_emp_picture@HNBHRM t " +
        //            " left join hnbhrm.hs_hr_employee@HNBHRM e on t.emp_number=e.emp_number " +
        //            " left join wf_admin_users w on e.emp_payrollno=lpad(w.epf_no,6,'0') " +
        //            " where w.user_code='" + userCode + "'";

        //    cmd.CommandText = selectQuery;
        //    dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        if (dr["epic_picture"] != System.DBNull.Value)
        //        {
        //            //Oracle blob = dr.GetOracleBlob(0);
        //            //Response.ContentType = "image/jpeg";
        //            //Response.BinaryWrite(blob.Value);



        //            Response.ContentType = "image/jpeg";

        //            Response.BinaryWrite((byte[])dr["epic_picture"]);
        //            Response.End();
        //        }
        //    }

        //    dr.Close();
        //    dr.Dispose();
        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();

        //} 

        private void loadImage(string userCode)
        {

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());


            con.Open();
            OracleDataReader dr;

            //con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT PHOTO FROM WF_ADMIN_USER_PHOTO  " +
                      " WHERE USER_CODE='" + userCode + "'  ";

            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["PHOTO"] != System.DBNull.Value)
                {
                    OracleBlob blob = dr.GetOracleBlob(0);
                    Response.ContentType = "image/jpeg";
                    Response.BinaryWrite(blob.Value);
                    Response.End();
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();



        }
    }
}