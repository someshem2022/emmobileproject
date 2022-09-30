namespace EM.InsurePlus.Repository
{
    using AutoMapper;
    using EM.InsurePlus.Common.Constants;
    using EM.InsurePlus.Data.Contract;
    using EM.InsurePlus.Data.Models.Enums;
    using EM.InsurePlus.Data.Models.Identity;
    using EM.InsurePlus.Repository.Contract;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using IO = EM.InsurePlus.Data.Models.Identity;
    using SO = EM.InsurePlus.Services.Models;

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IO.User> userManager;
        private readonly SignInManager<IO.User> signInManager;
        private readonly RoleManager<IO.Role> roleManager;
        private readonly IStorageContext context;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<IO.User> userManager, 
            IMapper mapper,           
            SignInManager<IO.User> signInManager, 
            RoleManager<IO.Role> roleManager, 
            IStorageContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._mapper = mapper;
        }

        public async Task<SO.UserModel> GetUserByEmail(string email)
        {
            IO.User userModel = await userManager.FindByEmailAsync(email);
            return _mapper.Map<IO.User, SO.UserModel>(userModel);
        }

        public async Task<SO.UserModel> GetUserByUserId(int userId)
        {
            IO.User userModel = await userManager.FindByIdAsync(userId.ToString());
            return _mapper.Map<IO.User, SO.UserModel>(userModel);
        }

        public async Task<bool> CreateUser(SO.UserModel user)
        {
            IO.User userModel = _mapper.Map<SO.UserModel, IO.User>(user);

            userModel.UserName = userModel.Email;
            userModel.EmailConfirmed = true;
            userModel.IsActivated = true;
            userModel.PhoneNumber = string.Empty;
            userModel.PhoneNumberConfirmed = false;
            userModel.TwoFactorEnabled = false;
            userModel.CreatedDate = DateTime.Now;

            IdentityResult result = await userManager.CreateAsync(userModel); //"Abcd@1234#"
            //await this.userManager.AddPasswordAsync(newUser, user.Password);
            //await this.signInManager.PasswordSignInAsync(newUser, user.Password, false, false);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> SignInAsync(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return false;
            }
            var formattedusername = userName.Trim().ToUpper();
            var user = await this.userManager.FindByNameAsync(formattedusername);
            if (user == null)
            {
                return false;
            }

            var isUserSignIn = await this.signInManager.CanSignInAsync(user);
            if (isUserSignIn)
            {
                await signInManager.PasswordSignInAsync(user, password, false, false);
                return true;
            }


            return false;
        }

        public async Task<SO.UserModel> Register(SO.UserModel user, bool isAdmin = false)
        {
            try
            {

            

            if (!await roleManager.RoleExistsAsync(SystemConstants.AdminRole))
                await roleManager.CreateAsync(new Role(SystemConstants.AdminRole));
            if (!await roleManager.RoleExistsAsync(SystemConstants.UserRole))
                await roleManager.CreateAsync(new Role(SystemConstants.UserRole));

            var doUser = new User { FirstName = user.FirstName,LastName=user.LastName, Email = user.Email, UserName = user.Email, EmailConfirmed = true, PhoneNumber = user.PhoneNumber, IsActivated = true,ProfileImage="profile.png" };
            var result = await userManager.CreateAsync(doUser);
            if (result.Succeeded)
            {
                var newUser = this.userManager.Users.First(x => x.Email == user.Email);

                if (isAdmin)
                {
                    await userManager.AddToRoleAsync(newUser, SystemConstants.AdminRole);
                }
                else
                {
                    await userManager.AddToRoleAsync(newUser, SystemConstants.UserRole);
                }
                await this.userManager.AddPasswordAsync(newUser, user.Password);
                await this.signInManager.PasswordSignInAsync(newUser, user.Password, false, false);
                return user;
            }

            return default;
            }
            catch (Exception ex)
            {

                var x = ex.ToString();
            }
            return null;
        }
    }
}