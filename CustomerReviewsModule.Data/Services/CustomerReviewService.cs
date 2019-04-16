using System;
using System.Linq;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviewsModule.Data.Services
{
    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                repository.DeleteCustomerReviews(ids);
            }
        }

        public CustomerReview[] GetByIds(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                var entities = repository.GetByIds(ids);

                var models = entities.Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance())).ToArray();
                return models;
            }
        }

        public void SaveCustomerReviews(CustomerReview[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var pkMap = new PrimaryKeyResolvingMap();

            using (var repository = _repositoryFactory())
            using (var changeTracker = GetChangeTracker(repository))
            {

                var alreadyExistEntities = repository.GetByIds(items.Where(m => !m.IsTransient()).Select(x => x.Id).ToArray());
                foreach (var model in items)
                {
                    var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>
                        .TryCreateInstance()
                        .FromModel(model, pkMap);

                    //pkMap.AddPair(model, sourceEntity);//strange thing
                    //sourceEntity = mapper.Map<CustomerReviewEntity>(model);

                    var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);
                    if (targetEntity != null)
                    {
                        changeTracker.Attach(targetEntity);
                        sourceEntity.Patch(targetEntity);
                    }
                    else
                    {
                        repository.Add(sourceEntity);
                    }
                }


                CommitChanges(repository);
                pkMap.ResolvePrimaryKeys();

            }
        }
    }
}
