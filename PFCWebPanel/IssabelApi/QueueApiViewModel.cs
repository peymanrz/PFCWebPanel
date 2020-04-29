using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PFCWebPanel
{
    public class QueueApiViewModel
    {

        [Key]
        [Display(Name = "شماره صف")]
        //[CustomValidation(typeof(QueueApiViewModel), "ValidateQueueNumber")]
        //[StringLength(5, MinimumLength = 5, ErrorMessage = "{0} باید5 رقم باشد")]

        [Required(ErrorMessage = "لطفا {0} را وارد کنید !")]
        public int Extension { get; set; }


        [Display(Name = "نام صف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید !")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} باید حداقل 3 و حداکثر 10 کاراکتر باشد")]

        public string Name { get; set; }



        [Display(Name = "کلمه عبور صف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید !")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "{0} باید 4 رقم باشد")]

        public string Password { get; set; }


        //[Display(Name = "اعضای صف")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید !")]
        //public List<string> StaticMembers { get; set; }


        [Display(Name = "استراتژی تماس")]
        public string RingStrategy { get; set; }


        [Display(Name = "از قلم انداختن اوپراتورهای مشغول")]
        public string SkipBusyAgents { get; set; }


        [Display(Name = "اعلان پیوستن به صف")]
        public string JoinAnnounceId { get; set; }


        [Display(Name = "ظبط تماس")]
        public string MonitorFormat { get; set; }


        //[Display(Name = "MonitorJoin")]
        //public string MonitorJoin { get; set; }


        [Display(Name = "نوع ضبط تماس")]
        public string MonitorType { get; set; }


        [Display(Name = "حداکثر انتظار در صف")]
        public string MaxWait { get; set; }


        [Display(Name = "نوع حداکثر انتظار ")]
        public string TimeoutPriority { get; set; }         /////Max Wait Time Mode


        [Display(Name = "مدت زمان انتظار برای جواب اوپراتور")]
        public string Timeout { get; set; }           /////////// Agent Timeout


        [Display(Name = "مدت تلاش مجدد اوپراتور")]
        public string AgentRetry { get; set; }


        [Display(Name = "اعلام مدت زمان انتظار به اوپراتور")]
        public string ReportHoldTime { get; set; }


        [Display(Name = "توقف خودکار اوپراتور در صورت پاسخ ندادن")]
        public string AutoPause { get; set; }


        [Display(Name = "تاخیر در توقف خودکار")]
        [Range(0, 3600,
            ErrorMessage = "مقدار وارد شده باید بین {1} و {2} باشد")]
        public int AutoPauseDelay { get; set; }


        [Display(Name = "متوقف کردن اوپراتور در صورت اشغال")]
        public string AutoPauseIfBusy { get; set; }


        [Display(Name = "متوقف کردن اوپراتور در صورت دردسترس نبودن")]
        public string AutoPauseIfUnavailable { get; set; }


        [Display(Name = "حداکثر تعداد تماس گیرندگان")]
        public string MaxCallersWaiting { get; set; }


        [Display(Name = "پیوستن به صف خالی از اوپراتور")]
        public string JoinEmpty { get; set; }


        [Display(Name = "ترک کردن صف خالی از اوپراتور")]
        public string LeaveWhenEmpty { get; set; }


        [Display(Name = "زمان تکرار اعلام موقعیت و زمان تخمین زده شده")]
        public string AnnounceFrequency { get; set; }


        [Display(Name = "اعلام زمان تخمین زده شده")]
        public string AnnounceHoldtime { get; set; }


        [Display(Name = "اعلام موقعیت صف")]
        public string AnnouncePosition { get; set; }


         [Display(Name = "تنظیم مجدد آمار صف")]
        public string CronSchedule { get; set; }

         //public static ValidationResult ValidateQueueNumber(long pNewName, ValidationContext pValidationContext)
        //{
        // // test  //bool IsNotValid = (db.queues_config.Where(qe => qe.extension == pNewName.ToString()).Any() || db.devices.Where(qe => qe.id == pNewName.ToString()).Any()); // should implement here the database validation logic
        //    //if (IsNotValid)
        //    //{


        //    //    return new ValidationResult("شماره وارد شده تکراری است");
        //    //}
        //    //else
        //    //{
        //    //    return ValidationResult.Success;
        //    //}

        //    //new ValidationResult("CityCode not recognized", new List<string> { "extension" });
        //}
    }

}
