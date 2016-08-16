using System;
using System.Collections.Specialized;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace ServiceLayer
{
    [ConfigurationElementType(typeof(CustomCallHandlerData))]
    public class AuthorizationHandler : ICallHandler
    {
        #region "Fields"

        #endregion

        #region Properties

        public int Order { get; set; }

        #endregion

        public AuthorizationHandler(NameValueCollection nvColl)
        {
                try
                {
                }
                catch
                {
                    // do nothing
                }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            string baldoId = null;
            string businessActionName = null;
            bool isAuthorized = false;
            RequestBaseMessage request = null;

            if (input != null && input.Arguments.Count > 0)
            {
                request = (RequestBaseMessage)input.Arguments[0];
            }

            try
            {
                if (request != null)
                {
                    //baldoId = request.BaldoUserId;
                    businessActionName = request.BusinessAction;

                    isAuthorized = IsAuthorized(businessActionName);
                    if (!isAuthorized)
                    {
                        throw new Exception("Not authorized!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("AuthorizationHandler error", ex));
            }

            return getNext().Invoke(input, getNext);
        }

        private bool IsAuthorized(string businessActionName)
        {
            return true;
        }
    }
}
