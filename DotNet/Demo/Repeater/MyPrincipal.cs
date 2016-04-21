using System.Collections;

namespace Repeater
{
    public class MyPrincipal : System.Security.Principal.IPrincipal
    {
        private System.Security.Principal.IIdentity identity;
        private ArrayList roleList;

        public MyPrincipal(string userID, string password)
        {
            identity = new MyIdentity(userID, password);

            if (identity.IsAuthenticated)
            {
                //If pass authentication, then start to retrieve role from database
                //To simplify, add admin role for such user.
                roleList = new ArrayList();
                roleList.Add("Admin");
            }
            else
            {
                // do nothing
            }
        }

        public ArrayList RoleList
        {
            get
            {
                return roleList;
            }
        }

        #region IPrincipal member

        public System.Security.Principal.IIdentity Identity
        {
            get
            {
                // TODO: implement MyPrincipal.Identity getter
                return identity;
            }
            set
            {
                identity = value;
            }
        }

        public bool IsInRole(string role)
        {
            // TODO: implement MyPrincipal.IsInRole
            return roleList.Contains(role);
        }

        #endregion
    }
}