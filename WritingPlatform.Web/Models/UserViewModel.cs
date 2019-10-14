using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WritingPlatform.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Логин обязательно к заполнению")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки не менее 3-х символов и не более 20")]
        [Display(Name = "Логин")]
        public string LoginUser { get; set; }

        [Required(ErrorMessage = "Поле E-mail обязательно к заполнению")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки не менее 3-х символов и не более 20")]
        [Display(Name = "E-mail")]
        public string EmailUser { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно к заполнению")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина строки не менее 6-ти символов и не более 20")]
        [Display(Name = "Пароль")]
        public string PasswordUser { get; set; }

        [Display(Name = "Роль")]
        public int RoleId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool? IsDelete { get; set; }
    }
}