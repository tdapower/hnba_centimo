using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HNBA_APP_STORE.CommonCLS
{
    public class AppsListCustomTemplate : ITemplate
    {

        static int itemcount = 0;
        ListItemType templateType;
        public AppsListCustomTemplate(ListItemType type)
        {
            templateType = type;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = "<TABLE border=1><TR><TH>Items</TH></TR>";
                    break;
                case ListItemType.Item:
                    lc.Text = "<TR><TD>";
                    lc.DataBinding += new EventHandler(TemplateControl_DataBinding);
                    break;
                case ListItemType.AlternatingItem:
                    lc.Text = "<TR><TD bgcolor=lightblue>Item number: " +
                       itemcount.ToString() + "</TD></TR>";
                    break;
                case ListItemType.Footer:
                    lc.Text = "</TABLE>";
                    break;

            }
            container.Controls.Add(lc);
            itemcount += 1;
        }

        private void TemplateControl_DataBinding(object sender, System.EventArgs e)
        {
            Literal lc;
            lc = (Literal)sender;
            RepeaterItem container = (RepeaterItem)lc.NamingContainer;
            lc.Text += DataBinder.Eval(container.DataItem, "APP_CODE");
            lc.Text += DataBinder.Eval(container.DataItem, "APP_NAME");
            lc.Text += "</TD></TR>";
        }
    }
}