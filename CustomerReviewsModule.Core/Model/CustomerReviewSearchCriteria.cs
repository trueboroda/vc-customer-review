using VirtoCommerce.Domain.Commerce.Model.Search;

namespace CustomerReviewsModule.Core.Model
{

    /// <summary>
    /// criteria for reviews searching
    /// </summary>
    public class CustomerReviewSearchCriteria
        : SearchCriteriaBase

    {


        public string[] ProductIds { get; set; }

        public bool? IsActive { get; set; }

    }
}
