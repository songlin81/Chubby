using System;
using System.Reflection;

namespace Repeater
{
    public partial class WebUserControlClickTest : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Web.UI.Page p = this.Page;
            Type pageType = p.GetType();
            MethodInfo mi = pageType.GetMethod("SetLabel");
            mi.Invoke(p, new object[] { "Simulate data retrieved from backend..." });
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            //below is needed since from UserControl AutoEventWireup="false" is given.
            this.ButtonUC.Click += new System.EventHandler(Button1_Click);
            Load += Page_Load;
        }   
    }
}