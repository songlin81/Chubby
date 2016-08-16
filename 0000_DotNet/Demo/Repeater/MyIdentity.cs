namespace Repeater
{
    public class MyIdentity : System.Security.Principal.IIdentity
    {
        private string userID;
        private string password;

        public MyIdentity(string currentUserID, string currentPassword)
        {
            userID = currentUserID;
            password = currentPassword;
        }

        private bool CanPass()
        {
            if (userID == "abc" && password == "123")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        #region IIdentity Member

        public bool IsAuthenticated
        {
            get
            {
                // TODO: Implement MyIdentity.IsAuthenticated getter
                return CanPass();
            }
        }

        public string Name
        {
            get
            {
                // TODO: implement MyIdentity.Name getter
                return userID;
            }
        }

        //This property can be modified for extension purpose.
        public string AuthenticationType
        {
            get
            {
                // TODO: implement MyIdentity.AuthenticationType getter
                return null;
            }
        }

        #endregion
    } 
}