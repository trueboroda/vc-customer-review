using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    public interface IProductRatingService
    {
        ProductRating GetProductRating(string productId);

        void RecalcProductRating(string productId);

    }
}
