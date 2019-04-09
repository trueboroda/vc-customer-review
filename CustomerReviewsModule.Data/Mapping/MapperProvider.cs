using AutoMapper;
using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Data.Mapping
{
    public class MapperProvider
    {
        // declare singleton field
        private volatile static IMapper instance;
        // Lock synchronization object
        private static object syncLock = new object();

        // private constructor.
        private MapperProvider()
        {
        }

        // Get instance
        public static IMapper GetInstance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {

                        var mapperConfiguration = GetMapperCfg();

                        instance = new Mapper(mapperConfiguration);
                    }
                }
            }
            return instance;

        }


        private static MapperConfiguration GetMapperCfg()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerReview, CustomerReviewEntity>();
                cfg.CreateMap<CustomerReviewEntity, CustomerReview>();
            });
        }
    }
}



