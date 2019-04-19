namespace CustomerReviewsModule.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewEvaluationAndProductRating_adding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerReviewEvaluation",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ReviewIsLiked = c.Boolean(nullable: false),
                        CustomerReviewId = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 64),
                        ModifiedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerReview", t => t.CustomerReviewId, cascadeDelete: true)
                .Index(t => new { t.CustomerReviewId, t.CustomerId }, unique: true);
            
            CreateTable(
                "dbo.CustomerReviewsProductRating",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 128),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            AddColumn("dbo.CustomerReview", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerReview", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerReview", "DislikeCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerReviewEvaluation", "CustomerReviewId", "dbo.CustomerReview");
            DropIndex("dbo.CustomerReviewEvaluation", new[] { "CustomerReviewId", "CustomerId" });
            DropColumn("dbo.CustomerReview", "DislikeCount");
            DropColumn("dbo.CustomerReview", "LikeCount");
            DropColumn("dbo.CustomerReview", "Rating");
            DropTable("dbo.CustomerReviewsProductRating");
            DropTable("dbo.CustomerReviewEvaluation");
        }
    }
}
