namespace CustomerReviewsModule.Core.Model
{
    public class CustomerReviewLikeChoice
    {
        public bool IsLiked { get; set; }

        public string ReviewId { get; set; }

        public string CustomerId { get; set; }
    }
}
