using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mvcForumapp.Models
{
    public class Questions
    {
        [Required(ErrorMessage = "Title required")]
        [Display(Name = "Enter Title")]
        [StringLength(20, MinimumLength = 4)]
        public string TopicTitle { get; set; }
        [Required(ErrorMessage = "Description required")]
        [Display(Name = "Enter Description")]
        [StringLength(20, MinimumLength = 10)]
        public string TopicDescription { get; set; }
        [Required(ErrorMessage = "Content required")]
        [StringLength(Int32.MaxValue, MinimumLength = 10)]
        public string TopicContent { get; set; }
    }
}