using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace WritingPlatform.Web.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Логин обязательно к заполнению")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки не менее 3-х символов и не более 20")]
        [Display(Name = "Логин")]
        public string LoginUser { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно к заполнению")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина строки не менее 6-ти символов и не более 20")]
        [Display(Name = "Пароль")]
        public string PasswordUser { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("PasswordUser", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Поле E-mail обязательно к заполнению")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки не менее 3-х символов и не более 20")]
        [Display(Name = "E-mail")]
        public string EmailUser { get; set; }

        [Display(Name = "Роль")]
        public int RoleId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool? IsDelete { get; set; }
    }
}