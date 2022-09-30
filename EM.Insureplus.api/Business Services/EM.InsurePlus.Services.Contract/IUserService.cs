namespace EM.InsurePlus.Services.Contract
{
    using SO = EM.InsurePlus.Services.Models;

    public interface IUserService
    {
        //Task<SO.UserProfileInfoModel> GetUserInfoAsync(SO.UserModel user);

        Task<bool> SignInAsync(string userName, string password);

        Task<bool> Register(SO.UserModel user, bool isAdmin = false);

       
    }
}