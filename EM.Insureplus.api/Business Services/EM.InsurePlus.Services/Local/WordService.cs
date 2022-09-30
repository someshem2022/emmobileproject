namespace EM.InsurePlus.Services.Local
{
    using EM.InsurePlus.Repository.Contract;
    using EM.InsurePlus.Services.Contract;
    using EM.InsurePlus.Services.Models.Enums;
    using SO = EM.InsurePlus.Services.Models;

    public class WordService : IWordService
    {
        private readonly IWordOfTheDayRepository wordOfTheDayRepository;
        private readonly IUserWordsRepository userWordsRepository;

        public WordService(IWordOfTheDayRepository wordOfTheDayRepository, IUserWordsRepository userWordsRepository)
        {
            this.wordOfTheDayRepository = wordOfTheDayRepository;
            this.userWordsRepository = userWordsRepository;
        }

        public async Task AddItem(SO.WordOfTheDayModel dataModel)
        {
            //Fix according to requirement => Id & PartitionKey
            dataModel.Id = Guid.NewGuid().ToString();
            dataModel.PartitionKey = Guid.NewGuid().ToString();

            await wordOfTheDayRepository.AddItem(dataModel);
        }

        public async Task<List<SO.WordOfTheDayModel>> GetAllItems()
        {
            return await wordOfTheDayRepository.GetAllItems();
        }

        public async Task<List<SO.WordOfTheDayModel>> GetItemsByWord(string searchKey)
        {
            return await wordOfTheDayRepository.GetItemsBySearchKey(searchKey);
        }

        public async Task UpdateItem(SO.WordOfTheDayModel dataModel)
        {
            await wordOfTheDayRepository.UpdateItem(dataModel);
        }

        public async Task DeleteItem(string wordId, string partitionKeyVal)
        {
            await wordOfTheDayRepository.DeleteItem(wordId, partitionKeyVal);
        }

        public async Task<SO.WordOfTheDayModel> GetWordOfTheDayById(SO.WordByIdRequestModel model)
        {
            return await wordOfTheDayRepository.GetItemByWordId(model.WordId, model.WordPartitionKey);
        }

        public async Task<SO.WordOfTheDayModel> GetWordOfTheDayAsync(SO.WordOfTheDayRequestModel model)
        {
            if (model.SubscriptionPlan == SubscriptionModes.Free)
            {
                return await FreeSubscription(model);
            }
            else
            {
                return await PaidSubscription(model);
            }
        }

        private async Task<SO.WordOfTheDayModel> FreeSubscription(SO.WordOfTheDayRequestModel model)
        {
            if (model.WordIdList is not null && model.WordIdList.Any())
            {//Has at least one record
                //Check Whether the request is a Skip Word request
                if (!model.IsSkipWord)
                {
                    var lastNode = model.WordIdList.OrderByDescending(x => x.Date).FirstOrDefault();

                    //Check last node date is equal to current date
                    if (lastNode is not null && lastNode.Date.Date == DateTime.Now.Date)
                    {
                        return await wordOfTheDayRepository.GetItemByWordId(lastNode.WordId, lastNode.WordPartitionKey);
                    }
                    else
                    {
                        return await GetUniqueWordOfTheDay(model.WordIdList, model.Mode);
                    }
                }
                else
                { //IsSkipWord = true
                    return await GetUniqueWordOfTheDay(model.WordIdList, model.Mode);
                }
            }
            else
            { // Has no records
                return await wordOfTheDayRepository.GetFirstItemByMode(model.Mode);
            }
        }

        private async Task<SO.WordOfTheDayModel> PaidSubscription(SO.WordOfTheDayRequestModel model)
        {
            //Check TrackDatabase has data of the specific user
            SO.UserWordModel userWordList = await this.userWordsRepository.GetUserWordByUserId(model.UserId);

            if (userWordList is not null && userWordList.Words is not null && userWordList.Words.Any())
            {//Has at least one record
                //Check Whether the request is a Skip Word request
                if (!model.IsSkipWord)
                {
                    var lastNode = userWordList.Words.OrderByDescending(x => x.Date).FirstOrDefault();

                    //Check last node date is equal to current date
                    if (lastNode is not null && lastNode.Date.Date == DateTime.Now.Date)
                    {
                        return await wordOfTheDayRepository.GetItemByWordId(lastNode.WordId, lastNode.WordPartitionKey);
                    }
                    else
                    {
                        return await GetUniqueWordOfTheDay(userWordList.Words, model.Mode);
                    }
                }
                else
                { //IsSkipWord = true
                    return await GetUniqueWordOfTheDay(userWordList.Words, model.Mode);
                }
            }
            else
            { // Has no records
                return await wordOfTheDayRepository.GetFirstItemByMode(model.Mode);
            }
        }

        private async Task<SO.WordOfTheDayModel> GetUniqueWordOfTheDay(List<SO.WordKeysModel> words, WordRequestModes mode)
        {
            string partitionKeyString = $"'{string.Join("','", words.Select(x => x.WordPartitionKey))}'";
            return await wordOfTheDayRepository.GetUniqueItem(partitionKeyString, mode);
        }
    }
}