using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mvcForumapp.Models
{
    public class Replys
    {
        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Required(ErrorMessage = "Content required")]
        [StringLength(Int32.MaxValue, MinimumLength = 10)]
        public string ReplyContent { get; set; }
    }
}