namespace Lx.Utilities.Contracts.Authorization
{
    public interface ICustomAuthorizeAttribute : IAccessCriteria
    {
        /// <summary>
        ///     If set to true, although the authorization check will be done, the user can still access the invoked method by
        ///     having IsAuthenticated set to true
        /// </summary>
        bool GetUserInfoOnly { get; set; }
    }
}