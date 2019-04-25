using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    /// <summary>
    /// service for work with Customer Reviews
    /// </summary>
    public interface ICustomerReviewService

    {

        CustomerReview[] GetByIds(string[] ids);

        //CustomerReview[] GetReviews(Expression<Func<CustomerReview, bool>> predicate);


        CustomerReview[] GetByProductId(string productId);
        CustomerReview[] GetByProductIds(string[] productIds);

        CustomerReview GetById(string id);

        CustomerReview CreateCustomerReview(CustomerReview review);

        void UpdateCustomerReviews(CustomerReview[] review);

        void DeleteCustomerReviews(string[] ids);


    }
}
