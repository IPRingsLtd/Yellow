using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace _360Appraisal.Models
{
    public enum FeedbackTypes { Self = 1, Hod, Subordinate, Peers, HR, Support };

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
        [Required, MinLength(4), MaxLength(6), Index("IX_Feedback", 1, IsUnique = true)]
        public string ReviewerID { get; set; }
        [Required, MinLength(4), MaxLength(6), Index("IX_Feedback", 2, IsUnique = true)]
        public string UserID { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required, MinLength(4), MaxLength(20), Index("IX_Feedback", 3, IsUnique = true)]
        public string FinancialYear { get; set; }
        [Required]
        public FeedbackTypes Type { get; set; }

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