using System;
using System.Linq;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Model;
using CustomerReviewsModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Services
{
    public class CustomerReviewEvaluationService : ICustomerReviewEvaluationService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;


        public CustomerReviewEvaluationService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public virtual CustomerReviewEvaluation GetCustomerReviewEvalutionForCustomer(string reviewId, string customerId)
        {
            using (var repository = _repositoryFactory())
            {
                var entity = repository.CustomerReviewEvaluations
                    .FirstOrDefault(x => x.CustomerReviewId == reviewId && x.CustomerId == customerId);

                var result = entity?
                    .ToModel(AbstractTypeFactory<CustomerReviewEvaluation>.TryCreateInstance());

                return result;
            }
        }


        public virtual void SaveEvaluation(CustomerReviewEvaluation evaluation)
        {
            using (var repository = _repositoryFactory())
            {
                var targetEntity = repository.CustomerReviewEvaluations
                    .Where(x => x.CustomerReviewId == evaluation.CustomerReviewId && x.CustomerId == evaluation.CustomerId)
                    .FirstOrDefault();

                var sourceEntity = AbstractTypeFactory<CustomerReviewEvaluationEntity>
                    .TryCreateInstance()
                    .FromModel(evaluation);


                if (targetEntity != null)
                {
                    sourceEntity.Patch(targetEntity);
                }
                else
                {
                    repository.Add(sourceEntity);
                }

            }
        }
    }
}
