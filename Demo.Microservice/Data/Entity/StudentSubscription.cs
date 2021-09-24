using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Microservice.App.Data.Entity
{
    [Table("Subscription", Schema = "demo")]
    public class StudentSubscription
    {
        [Key]
        public int ID { get; set; }
        public int AccountID { get; set; }
        public DateTime? ValidityStartDate { get; set; }
        public int? ValidityPeriod { get; set; }        
        public bool? Active { get; set; }
        public int? GradYear { get; set; }
        public DateTime TimeStamp { get; set; }

        public InstitutionSubscription InstitutionSubscription { get; set; }

        [ForeignKey("QuestionBankID")]
        public QuestionBank QuestionBank { get; set; }
    }
}
