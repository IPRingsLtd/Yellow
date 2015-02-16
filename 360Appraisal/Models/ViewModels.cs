using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Appraisal.Models
{
    public class Department
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class Position
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    
    public class TResponse<T>
    {
        public string Type { get; set; }
        public string Classname { get; set; }
        public Exception Error { get; set; }
        public string StatusCode { get; set; }
        public string RequestUrl { get; set; }
    }

    public class ResponseList<T> : TResponse<T>
    {
        public List<T> List { get; set; }
        public int Count { get; set; }
    }

    public class ResponseValue<T> : TResponse<T>
    {
        public T Value { get; set; }
    }

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

    public class BaseLoginViewModel
    {
        [Required]
        [Display(Name = "Employee Code")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginViewModel : BaseLoginViewModel
    {
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string _ReturnUrl { get; set; }
    }
}