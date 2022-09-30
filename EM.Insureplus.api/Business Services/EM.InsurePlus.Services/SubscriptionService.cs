namespace EM.InsurePlus.Services
{
    using EM.InsurePlus.Common;
    using EM.InsurePlus.Common.Constants;
    using EM.InsurePlus.Repository.Contract;
    using EM.InsurePlus.Services.Contract;
    using EM.InsurePlus.Services.Models;

    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUserRepository userRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
        }

       

        public async Task<SubscriptionModel?> CreateSubscription(int userId)
        {
            var subscriptionInfo = await _subscriptionRepository.CreateSubscription(userId);

            if (subscriptionInfo is not null)
                subscriptionInfo.Status = Models.Enums.SubscriptionModes.Active.ToString();

            return subscriptionInfo;
        }
    }
}