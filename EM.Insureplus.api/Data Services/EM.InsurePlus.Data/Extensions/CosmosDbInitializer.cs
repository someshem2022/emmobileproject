namespace EM.InsurePlus.Data.Extensions
{
    using System.Net;
    using EM.InsurePlus.Common.Configuration;
    using EM.InsurePlus.Data.Contract;
    using EM.InsurePlus.Data.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class CosmosDbInitializer : ICosmosDbInitializer
    {
        private readonly IOptions<AppConfigs> _appConfigs;

        public CosmosDbInitializer(IOptions<AppConfigs> appConfigs)
        {
            this._appConfigs = appConfigs;
        }

        public async Task StartDatabaseConfiguration()
        {
            try
            {
                using (CosmosClient cosmosClient = new CosmosClient(_appConfigs.Value.EndpointUri, _appConfigs.Value.PrimaryKey,
                    new CosmosClientOptions() { AllowBulkExecution = true }))
                {
                    // Create a new database
                    Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(_appConfigs.Value.DatabaseId);

                    // Create Words container
                    Container container = await database.CreateContainerIfNotExistsAsync(_appConfigs.Value.WordsContainer, "/partitionKey");

                    // Create User Words Container
                    await database.CreateContainerIfNotExistsAsync(_appConfigs.Value.UserWordsContainer, "/partitionKey");

                    //Init Mock Data
                    //  --Check Database has Rows
                    QueryDefinition queryDefinition = new QueryDefinition($"SELECT COUNT(1) as RowCount FROM c");
                    FeedIterator<DocumentRowCountModel> queryResultSetIterator = container.GetItemQueryIterator<DocumentRowCountModel>(queryDefinition);

                    int rowCount = 0;
                    while (queryResultSetIterator.HasMoreResults)
                    {
                        FeedResponse<DocumentRowCountModel> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                        foreach (DocumentRowCountModel _count in currentResultSet)
                        {
                            rowCount = _count.RowCount;
                        }
                    }

                    if (rowCount == 0)
                    {
                        //  --Feed Data to the Database
                        EmbeddedFileReader embeddedReader = new();

                        string jsonContent = embeddedReader.ReadEmbeddedResource("Evicio.ILearnIt.Data.Local", "WordCollection.json");
                        if (string.IsNullOrEmpty(jsonContent)) return;

                        var wordOfTheDayMockData = JsonConvert.DeserializeObject<List<WordOfTheDayModel>>(jsonContent);
                        if (wordOfTheDayMockData != null && wordOfTheDayMockData.Any())
                        {
                            foreach (var word in wordOfTheDayMockData)
                            {
                                word.Id = Guid.NewGuid().ToString();
                                word.PartitionKey = Guid.NewGuid().ToString();
                                _ = await container.CreateItemAsync(word, new PartitionKey(word.PartitionKey));
                            }
                        }
                    }

                    // Read & Change the current throughput
                    /*int? throughput = await container.ReadThroughputAsync();
                    if (throughput.HasValue && throughput.Value < 400)
                    {
                        int newThroughput = throughput.Value + 100;

                        // Update throughput
                        await container.ReplaceThroughputAsync(newThroughput);
                    }*/
                }
            }
            catch (CosmosException cosmosException) when (cosmosException.StatusCode == HttpStatusCode.BadRequest) //Cannot read container throuthput.
            {
                Console.WriteLine(cosmosException.ResponseBody);
            }
            catch (CosmosException de)
            {
                Console.WriteLine("{0} error occurred: {1}", de.InnerException != null ? de.InnerException.Message : string.Empty, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
        }
    }
}