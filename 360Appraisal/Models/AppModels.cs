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
        public string Description { get; set; }
        [InverseProperty("Section")]
        public ICollection<Topic> Topics { get; set; }
    }

    public class Topic : Base
    {
        public string Description { get; set; }
        [InverseProperty("Topics")]
        public virtual Section Section { get; set; }        
    }   
}