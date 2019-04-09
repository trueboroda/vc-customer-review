using CustomerReviewsModule.Core.Model;
using VirtoCommerce.Domain.Commerce.Model.Search;

namespace CustomerReviewsModule.Core.Services
{
    public interface ICustomerReviewSearchService
    {

        GenericSearchResult<CustomerReview> SearchCustomerReviews(CustomerReviewSearchCriteria criteria);

    }
}
