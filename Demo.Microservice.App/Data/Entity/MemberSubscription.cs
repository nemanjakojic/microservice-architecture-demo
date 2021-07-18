using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Microservice.App.Data.Entity
{
    [Table("MemberSubscription", Schema = "main")]
    public class MemberSubscription
    {
        [Key]
        public int ID { get; set; }
        public Guid ExternalId { get; set; }

        // This foreign key allows for clean delineation of domains and as such is acceptable
        public int AccountID { get; set; }

        // Account entity type should not be part of Subscription domain
        // public Account Account { get; set; }

        public DateTime? AvailableDate { get; set; }
        public int? AvailablePeriod { get; set; }
        public DateTime? ValidityStartDate { get; set; }
        public int? ValidityPeriod { get; set; }

        
        public DateTime? LastLogin { get; set; }
        public DateTime? FirstLogin { get; set; }
        public bool? Active { get; set; }
        public int? GradYear { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime? ResetDate { get; set; }

        // This entity type contains an object reference to InstitutionSubscription entity type
        // InstitutionSubscriptionId foreign key is redundant in that case.
        // public Guid? InstitutionSubscriptionId { get; set; }
        public InstitutionSubscription InstitutionSubscription { get; set; }


        [ForeignKey("ExamYearID")]
        public ExamYear ExamYear { get; set; }

        [ForeignKey("ExamBankID")]
        public ExamBank ExamBank { get; set; }
    }
}
