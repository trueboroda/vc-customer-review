using System.Linq;
using CustomerReviewsModule.Core.Model;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Repositories
{
    public interface ICustomerReviewRepository
        : IRepository

    {

        IQueryable<CustomerReviewEntity> CustomerReviews { get; }



        CustomerReviewEntity[] GetByIds(string[] ids);

        void DeleteCustomerReviews(string[] ids);

    }
}
