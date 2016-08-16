namespace ServiceLayer
{
    public class AuthorizationManager : IAuthorizationManager 
    {
        public GetUserResponse GetUser(GetUserRequest request)
        {
            GetUserResponse response = new GetUserResponse();

            response.Id=request.KeyId+1;
            response.Value = "Good Result";

            return response;
        }
    }
}
