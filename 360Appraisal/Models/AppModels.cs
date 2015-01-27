using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _360Appraisal.Models
{
    public class Base
    {
        [Required]
        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime UpdatedAt { get; set; }
        [Timestamp]
        [ScaffoldColumn(false)]
        public Byte[] TimeStamp { get; set; }
        [Required]
        [ScaffoldColumn(false)]
        public bool ActiveFlag { get; set; }
        [Key]
        [Required]
        [MaxLength(40)]
        [ScaffoldColumn(false)]
        public string Key { get; set; }
    }

    public class Section : Base
    {
        [Required]
        public string Description { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }

    public class Topic : Base
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public virtual Section Section { get; set; }

        public ICollection<Question> Questions { get; set; }
    }

    public class Question : Base
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public virtual Topic Topic { get; set; }        
    }

    public class Feedback : Base
    {       
        [Required]
        [MinLength(4)]
        [MaxLength(6)]
        public string ReviewerID { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(6)]
        public string UserID { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required]
        public string FinancialYear { get; set; }
        public string Comments { get; set; }

        public ICollection<Score> Scores { get; set; }
    }

    public class Score : Base
    {
        [Required]
        public virtual Question Question { get; set; }           
        [Required]
        public int Points { get; set; }
        [Required]
        public virtual Feedback Feedback { get; set; }
    }   

}