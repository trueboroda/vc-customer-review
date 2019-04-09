using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Web.Security;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Web.Security;

namespace CustomerReviewsModule.Web.Controllers.Api
{
    [RoutePrefix("api/CustomerReviewsModule.Web")]
    public class ManagedModuleController : ApiController
    {

        private ICustomerReviewSearchService _customerReviewSearchService;

        public ManagedModuleController(ICustomerReviewSearchService customerReviewSearchService)
        {
            _customerReviewSearchService = customerReviewSearchService;
        }

        // GET: api/managedModule
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(new { result = "Hello world!" });
        }


        [HttpPost]

        [Route("search")]

        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]

        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]

        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)

        {

            GenericSearchResult<CustomerReview> result = _customerReviewSearchService.SearchCustomerReviews(criteria);

            return Ok(result);

        }
    }
}
