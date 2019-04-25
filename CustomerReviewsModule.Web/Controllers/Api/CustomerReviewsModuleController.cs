using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviewsModule.Core.Model;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Web.Security;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Common;
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

        public IHttpActionResult SearchCustomerReviews([FromBody]CustomerReviewSearchCriteria criteria)

        {

            GenericSearchResult<CustomerReview> result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);

        }


        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs of reviews</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<CustomerReview>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult GetByIds([FromUri] string[] ids)
        {
            var result = _customerReviewService.GetByIds(ids);

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
        public IHttpActionResult Create([FromBody]CustomerReview customerReview)
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
        public IHttpActionResult Update([FromBody]CustomerReview[] customerReviews)
        {
            _customerReviewService.UpdateCustomerReviews(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }



        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs of reviews</param>
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

        /// <summary>
        /// Return concret customer like/dislike evaluation for concret review
        /// </summary>
        /// <param name="reviewId">review Id</param>
        /// <param name="customerId">customer Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("evaluetion")]
        [ResponseType(typeof(IEnumerable<CustomerReviewEvaluation>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult GetCustomerReviewsEvaluationsForCustomer([FromUri]string[] reviewIds, [FromUri]string customerId)
        {
            var result = _customerReviewEvaluationService.GetCustomerReviewsEvaluationsForCustomer(reviewIds, customerId);
            return Ok(result);
        }


        /// <summary>
        /// Add or update concret customer like/dislike evaluation for concret review
        /// </summary>
        /// <param name="evaluation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("evaluation")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public async Task<IHttpActionResult> SaveCustomerReviewEvaluation([FromBody]CustomerReviewEvaluation evaluation)
        {

            using (await AsyncLock.GetLockByKey(GetAsyncLockCustomerReviewKey(evaluation.CustomerReviewId)).LockAsync())
            {
                _customerReviewEvaluationService.SaveEvaluation(evaluation);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Return total rating of product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("productrating")]
        [ResponseType(typeof(IEnumerable<ProductRating>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult GetProductsRatings([FromUri]string[] productIds)
        {
            var productRating = _productRatiingService.GetProductsRatings(productIds);
            return Ok(productRating);
        }


        //get key for async lock
        private string GetAsyncLockCustomerReviewKey(object cartId)
        {
            throw new NotImplementedException();
        }


    }
}
