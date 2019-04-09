using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Core.Model
{
    [Table("CustomerReview")]
    public class CustomerReviewEntity : AuditableEntity

    {
        [StringLength(128)]
        public string AuthorNickname { get; set; }

        [StringLength(2048)]
        [Required]
        public string Content { get; set; }

        public bool IsActive { get; set; }

        [StringLength(128)]
        [Required]
        public string ProductId { get; set; }


        public virtual void Update(CustomerReviewEntity target)
        {

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            target.AuthorNickname = AuthorNickname;
            target.Content = Content;
            target.IsActive = IsActive;
            target.ProductId = ProductId;
        }

    }
}
