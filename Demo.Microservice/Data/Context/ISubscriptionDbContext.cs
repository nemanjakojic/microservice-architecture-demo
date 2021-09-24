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
        DbSet<StudentSubscription> StudentSubscription { get; set; }
        DbSet<QuestionBank> QuestionBank { get; set; }
        DbSet<InstitutionSubscription> InstitutionSubscription { get; set; }
    }
}
