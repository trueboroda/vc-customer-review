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
    public class CustomerReviewsModuleController : ApiController
    {

        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;
        private readonly ICustomerReviewEvaluationService _customerReviewEvaluationService;
        private readonly IProductRatingService _productRatiingService;


        public CustomerReviewsModuleController(ICustomerReviewSearchService customerReviewSearchService, ICustomerReviewService customerReviewService, ICustomerReviewEvaluationService customerReviewEvaluationService, IProductRatingService productRatingService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
            _customerReviewEvaluationService = customerReviewEvaluationService;
            _productRatiingService = productRatingService;
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
        ///  Create new customer review
        /// </summary>
        /// <param name="customerReviews">Customer review</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(CustomerReview))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewCreate)]
        public IHttpActionResult Create(CustomerReview customerReview)
        {
            var review = _customerReviewService.CreateCustomerReview(customerReview);
            return Ok(review);
        }



        /// <summary>
        ///  update existing customer review
        /// </summary>
        /// <param name="customerReview">Customer review</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            _customerReviewService.UpdateCustomerReviews(customerReviews);
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
        [Route("evaluetion")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult GetCustomerReviewEvaluationForCustomer(string reviewId, string customerId)
        {
            var result = _customerReviewEvaluationService.GetCustomerReviewEvaluationForCustomer(reviewId, customerId);
            return Ok(result);
        }



        [HttpPost]
        [Route("evaluation")]
        public IHttpActionResult SaveCustomerReviewEvaluation(CustomerReviewEvaluation evaluation)
        {

            _customerReviewEvaluationService.SaveEvaluation(evaluation);

            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpGet]
        [Route("productrating")]
        [ResponseType(typeof(ProductRating))]
        public IHttpActionResult GetProductRating([FromUri]string productId)
        {
            var productRating = _productRatiingService.GetProductRating(productId);
            return Ok(productRating);
        }




    }
}
