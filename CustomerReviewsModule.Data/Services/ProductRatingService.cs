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


        public virtual ProductRating[] GetProductsRatings(string[] productIds)
        {
            if (productIds == null)
            {
                throw new ArgumentNullException(nameof(productIds));
            }

            using (var repository = _repositoryFactory())
            {
                var entities = repository.ProductRatings.Where(x => productIds.Contains(x.ProductId)).ToArray();

                var resultList = entities.Select(x => x.ToModel(AbstractTypeFactory<ProductRating>.TryCreateInstance())).ToList();

                foreach (var productId in productIds)
                {
                    if (!resultList.Any(x => x.ProductId == productId))
                    {
                        resultList.Add(new ProductRating() { ProductId = productId, Rating = 0.0 });
                    }
                }
                return resultList.ToArray();
            }
        }
        public virtual ProductRating GetProductRating(string productId)
        {

            if (productId == null)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            var result = GetProductsRatings(new[] { productId }).FirstOrDefault();

            return result;
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
