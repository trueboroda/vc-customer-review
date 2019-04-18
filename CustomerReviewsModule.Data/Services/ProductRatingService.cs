using System;
using System.Linq;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.BackgroundJobs;
using CustomerReviewsModule.Data.Model;
using CustomerReviewsModule.Data.Repositories;
using Hangfire;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviewsModule.Data.Services
{
    public class ProductRatingService : ServiceBase, IProductRatingService
    {


        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public ProductRatingService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public virtual ProductRating GetProductRating(string productId)
        {

            if (productId == null)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            using (var repository = _repositoryFactory())
            {
                var entity = repository.ProductRatings.FirstOrDefault(x => x.ProductId == productId);

                var result = AbstractTypeFactory<ProductRating>.TryCreateInstance();

                if (entity != null)
                {
                    result = entity.ToModel(result);
                }
                else
                {
                    result.ProductId = productId;
                    result.Rating = 0.0;
                }

                return result;
            }
        }
        /// <summary>
        /// enqueue hangfires job for recalculating products ratings
        /// </summary>
        /// <param name="productIds"></param>
        public virtual void RecalculateProductRating(string[] productIds)
        {
            if (productIds == null)
            {
                throw new ArgumentNullException(nameof(productIds));
            }

            BackgroundJob.Enqueue<RecalculateProductsRatingsJob>(x => x.Recalculate(productIds));
        }

        public virtual void SaveProductRating(ProductRating productRating)
        {

            if (productRating == null)
            {
                throw new ArgumentNullException(nameof(productRating));
            }

            using (var repository = _repositoryFactory())
            using (var changeTracker = base.GetChangeTracker(repository))
            {


                var sourceEntity = AbstractTypeFactory<ProductRatingEntity>.TryCreateInstance()
                    .FromModel(productRating);

                var targetEntity = repository.ProductRatings
                    .Where(x => x.ProductId == productRating.ProductId)
                    .FirstOrDefault();


                if (targetEntity != null)
                {
                    changeTracker.Attach(targetEntity);
                    sourceEntity.Patch(targetEntity);
                }
                else
                {
                    repository.Add(sourceEntity);
                }

                repository.UnitOfWork.Commit();
            }
        }
    }
}
