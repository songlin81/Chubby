namespace ServiceLayer
{
    public interface IAuthorizationManager
    {
        GetUserResponse GetUser(GetUserRequest request);
    }
}
