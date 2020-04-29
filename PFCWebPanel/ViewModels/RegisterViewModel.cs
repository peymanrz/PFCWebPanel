using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PFCWebPanel.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "شماره موبایل"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(11, ErrorMessage = "مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        public string Mobile { get; set; }
        [Display(Name = "نام کاربر"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(50, ErrorMessage = "مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        public string Name { get; set; }
        [Display(Name = "کلمه عبور"), MaxLength(50, ErrorMessage = "مقدار {0}  نمی تواند بیشتر از {1} باشد"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), DataType((DataType)DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور"), MaxLength(50, ErrorMessage = "مقدار {0}  نمی تواند بیشتر از {1} باشد"), DataType((DataType)DataType.Password), Compare("Password", ErrorMessage = "کلمه های عبور با یکدیگر همخوانی ندارند")]
        public string Repassword { get; set; }

    }
}
