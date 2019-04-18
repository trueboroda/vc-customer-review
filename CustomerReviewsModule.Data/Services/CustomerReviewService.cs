using System;
using System.Linq;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Model;
using CustomerReviewsModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviewsModule.Data.Services
{


    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        private readonly IProductRatingService _productRatingService;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory, IProductRatingService productRatingService)
        {
            _repositoryFactory = repositoryFactory;
        }



        public virtual void DeleteCustomerReviews(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                //repository.DeleteCustomerReviews(ids);


                var reviews = repository.CustomerReviews.Where(x => ids.Contains(x.Id)).ToList();

                //get distinct products ids of reviews for products ratings recalculation
                var prodctIds = reviews.Select(x => x.ProductId).Distinct().ToArray();

                foreach (var review in reviews)
                {
                    repository.Remove(review);
                }

                CommitChanges(repository);

                //recalulating products rating
                _productRatingService.RecalculateProductRating(prodctIds);
            }
        }

        public virtual CustomerReview[] GetByIds(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                var entities = repository.GetCustomerReviewsByIds(ids);

                var models = entities.Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance())).ToArray();
                return models;
            }
        }

        public virtual CustomerReview GetById(string id)
        {
            return GetByIds(new[] { id }).FirstOrDefault();
        }

        public virtual CustomerReview CreateCustomerReview(CustomerReview review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            var pkMap = new PrimaryKeyResolvingMap();

            using (var repository = _repositoryFactory())
            {

                var reviewEntity = AbstractTypeFactory<CustomerReviewEntity>
                    .TryCreateInstance()
                    .FromModel(review, pkMap);

                repository.Add(reviewEntity);
                CommitChanges(repository);
                pkMap.ResolvePrimaryKeys();

                _productRatingService.RecalculateProductRating(new[] { review.ProductId });

                return reviewEntity.ToModel(review);
            }

        }


        public virtual void UpdateCustomerReview(CustomerReview[] reviews)
        {
            if (reviews == null)
            {
                throw new ArgumentNullException(nameof(reviews));
            }

            var pkMap = new PrimaryKeyResolvingMap();

            using (var repository = _repositoryFactory())
            using (var changeTracker = GetChangeTracker(repository))
            {

                var alreadyExistEntities = repository.GetCustomerReviewsByIds(reviews.Where(m => !m.IsTransient()).Select(x => x.Id).ToArray());



                foreach (var model in reviews)
                {
                    var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>
                        .TryCreateInstance()
                        .FromModel(model, pkMap);


                    var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);
                    if (targetEntity != null)
                    {
                        changeTracker.Attach(targetEntity);
                        sourceEntity.Patch(targetEntity);

                    }
                }

                CommitChanges(repository);
                pkMap.ResolvePrimaryKeys();

                //recalculate products ratings
                var productIds = reviews.Select(x => x.ProductId).Distinct().ToArray();
                _productRatingService.RecalculateProductRating(productIds);

            }
        }

        public virtual CustomerReview[] GetByProductId(string productId)
        {
            using (var repository = _repositoryFactory())
            {
                var result = repository.CustomerReviews.Where(x => x.ProductId == productId).ToArray();
                return result;
            }
        }
    }
}
