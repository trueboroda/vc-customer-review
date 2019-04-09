using System;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using VirtoCommerce.Domain.Commerce.Model.Search;

namespace CustomerReviewsModule.Data.Services
{
    class CustomerReviewSearchService : ICustomerReviewSearchService
    {
        public GenericSearchResult<CustomerReview> SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
