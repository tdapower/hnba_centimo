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


public partial class UserAdminDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltrlActiveUsers.Text = "<div class=\"num\" data-start=\"0\" data-end=\"" + GetValues("ActiveUsers") + "\" data-postfix=\"\" data-duration=\"1500\" data-delay=\"0\">0</div>";

            ltrlSystemPages.Text = " <div class=\"num\" data-start=\"0\" data-end=\"" + GetValues("SystemPages") + "\" data-postfix=\"\" data-duration=\"1500\" data-delay=\"600\">0</div>";


            ltrlUserRoles.Text = "<div class=\"num\" data-start=\"0\" data-end=\"" + GetValues("UserRoles") + "\" data-postfix=\"\" data-duration=\"1500\" data-delay=\"1200\">0</div>";

            string myScript = "";
            myScript = "\n<script type=\"text/javascript\" language=\"Javascript\" id=\"EventScriptBlock\">\n";
            myScript += "alert('hi');";
            myScript += "\n\n </script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript, true);


            //string lineChartSript = "";
            //lineChartSript = "\n<script type=\"text/javascript\" language=\"Javascript\" id=\"lineChartSriptScriptBlock\">\n";
            //lineChartSript += "   jQuery(document).ready(function ($) {";
            //lineChartSript += "var line_chart_demo = $(\"#line-chart-demo\");";
            //lineChartSript += "var line_chart = Morris.Line({";
            //lineChartSript += "  element: 'line-chart-demo',";
            //lineChartSript += " data: [";
            //lineChartSript += " { y: '1', a: 100 },";
            //lineChartSript += " { y: '2', a: 75 },";
            //lineChartSript += "  { y: '3', a: 100 }";
            //lineChartSript += " ],";
            //lineChartSript += "xkey: 'y',";
            //lineChartSript += " ykeys: ['a'],";
            //lineChartSript += " labels: ['October 2013', 'November 2013'],";
            //lineChartSript += "redraw: true";
            //lineChartSript += "});";
            //lineChartSript += "line_chart_demo.parent().attr('style', '');";
            //lineChartSript += "";


            //lineChartSript += " });";
            //lineChartSript += "\n\n </script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey1", lineChartSript, true);


            LoadUserRoleStatChart();


        }
    }


    private void LoadUserRoleStatChart()
    {
        
            string barChartSript = "";
            barChartSript = "\n<script type=\"text/javascript\" language=\"Javascript\" id=\"barChartSriptScriptBlock\">\n";
            barChartSript += "   jQuery(document).ready(function ($) {";
            barChartSript += "var bar_chart_demo = $(\"#line-chart-demo\");";
            barChartSript += "var bar_chart = Morris.Bar({";
            barChartSript += "  element: 'user-role-bar-chart-view',";
            barChartSript += " data: [";
            barChartSript +=  GetChartValues();
            barChartSript += " ],";
            barChartSript += "xkey: 'x',";
            barChartSript += " ykeys: ['y'],";
            barChartSript += " xLabelAngle: 90,";
            barChartSript += " labels: ['No. of Users'],";
            barChartSript += "redraw: true";
            barChartSript += "});";
            barChartSript += "bar_chart_demo.parent().attr('style', '');";
            barChartSript += "";


            barChartSript += " });";
            barChartSript += "\n\n </script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey1", barChartSript, true);



          // bar Chart
              //  var area_chart_demo = $("#user-role-bar-chart-view");

              //  area_chart_demo.parent().show();

              //  var area_chart = Morris.Bar({
              //      element: 'user-role-bar-chart-view',
              //      data: [
              //            { x: 'COUNTY PARK ROAD ELEM.', yIndex: 376.92 },
              //{ x: 'COUNTY PARK ROAD ELEM.', yIndex: 12.92 },
              //      ],
              //      xkey: 'x',
              //      ykeys: ['yIndex'],
              //      labels: ['Index Points'],
              //      ymax: 500,
              //      barRatio: 0.2,
              //      xLabelAngle: 45,
              //      hideHover: 'auto'
              //  });
              //  area_chart_demo.parent().attr('style', '');
    }
    private string GetValues(string type)
    {
        string returnVal = "";

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        if (type == "ActiveUsers")
        {
            selectQuery = "SELECT COUNT(*) FROM WF_ADMIN_USERS T WHERE T.STATUS=1";

        }
        else if (type == "SystemPages")
        {
            selectQuery = "SELECT COUNT(*) FROM WF_ADMIN_SUB_MENU T";

        }
        else if (type == "UserRoles")
        {
            selectQuery = "SELECT COUNT(*) FROM WF_ADMIN_USER_ROLES T";

        }

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();


        if (dr.HasRows)
        {
            while (dr.Read())
            {

                returnVal = dr[0].ToString();

            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();


        return returnVal;
    }


     [System.Web.Services.WebMethod]
    public static  string GetChartValues()
    {
        string returnVal = "";

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";

        selectQuery = "SELECT UR.DESCRIPTION,(SELECT COUNT(*) FROM WF_ADMIN_USERS U WHERE U.USER_ROLE_CODE=UR.USER_ROLE_CODE AND U.STATUS=1) FROM WF_ADMIN_USER_ROLES UR  ORDER BY UR.USER_ROLE_NAME";



        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();

        if (dr.HasRows)
        {
            while (dr.Read())
            {


                returnVal = returnVal + "{x:'" + dr[0].ToString() + "',y:" + dr[1].ToString() + "},";
                //Morris.Line({
                //    element: 'line-chart-demo',
                //    data: [
                //        { y: '2006', a: 100, b: 90 },
                //        { y: '2007', a: 75, b: 65 },
                //        { y: '2008', a: 50, b: 40 },
                //        { y: '2009', a: 75, b: 65 },
                //        { y: '2010', a: 50, b: 40 },
                //        { y: '2011', a: 75, b: 65 },
                //        { y: '2012', a: 100, b: 90 }
                //    ],
                //    xkey: 'y',
                //    ykeys: ['a', 'b'],
                //    labels: ['October 2013', 'November 2013'],
                //    redraw: true
                //});
            }
        }

        returnVal = returnVal.Remove(returnVal.Length - 1);
     

        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();


        return returnVal;
    }
}
