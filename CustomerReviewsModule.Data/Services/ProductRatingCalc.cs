using System;
using System.Collections.Generic;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;

namespace CustomerReviewsModule.Data.Services
{
    public class ProductRatingCalc : IProductRatingCalc
    {
        public double CalcRating(IEnumerable<CustomerReview> reviews)
        {
            throw new NotImplementedException();
        }
    }
}
