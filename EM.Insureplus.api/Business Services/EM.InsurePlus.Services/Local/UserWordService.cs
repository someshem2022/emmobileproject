namespace EM.InsurePlus.Services.Local
{
    using EM.InsurePlus.Repository.Contract;
    using EM.InsurePlus.Services.Contract;
    using SO = EM.InsurePlus.Services.Models;

    public class UserWordService : IUserWordService
    {
        private readonly IUserWordsRepository userWordsRepository;

        public UserWordService(IUserWordsRepository userWordsRepository)
        {
            this.userWordsRepository = userWordsRepository;
        }

       
    }
}