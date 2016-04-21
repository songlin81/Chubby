using System;
using System.Collections;
using System.Data;
using System.Security.Principal;

namespace Repeater
{
    public partial class WebForm1 : MyPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String rootPath = Server.MapPath("~");
            ds.ReadXml(rootPath + "websites.xml");
            repeater1.DataSource = ds.Tables[0];
            repeater1.DataBind();

            TreeView1.DataSourceID = "site";
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            Label1.Text = TreeView1.SelectedNode.ValuePath;
        }

        public void SetLabel(string str)
        {
            Label2.Text = str;
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            MyPrincipal principal = new MyPrincipal(tbxUserID.Text, tbxPassword.Text);
            if (!principal.Identity.IsAuthenticated)
            {
                lblLoginMessage.Text = "Incorrect username or password";
                Panel1.Visible = false;
            }
            else
            {
                //Context.User = principal;
                Session["ss"] = principal;
                Hashtable userMessage = new Hashtable();
                userMessage.Add("UserID", tbxUserID.Text);
                userMessage.Add("UserPassword", tbxPassword.Text);
                Context.Cache.Insert("UserMessage", userMessage);
                lblLoginMessage.Text = tbxUserID.Text + "Already logged in";
                Panel1.Visible = true;
            }
        }

        private void btnAdmin_Click(object sender, System.EventArgs e)
        {
            Context.User = Session["ss"] as IPrincipal;
            if (Context.User.IsInRole("Admin"))
            {
                lblRoleMessage.Text = "User" + ((MyPrincipal)Context.User).Identity.Name + " is in Admin group";
            }
            else
            {
                lblRoleMessage.Text = "User" + Context.User.Identity.Name + " is not in Admin group";
            }
        }

        private void btnUser_Click(object sender, System.EventArgs e)
        {
            if (Context.User.IsInRole("User"))
            {
                lblRoleMessage.Text = "User" + Context.User.Identity.Name + " is in User group";
            }
            else
            {
                lblRoleMessage.Text = "User" + Context.User.Identity.Name + " is not in User group";
            }
        }
    }
}