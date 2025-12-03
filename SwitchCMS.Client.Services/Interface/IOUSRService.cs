using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Model.UI;


namespace SwitchCMS.Client.Services.Interface
{
    public interface IOUSRService
    {
        Task<AuthenticationResponseModel> LoginUser(OUSR model);
        Task<OUSR> GetLoginUserDetails(string token);
        Task<UsersPagination> GetUserByPagination(UsersPagination pagination, string token);
        Task<ModificationStatus> CreeateUpdateUser(OUSR model, string token);
        Task<ModificationStatus> DeleteUserByUserID(int UserID,  string token);
    }
}
