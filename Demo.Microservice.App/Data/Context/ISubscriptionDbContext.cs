using Demo.Microservice.App.Data.Entity;
using Demo.Microservice.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.App.Data.Context
{
    public interface ISubscriptionDbContext : IApplicationDbContext
    {
        DbSet<MemberSubscription> MemberSubscription { get; set; }
        DbSet<ExamBank> ExamBank { get; set; }
        DbSet<InstitutionSubscription> InstitutionSubscription { get; set; }
        DbSet<ExamYear> ExamYear { get; set; }
    }
}
