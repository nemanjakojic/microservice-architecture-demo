using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Microservice.App.Data.Entity
{
    [Table("InstitutionSubscription", Schema = "dbo")]
    public class InstitutionSubscription
    {
        [Key]
        public Guid InstitutionSubscriptionId { get; set; }
        public Guid InstitutionNodeId { get; set; }
        public short ValidityPeriod { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int ExamBankID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? NumMemberSubscription { get; set; }
        public bool? AssessmentOnly { get; set; }
        public int ExamYearID { get; set; }
        public double? Price { get; set; }
        public int? TaxZipCode { get; set; }

        // NotMapped properties indicate flaws in OR mapping (candidate for sw. arch. discussions)
        //[NotMapped]
        //public int AssignedMembers { get; set; }

        //[NotMapped]
        //public string ExamBankName { get; set; }

        [ForeignKey("ExamBankID")]
        public ExamBank ExamBank { get; set; }

        [ForeignKey("ExamYearID")]
        public ExamYear ExamYear { get; set; }

        // [ForeignKey("InstitutionNodeId")]
        // public InstitutionNode InstitutionNode { get; set; }

        // This might be a potentially "expensive" mapping (candidate for sw. arch. discussions)
        //[ForeignKey("InstitutionSubscriptionId")]
        //public ICollection<MemberSubscription> MemberSubscriptions { get; set; }
    }
}
