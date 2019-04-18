using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using CustomerReviewsModule.Data.Model;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace CustomerReviewsModule.Data.Repositories
{
    /// <summary>
    /// Repository for DAL operations over the customer review entities. Also Repository is EF data context for module.
    /// </summary>
    public class CustomerReviewRepository :
        EFRepositoryBase, ICustomerReviewRepository
    {


        public CustomerReviewRepository()
            : base("VirtoCommerce")
        {

        }



        public IQueryable<CustomerReviewEntity> CustomerReviews => GetAsQueryable<CustomerReviewEntity>();

        public IQueryable<CustomerReviewEvaluationEntity> CustomerReviewEvaluations => GetAsQueryable<CustomerReviewEvaluationEntity>();

        public IQueryable<ProductRatingEntity> ProductRatings => GetAsQueryable<ProductRatingEntity>();



        public CustomerReviewRepository(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, null, interceptors)
        {
            Configuration.LazyLoadingEnabled = false;
        }




        public void DeleteCustomerReviews(string[] ids)
        {
            var items = GetCustomerReviewsByIds(ids);
            foreach (var item in items)
            {
                Remove(item);
            }
        }



        public CustomerReviewEntity[] GetCustomerReviewsByIds(string[] ids)
        {
            return CustomerReviews.Where(x => ids.Contains(x.Id)).ToArray();
        }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<CustomerReviewEntity>()
                .ToTable("CustomerReview")
                .HasKey(x => x.Id);

            modelBuilder.Entity<CustomerReviewEvaluationEntity>()
                .ToTable("CustomerReviewEvaluation")
                .HasKey(x => x.Id);
            modelBuilder.Entity<CustomerReviewEvaluationEntity>()
                .HasRequired(x => x.CustomerReview)
                .WithMany(x => x.Evaluations)
                .HasForeignKey(x => x.CustomerReviewId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ProductRatingEntity>()
                .ToTable("CustomerReviewsProductRating")
                .HasKey(x => x.ProductId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
