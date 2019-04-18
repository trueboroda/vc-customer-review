using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Core.Model
{
    /// <summary>
    /// User like/dislike choice
    /// </summary>
    public class CustomerReviewEvaluation : AuditableEntity
    {
        /// <summary>
        /// true if review is liked, unelse if review dislike value is false
        /// </summary>
        public bool ReviewIsLiked { get; set; }

        public string CustomerReviewId { get; set; }

        public string CustomerId { get; set; }
    }
}
