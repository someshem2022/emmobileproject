namespace EM.InsurePlus.Services.Contract
{
    using SO = EM.InsurePlus.Services.Models;

    public interface IWordService
    {
        Task AddItem(SO.WordOfTheDayModel dataModel);

        Task<List<SO.WordOfTheDayModel>> GetAllItems();

        Task<List<SO.WordOfTheDayModel>> GetItemsByWord(string searchKey);

        Task<SO.WordOfTheDayModel> GetWordOfTheDayById(SO.WordByIdRequestModel model);

        Task<SO.WordOfTheDayModel> GetWordOfTheDayAsync(SO.WordOfTheDayRequestModel model);

        Task UpdateItem(SO.WordOfTheDayModel dataModel);

        Task DeleteItem(string wordId, string partitionKeyVal);
    }
}