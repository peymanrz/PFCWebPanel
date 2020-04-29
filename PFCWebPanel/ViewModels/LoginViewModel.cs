using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PFCWebPanel.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "شماره موبایل"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(11, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(50, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} کاراکتر باشد"), DataType((DataType)DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
