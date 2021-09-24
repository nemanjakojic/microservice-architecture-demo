using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Microservice.App.Data.Entity
{
    [Table("QuestionBank", Schema = "demo")]
    public class QuestionBank
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
