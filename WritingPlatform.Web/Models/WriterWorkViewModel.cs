using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WritingPlatform.Web.Models
{
    public class WriterWorkViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Название произведения обязательно к заполнению")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки не менее 3-х символов и не более 50")]
        [Display(Name = "Название произведения")]
        public string TitleWork { get; set; }

        [Required(ErrorMessage = "Поле Текст произведения обязательно к заполнению")]
        [Display(Name = "Текст произведения")]
        public string WriterWorkText { get; set; }

        [Display(Name = "Жанр")]
        public int GenreId { get; set; }

        [Display(Name = "Пользователь")]
        public int UserId { get; set; }

        [Display(Name = "Дата публикации")]
        public DateTime? PublicationDate { get; set; }
    }
}