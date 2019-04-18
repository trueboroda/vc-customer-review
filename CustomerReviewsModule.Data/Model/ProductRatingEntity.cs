using System;
using System.ComponentModel.DataAnnotations;
using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Data.Model
{
    public class ProductRatingEntity
    {

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [StringLength(128)]
        public double Rating { get; set; }


        public virtual ProductRating ToModel(ProductRating productRating)
        {
            if (productRating == null)
            {
                throw new ArgumentNullException(nameof(productRating));
            }

            productRating.ProductId = ProductId;
            productRating.Rating = Rating;

            return productRating;
        }

    }
}
