using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Core.Model
{

    /// <summary>
    /// Customer review
    /// </summary>
    public class CustomerReview : AuditableEntity

    {

        public string AuthorNickname { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public string ProductId { get; set; }

        public int Rating { get; set; }

        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

    }
}
