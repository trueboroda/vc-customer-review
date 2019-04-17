using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Model
{
    public class ProductRatingEntity : Entity
    {
        public string ProductId { get; set; }

        public string Rating { get; set; }
    }
}
