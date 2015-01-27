using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Appraisal.Models
{
    public class SectionViewModel
    {
        public SectionViewModel() { }
        public SectionViewModel(Section section)
        {
            this.Description = section.Description;
        }

        [Required]
        public string Description { get; set; }
    }

    public class SectionEditViewModel : SectionViewModel
    {
        public SectionEditViewModel() { }
        public SectionEditViewModel(Section section)
            : base(section)
        {
            this.Key = section.Key;
        }

        [Required]
        public string Key { get; set; }
    }

    public class SectionBaseViewModel
    {
        [Required]
        public string Section { get; set; }

        public SelectList Sections { get; set; }
    }

    public class TopicViewModel : SectionBaseViewModel
    {
        public TopicViewModel() { }
        public TopicViewModel(Topic topic)
        {
            this.Description = topic.Description;
        }

        [Required]
        public string Description { get; set; }
    }

    public class TopicEditViewModel : TopicViewModel
    {
        public TopicEditViewModel() { }
        public TopicEditViewModel(Topic topic)
            : base(topic)
        {
            this.Key = topic.Key;
        }

        [Required]
        public string Key { get; set; }
    }
}