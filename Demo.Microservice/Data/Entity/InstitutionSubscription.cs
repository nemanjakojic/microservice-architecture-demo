using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Microservice.App.Data.Entity
{
    [Table("InstitutionSubscription", Schema = "demo")]
    public class InstitutionSubscription
    {
        [Key]
        public Guid Id { get; set; }
        public Guid InstitutionId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime TimeStamp { get; set; }
        
        [ForeignKey("QuestionBankID")]
        public QuestionBank QuestionBank { get; set; }
    }
}
