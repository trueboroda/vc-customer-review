using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{

    /// <summary>
    /// Service for work with product rating
    /// </summary>
    public interface IProductRatingService
    {
        ProductRating GetProductRating(string productId);

        ProductRating[] GetProductsRatings(string[] productIds);

        void RecalculateProductRating(string[] productIds);

        void SaveProductRating(ProductRating productRating);

    }
}
