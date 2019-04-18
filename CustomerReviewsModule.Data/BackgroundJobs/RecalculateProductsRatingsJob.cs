using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.BackgroundJobs
{

    /// <summary>
    /// Hangfire Job class for background recalculate product users rating
    /// </summary>
    public class RecalculateProductsRatingsJob
    {

        private readonly ICustomerReviewService _customerReviewService;
        private readonly IProductRatingCalc _ratingCalculator;
        private readonly IProductRatingService _productRatingService;

        public RecalculateProductsRatingsJob(ICustomerReviewService customerReviewService, IProductRatingCalc ratingCalculator, IProductRatingService productRatingService)
        {
            _customerReviewService = customerReviewService;
            _ratingCalculator = ratingCalculator;
            _productRatingService = productRatingService;
        }



        public void Recalculate(string[] productIds)
        {
            foreach (var productId in productIds)
            {
                var reviews = _customerReviewService.GetByProductId(productId);

                var rating = _ratingCalculator.CalcRating(reviews);

                var productRating = AbstractTypeFactory<ProductRating>.TryCreateInstance();
                productRating.ProductId = productId;
                productRating.Rating = rating;

                _productRatingService.SaveProductRating(productRating);

            }

        }

    }
}
