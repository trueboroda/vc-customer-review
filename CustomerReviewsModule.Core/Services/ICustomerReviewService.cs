using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    public interface ICustomerReviewService

    {

        CustomerReview[] GetByIds(string[] ids);

        void SaveCustomerReviews(CustomerReview[] items);

        void DeleteCustomerReviews(string[] ids);


        void ActivateCustomerReview(string id);

        void DeactivateCustomerReview(string id);

        void LikeCustomerReview(string reviewId, string customerId);

        void DislikeCustomerReview(string reviewId, string customerId);


    }
}
