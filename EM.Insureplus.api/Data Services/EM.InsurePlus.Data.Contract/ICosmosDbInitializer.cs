namespace EM.InsurePlus.Data.Contract
{
    public interface ICosmosDbInitializer
    {
        Task StartDatabaseConfiguration();
    }
}