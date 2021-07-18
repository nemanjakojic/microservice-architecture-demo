using Demo.Microservice.App.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Demo.Microservice.App.Data.Context
{
    public class SubscriptionDbContext : DbContext, ISubscriptionDbContext
    {
        public SubscriptionDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<MemberSubscription> MemberSubscription { get; set; }
        public DbSet<ExamBank> ExamBank { get; set; }
        public DbSet<InstitutionSubscription> InstitutionSubscription { get; set; }
        public DbSet<ExamYear> ExamYear { get; set; }
    }
}
