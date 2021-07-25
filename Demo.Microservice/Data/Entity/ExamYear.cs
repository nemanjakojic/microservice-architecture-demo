using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Microservice.App.Data.Entity
{
    [Table("ExamYear", Schema = "main")]
    public partial class ExamYear
    {
        [Key]
        public int ID { get; set; }
        public Guid InstitutionNodeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Start { get; set; }
        public int? Finish { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
