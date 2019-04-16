using System.Linq;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Repositories;
using CustomerReviewsModule.Data.Services;
using Microsoft.Practices.Unity;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace CustomerReviewsModule.Web
{
    public class Module : ModuleBase
    {
        private const string _connectionStringName = "VirtoCommerce";
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public override void SetupDatabase()
        {
            // Modify database schema with EF migrations
            using (var context = new CustomerReviewRepository(_connectionStringName, _container.Resolve<AuditableInterceptor>()))
            {
                var initializer = new SetupDatabaseInitializer<CustomerReviewRepository, Data.Migrations.Configuration>();
                initializer.InitializeDatabase(context);
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            // This method is called for each installed module on the first stage of initialization.

            // Register implementations:
            _container.RegisterType<ICustomerReviewRepository>(new InjectionFactory(c => new CustomerReviewRepository(_connectionStringName
                , new EntityPrimaryKeyGeneratorInterceptor()
                , _container.Resolve<AuditableInterceptor>())));

            _container.RegisterType<ICustomerReviewSearchService, CustomerReviewSearchService>();
            _container.RegisterType<ICustomerReviewService, CustomerReviewService>();

        }

        public override void PostInitialize()
        {
            base.PostInitialize();



            //Registering settings to store module allows to use individual values in each store
            var settingManager = _container.Resolve<ISettingsManager>();
            var storeSettingsNames = new[] { "CustomerReviewsModule.CustomerReviewsEnabled" };
            var storeSettings = settingManager.GetModuleSettings("CustomerReviewsModule")
                .Where(x => storeSettingsNames.Contains(x.Name)).ToArray();

            settingManager.RegisterModuleSettings("VirtoCommerce.Store", storeSettings);
        }
    }
}
