using System;
using System.Collections;
using System.Security.Principal;

namespace Repeater
{
    public class MyPage : System.Web.UI.Page
    {
        public MyPage()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += MyPage_Load;
        }

        //Extract user onLoad.
        private void MyPage_Load(object sender, System.EventArgs e)
        {
            Context.User = Session["ss"] as IPrincipal;
            if (Context.User == null)
            {
                return;
            }
            if (Context.Cache["UserMessage"] != null)
            {
                Hashtable userMessage = (Hashtable)Context.Cache["UserMessage"];  //Cache here is persisting data even restart web application!!!
                MyPrincipal principal = new MyPrincipal(userMessage["UserID"].ToString(), userMessage["UserPassword"].ToString());
                Context.User = principal;
            }
        }
    } 
}