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

        public DbSet<StudentSubscription> StudentSubscription { get; set; }
        public DbSet<QuestionBank> QuestionBank { get; set; }
        public DbSet<InstitutionSubscription> InstitutionSubscription { get; set; }
    }
}
