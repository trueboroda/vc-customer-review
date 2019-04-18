using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    /// <summary>
    /// a service for work with like/dislike evaluation of customer for some customer review
    /// </summary>
    public interface ICustomerReviewEvaluationService
    {

        CustomerReviewEvaluation GetCustomerReviewEvaluationForCustomer(string reviewId, string customerId);

        void SaveEvaluation(CustomerReviewEvaluation evaluation);

    }
}
