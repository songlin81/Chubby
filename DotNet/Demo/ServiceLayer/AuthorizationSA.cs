using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace ServiceLayer
{
    public class AuthorizationSA
    {
        public string GetUser(string baldoId)
        {
            GetUserRequest request = new GetUserRequest();
            //GetUserResponse response = null;

            request.KeyId = Int32.Parse(baldoId);
           
            var value = Execute<IAuthorizationManager, GetUserResponse>(request);

            return value.Value;
        }

        protected static TResponseMessage Execute<TServiceInterface, TResponseMessage>(object requestMessage)
        {
            Exception shieldException;

            var serviceTypes = (NameValueCollection)ConfigurationManager.GetSection("serviceConfiguration/serviceTypeMapping");
            var trace = new StackTrace();

            string serviceActionName = trace.GetFrame(1).GetMethod().Name;
            try
            {
                var requestBase = (RequestBaseMessage)requestMessage;

                requestBase.BusinessAction = serviceActionName;

                Type serviceType = typeof(TServiceInterface);
                string typeName = serviceTypes[serviceType.Name];
                Type serviceImplementationType = Type.GetType(typeName);
                if (serviceImplementationType != null)
                {
                    object serviceTarget = Activator.CreateInstance(serviceImplementationType);

                    object serviceInstance = Microsoft.Practices.EnterpriseLibrary.PolicyInjection.PolicyInjection.Wrap<TServiceInterface>(serviceTarget);
                    MethodInfo method = serviceType.GetMethod(serviceActionName);
                    return (TResponseMessage)method.Invoke(serviceInstance, new[] { requestMessage });
                }
            }

            catch (Exception ex)
            {
                //shieldException = ShieldAndLogException(ex, "ApplicationException", string.Empty);
                throw ex;
            }
            return default(TResponseMessage);
        }
    }
}
