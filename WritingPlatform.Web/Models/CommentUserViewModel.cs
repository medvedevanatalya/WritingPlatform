using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WritingPlatform.Web.Models
{
    public class CommentUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Пользователь")]
        public int UserId { get; set; }

        [Display(Name = "Название произведения")]
        public int WriterWorkId { get; set; }

        [Required(ErrorMessage = "Поле Комментарий обязательно к заполнению")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Комментарий должен содержать миниму 1 символ")]
        [Display(Name = "Комментарий")]
        public string CommentsText { get; set; }
    }
}