using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    public interface ICustomerReviewEvaluationService
    {

        CustomerReviewEvaluation GetCustomerReviewEvaluationForCustomer(string reviewId, string customerId);

        void SaveEvaluation(CustomerReviewEvaluation evaluation);

        //bool ChangeEvaluetion(CustomerReviewEvaluation evaluation);

    }
}
