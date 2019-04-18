using System.Collections.Generic;
using System.Linq;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;

namespace CustomerReviewsModule.Data.Services
{
    public class ProductRatingCalc : IProductRatingCalc
    {
        public double CalcRating(IEnumerable<CustomerReview> reviews)
        {
            var result = reviews.Sum(x => x.Rating) / (double)reviews.Count();

            return result;
        }
    }
}
