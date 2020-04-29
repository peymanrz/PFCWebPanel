using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PFCWebPanel.ViewModels
{
  public class ProfileViewModel
    {
        [Display(Name ="شماره موبایل جدید  ")]
        [MaxLength(11,ErrorMessage ="مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        public string Mobile { get; set; }

        [Display(Name ="کلمه عبور جاری")]
        [MaxLength(50,ErrorMessage ="مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name ="کلمه عبور جدید")]
        [MaxLength(50,ErrorMessage ="مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="تکرار کلمه عبور جدید")]
        [MaxLength(50,ErrorMessage ="مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="کلمه های عبور با یکدیگر همخوانی ندارند")]
        public string RePassword { get; set; }

    }
}
