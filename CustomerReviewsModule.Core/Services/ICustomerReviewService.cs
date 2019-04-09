using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerReviewsModule.Core.Model;

namespace CustomerReviewsModule.Core.Services
{
    public interface ICustomerReviewService

    {

        CustomerReview[] GetByIds(string[] ids);



        void SaveCustomerReviews(CustomerReview[] items);



        void DeleteCustomerReviews(string[] ids);

    }
}
