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

        public double Rating { get; set; }


        public virtual ProductRatingEntity FromModel(ProductRating productRating)
        {
            if (productRating == null)
            {
                throw new ArgumentNullException(nameof(productRating));
            }
            ProductId = productRating.ProductId;
            Rating = productRating.Rating;

            return this;
        }


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


        public virtual void Patch(ProductRatingEntity target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));


            target.Rating = Rating;

        }

    }
}
