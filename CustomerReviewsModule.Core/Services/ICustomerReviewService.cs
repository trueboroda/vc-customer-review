using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    /// <summary>
    /// service for work with Customer Reviews
    /// </summary>
    public interface ICustomerReviewService

    {

        CustomerReview[] GetByIds(string[] ids);

        CustomerReview GetById(string id);

        CustomerReview CreateCustomerReview(CustomerReview review);

        void UpdateCustomerReview(CustomerReview[] review);

        void DeleteCustomerReviews(string[] ids);


    }
}
