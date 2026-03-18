using System;
using QuantityAppService;
using QuantityAppRepository;
using Microsoft.Extensions.Configuration;
using QuantityAppModel;


namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            Console.WriteLine("Choose Storage Option: ");
            Console.WriteLine("1 -> Sql server Database.");
            Console.WriteLine("2 -> Cache File");

            IQuantityMeasurementRepository repository;

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:

                string connectionString = configuration.GetConnectionString("DefaultConnection");

                QuantityMeasurementDatabaseRepository dbRepository = new QuantityMeasurementDatabaseRepository(connectionString);
                QuantityMeasurementCacheRepository cacheRepository = new QuantityMeasurementCacheRepository();


                if(dbRepository.IsDatabaseAvailable()){
                    List<QuantityMeasurementEntity> entities = cacheRepository.GetAllCachedMeasurements();

                        if(entities.Count > 0)
                        {
                            dbRepository.SaveMeasurementsFromCache(entities);
                            cacheRepository.ClearCache();
                        }
                    repository = dbRepository;
                    Console.WriteLine("Using SQL Server Database Storage");
                }
                    else
                    {
                        Console.WriteLine("Database unavailable. Switching to cache storage.");
                        repository = new QuantityMeasurementCacheRepository();
                    }
                
                break;

                case 2:
                repository = new QuantityMeasurementCacheRepository();
                Console.WriteLine("Using Cache File Storgae");
                break;

                default:
                Console.WriteLine("Wrong Choice!");
                return;
                
            }

            // Create the dependencies
            IQuantityMeasurementService appService = new QuantityMeasurementServices(repository);
            IMenu menu = new Menu();
            
            // Inject them into the controller
            QuantityMeasurementController applicationController = new QuantityMeasurementController(appService, menu);
            
            // Start the application
            applicationController.InitializeApplication();
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
