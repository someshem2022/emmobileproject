using SO = EM.InsurePlus.Services.Models;

namespace EM.InsurePlus.Repository.Contract
{
    public interface IUserRepository
    {
        Task<SO.UserModel> GetUserByEmail(string email);

        Task<SO.UserModel> GetUserByUserId(int userId);       

        Task<bool> CreateUser(SO.UserModel user);
        Task<bool> SignInAsync(string userName, string password);

        Task<SO.UserModel> Register(SO.UserModel user, bool isAdmin = false);
    }
}