using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerReviewsModule.Core.Model;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Model
{
    [Table("CustomerReview")]
    public class CustomerReviewEntity : AuditableEntity

    {
        [StringLength(128)]
        public string AuthorNickname { get; set; }

        [StringLength(1048)]
        [Required]
        public string Content { get; set; }

        public bool IsActive { get; set; }

        [StringLength(128)]
        [Required]
        public string ProductId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public virtual ObservableCollection<CustomerReviewEvaluationEntity> Evaluations { get; set; } = new NullCollection<CustomerReviewEvaluationEntity>();




        public virtual CustomerReview ToModel(CustomerReview customerReview)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            customerReview.Id = Id;
            customerReview.CreatedBy = CreatedBy;
            customerReview.CreatedDate = CreatedDate;
            customerReview.ModifiedBy = ModifiedBy;
            customerReview.ModifiedDate = ModifiedDate;

            customerReview.AuthorNickname = AuthorNickname;
            customerReview.Content = Content;
            customerReview.IsActive = IsActive;
            customerReview.ProductId = ProductId;
            customerReview.Rating = Rating;
            customerReview.LikeCount = LikeCount;
            customerReview.DislikeCount = DislikeCount;


            return customerReview;
        }

        public virtual CustomerReviewEntity FromModel(CustomerReview customerReview, PrimaryKeyResolvingMap pkMap)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            pkMap.AddPair(customerReview, this);

            Id = customerReview.Id;
            CreatedBy = customerReview.CreatedBy;
            CreatedDate = customerReview.CreatedDate;
            ModifiedBy = customerReview.ModifiedBy;
            ModifiedDate = customerReview.ModifiedDate;

            AuthorNickname = customerReview.AuthorNickname;
            Content = customerReview.Content;
            IsActive = customerReview.IsActive;
            ProductId = customerReview.ProductId;
            Rating = customerReview.Rating;


            return this;
        }

        public virtual void Patch(CustomerReviewEntity target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            target.ModifiedBy = ModifiedBy;
            target.ModifiedDate = ModifiedDate;

            target.AuthorNickname = AuthorNickname;
            target.Content = Content;
            target.IsActive = IsActive;
            target.ProductId = ProductId;
            target.Rating = Rating;
        }

    }
}
