using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerReviewsModule.Core.Model;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Model
{
    public class CustomerReviewEvaluationEntity : AuditableEntity
    {
        public bool ReviewIsLiked { get; set; }

        [Required]
        [StringLength(128)]
        [Index("IX_CustomerReviewId_CustomerId", 1, IsUnique = true)]
        public string CustomerReviewId { get; set; }

        [Required]
        [StringLength(128)]
        [Index("IX_CustomerReviewId_CustomerId", 2, IsUnique = true)]
        public string CustomerId { get; set; }


        public virtual CustomerReviewEntity CustomerReview { get; set; }


        public virtual CustomerReviewEvaluationEntity FromModel(CustomerReviewEvaluation evaluation)
        {

            if (evaluation == null)
            {
                throw new ArgumentNullException(nameof(evaluation));
            }

            evaluation.Id = Id;
            evaluation.CreatedBy = CreatedBy;
            evaluation.CreatedDate = CreatedDate;
            evaluation.ModifiedBy = ModifiedBy;
            evaluation.ModifiedDate = ModifiedDate;

            evaluation.CustomerReviewId = CustomerReviewId;
            evaluation.CustomerId = CustomerId;
            evaluation.ReviewIsLiked = ReviewIsLiked;

            return this;
        }

        public virtual CustomerReviewEvaluation ToModel(CustomerReviewEvaluation evaluation)
        {
            if (evaluation == null)
            {
                throw new ArgumentNullException(nameof(evaluation));
            }

            evaluation.Id = Id;
            evaluation.CreatedBy = CreatedBy;
            evaluation.CreatedDate = CreatedDate;
            evaluation.ModifiedBy = ModifiedBy;
            evaluation.ModifiedDate = ModifiedDate;

            evaluation.CustomerReviewId = CustomerReviewId;
            evaluation.CustomerId = CustomerId;
            evaluation.ReviewIsLiked = ReviewIsLiked;


            return evaluation;
        }

        public virtual void Patch(CustomerReviewEvaluationEntity target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            target.ModifiedBy = ModifiedBy;
            target.ModifiedDate = ModifiedDate;

            target.CustomerReviewId = CustomerReviewId;
            target.CustomerId = CustomerId;
            target.ReviewIsLiked = ReviewIsLiked;
        }
    }
}
