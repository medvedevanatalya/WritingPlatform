using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WritingPlatform.Web.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Название жанра обязательно к заполнению")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки не менее 3-х символов и не более 50")]
        [Display(Name = "Название жанра")]
        public string NameGenre { get; set; }
    }
}