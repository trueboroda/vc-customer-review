using System.Collections.Generic;
using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    public interface IProductRatingCalc
    {
        double CalcRating(IEnumerable<CustomerReview> reviews);
    }
}