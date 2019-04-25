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
    /// <summary>
    /// service for work with like/dislike customer review entity. also automacit changing like counters of review entity under hood.
    /// </summary>
    public class CustomerReviewEvaluationService : ServiceBase, ICustomerReviewEvaluationService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;


        public CustomerReviewEvaluationService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public virtual CustomerReviewEvaluation[] GetCustomerReviewsEvaluationsForCustomer(string[] reviewIds, string customerId)
        {
            if (reviewIds == null)
            {
                throw new ArgumentNullException(nameof(reviewIds));
            }

            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            using (var repository = _repositoryFactory())
            {
                var entities = repository.CustomerReviewEvaluations
                    .Where(x => reviewIds.Contains(x.CustomerReviewId) && x.CustomerId == customerId).ToArray();

                var result = entities.Select(x => x.ToModel(AbstractTypeFactory<CustomerReviewEvaluation>.TryCreateInstance()))
                                        .ToArray();

                return result;
            }
        }

        public virtual CustomerReviewEvaluation GetCustomerReviewEvaluationForCustomer(string reviewId, string customerId)
        {
            if (reviewId == null)
            {
                throw new ArgumentNullException(nameof(reviewId));
            }

            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            var result = GetCustomerReviewsEvaluationsForCustomer(new[] { reviewId }, customerId).FirstOrDefault();

            return result;
        }


        public virtual void SaveEvaluation(CustomerReviewEvaluation evaluation)
        {

            if (evaluation == null)
            {
                throw new ArgumentNullException(nameof(evaluation));
            }

            using (var repository = _repositoryFactory())
            using (var changeTracker = base.GetChangeTracker(repository))
            {

                var customerReviewEntity = repository.CustomerReviews.FirstOrDefault(x => x.Id == evaluation.CustomerReviewId);

                if (customerReviewEntity == null)
                {
                    throw new InvalidOperationException($"Customer review with such Id={evaluation.CustomerReviewId} was not found");
                }

                var targetEntity = repository.CustomerReviewEvaluations
                    .Where(x => x.CustomerReviewId == evaluation.CustomerReviewId && x.CustomerId == evaluation.CustomerId)
                    .FirstOrDefault();

                var sourceEntity = AbstractTypeFactory<CustomerReviewEvaluationEntity>
                    .TryCreateInstance()
                    .FromModel(evaluation);


                if ((targetEntity != null))
                {
                    if (targetEntity.ReviewIsLiked != sourceEntity.ReviewIsLiked)
                    {
                        changeTracker.Attach(targetEntity);
                        sourceEntity.Patch(targetEntity);
                        //like counter changing
                        if (sourceEntity.ReviewIsLiked)
                        {
                            customerReviewEntity.LikeCount++;
                            customerReviewEntity.DislikeCount--;
                        }
                        else
                        {
                            customerReviewEntity.DislikeCount++;
                            customerReviewEntity.LikeCount--;
                        }
                    }
                }
                else
                {
                    repository.Add(sourceEntity);

                    //like counter changing
                    if (sourceEntity.ReviewIsLiked)
                    {
                        customerReviewEntity.LikeCount++;
                    }
                    else
                    {
                        customerReviewEntity.DislikeCount++;
                    }
                }

                CommitChanges(repository);
            }
        }
    }
}
