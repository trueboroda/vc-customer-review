using System.Data.Entity;
using System.Linq;
using CustomerReviewsModule.Core.Model;
using VirtoCommerce.Platform.Data.Infrastructure;

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

        public CustomerReviewRepository(string nameOrConnectionString)
            : base(nameOrConnectionString, null, null)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<CustomerReviewEntity> CustomerReviews => GetAsQueryable<CustomerReviewEntity>();

        public void DeleteCustomerReviews(string[] ids)
        {
            var items = GetByIds(ids);
            foreach (var item in items)
            {
                Remove(item);
            }
        }

        public CustomerReviewEntity[] GetByIds(string[] ids)
        {
            return CustomerReviews.Where(x => ids.Contains(x.Id)).ToArray();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReviewEntity>()
                .ToTable("CustomerReview")
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}
