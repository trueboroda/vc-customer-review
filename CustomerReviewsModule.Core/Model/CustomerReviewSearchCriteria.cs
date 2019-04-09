using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Domain.Commerce.Model.Search;

namespace CustomerReviewsModule.Core.Model
{
    public class CustomerReviewSearchCriteria 
        : SearchCriteriaBase

    {

        public string[] ProductIds { get; set; }

        public bool? IsActive { get; set; }

    }
}
