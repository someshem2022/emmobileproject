namespace EM.InsurePlus.Services
{
    using EM.InsurePlus.Common.Constants;
    using EM.InsurePlus.Repository.Contract;
    using EM.InsurePlus.Services.Contract;
    using SO = EM.InsurePlus.Services.Models;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;           
        }

        public async Task<bool> Register(SO.UserModel user, bool isAdmin = false)
        {
            if (user == null)
            {
                return false;
            }
            
            var result = await this.userRepository.Register(user, isAdmin);
            return result != null;
        }
        public async Task<bool> SignInAsync(string userName, string password)
        {
            if (userName == null || password == null) return false;            

            var result = await this.userRepository.SignInAsync(userName, password);
            return result;
        }  
        private async Task<bool> CreateUser(SO.UserModel user)
        {
            return await userRepository.CreateUser(user);
        }

       
    }
}