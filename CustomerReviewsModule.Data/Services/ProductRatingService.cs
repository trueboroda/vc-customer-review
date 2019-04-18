using System;
using System.Linq;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Services
{
    public class ProductRatingService : IProductRatingService
    {


        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public ProductRatingService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public ProductRating GetProductRating(string productId)
        {
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

        public void RecalculateProductRating(string[] productIds)
        {
            throw new NotImplementedException();
        }
    }
}
