namespace EM.InsurePlus.Services.Contract
{
    using EM.InsurePlus.Services.Models;

    public interface ISubscriptionService
    {

        Task<SubscriptionModel?> CreateSubscription(int userId);
    }
}