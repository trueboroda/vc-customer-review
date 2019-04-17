using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Web.Security;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Web.Security;

namespace CustomerReviewsModule.Web.Controllers.Api
{
    [RoutePrefix("api/CustomerReviews")]
    public class ManagedModuleController : ApiController
    {

        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;

        public ManagedModuleController(ICustomerReviewSearchService customerReviewSearchService, ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
        }



        /// <summary>
        /// return search result object with founded customer reviews
        /// </summary>
        /// <param name="criteria">search criteria</param>
        /// <returns></returns>

        [HttpPost]

        [Route("search")]

        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]

        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]

        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)

        {

            GenericSearchResult<CustomerReview> result = _customerReviewSearchService.SearchCustomerReviews(criteria);

            return Ok(result);

        }


        /// <summary>
        ///  Create new or update existing customer reviews
        /// </summary>
        /// <param name="customerReviews">Customer reviews</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            _customerReviewService.SaveCustomerReviews(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewDelete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviews(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpGet]
        [Route("activate")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Activate([FromUri]string id)
        {
            _customerReviewService.ActivateCustomerReview(id);

            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpGet]
        [Route("deactivate")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Deactivate()
        {
            _customerReviewService.DeactivateCustomerReview(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("like")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Like(string reviewId, string customerId)
        {
            _customerReviewService.LikeCustomerReview(reviewId, customerId);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("dislike")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Dislike(string reviewId, string customerId)
        {
            _customerReviewService.DislikeCustomerReview(reviewId, customerId);

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
