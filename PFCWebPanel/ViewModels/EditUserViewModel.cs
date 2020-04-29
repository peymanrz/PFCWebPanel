    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    namespace PFCWebPanel.ViewModels
{


    public class EditUserViewModel
    {
        [Display(Name="شماره موبایل"), Required(ErrorMessage="مقدار {0} را وارد کنید"), MaxLength(11, ErrorMessage="مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        public string Mobile { get; set; }

        [Display(Name="نام کاربر"), MaxLength(50, ErrorMessage="مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        public string Name { get; set; }

        [Display(Name="کلمه عبور"), MaxLength(50, ErrorMessage="مقدار {0}  نمی تواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage="مقدار {0} را وارد کنید")]
        public int Id { get; set; }

        [Required(ErrorMessage="مقدار {0} را وارد کنید")]
        public int RoleId { get; set; }
    }
}

