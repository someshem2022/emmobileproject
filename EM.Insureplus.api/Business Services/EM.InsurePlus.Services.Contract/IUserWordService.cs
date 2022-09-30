namespace EM.InsurePlus.Services.Contract
{
    using SO = EM.InsurePlus.Services.Models;

    public interface IUserWordService
    {
        Task AddUserWords(SO.AddUserWordRequestModel dataModel);

        Task<SO.UserWordModel> GetUserWordsByUser(SO.GetUserWordRequestModel dataModel);
    }
}