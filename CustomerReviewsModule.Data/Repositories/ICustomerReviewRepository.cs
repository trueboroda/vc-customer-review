using System.Linq;
using CustomerReviewsModule.Data.Model;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Repositories
{
    /// <summary>
    /// Repository for DAL operations over the customer review entities.
    /// </summary>
    public interface ICustomerReviewRepository
        : IRepository

    {

        IQueryable<CustomerReviewEntity> CustomerReviews { get; }
        IQueryable<CustomerReviewEvaluationEntity> CustomerReviewEvaluations { get; }

        IQueryable<ProductRatingEntity> ProductRatings { get; }

        CustomerReviewEntity[] GetCustomerReviewsByIds(string[] ids);

        void DeleteCustomerReviews(string[] ids);



    }
}
