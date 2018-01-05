using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMG.Model
{
    public class RegisterView
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "帳號")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "帳號至少要輸入8字元")]
        //[Remote("CheckAccount", "Remote", ErrorMessage = "帳號已重複")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密碼需介於6~20字元")]
        public string Password { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string Name { get; set; }

        [DisplayName("性別")]
        [Required(ErrorMessage = "請輸入性別")]
        public string Sex { get; set; }

        [DisplayName("出生日期")]
        [Required(ErrorMessage = "請輸入出生日期")]
        public DateTime Birth { get; set; }

        [DisplayName("血型")]
        public string Blood { get; set; }

        [DisplayName("身高(Cm)")]
        public float Height { get; set; }

        [DisplayName("體重(Kg)")]
        public float Weight { get; set; }

        [DisplayName("體脂率")]
        [Required(ErrorMessage = "請輸入體脂率")]
        public float Bodyfat { get; set; }

        [DisplayName("基礎代謝率")]
        [Required(ErrorMessage = "請輸入基礎代謝率")]
        public float BMR { get; set; }

        [DisplayName("病歷")]
        public string Disease { get; set; }

        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("手機號碼")]
        public string Phone { get; set; }

        [DisplayName("積分")]
        public int Score { get; set; }

        [DisplayName("大頭貼")]
        public string Img { get; set; }

        public string Url { get; set; }

        [DisplayName("檔案類型")]
        public string Type { get; set; }

        public int Size { get; set; }

        [DisplayName("1.您平常是否有控制飲食的習慣?(單選)")]
        public string Control { get; set; }

        [DisplayName("2.您平常吃水果的習慣為何?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Fruit { get; set; }

        [DisplayName("3.您的消化能力為何?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Ability { get; set; }

        [DisplayName("4.您用餐是否定時定量?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Ontime { get; set; }

        [DisplayName("5.您平常吃多少用油炸的、或用油煎的食物?(1份以半碗計算)(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Fry { get; set; }

        [DisplayName("6.您平常有沒有喝含糖飲料?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Drink { get; set; }

        [DisplayName("7.您的睡眠品質如何?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Quality { get; set; }

        [DisplayName("8.您每週運動的次數(單選)")]
        [Required(ErrorMessage = "請輸入運動次數")]
        public string Sportnum { get; set; }

        [DisplayName("9.您每次運動的時間(單選)")]
        [Required(ErrorMessage = "請輸入運動的時間")]
        public string Sporttime { get; set; }

        [DisplayName("10.您喜愛的運動項目是什麼?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Sports { get; set; }

        [DisplayName("11.您健身最大的動機為?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Motivation { get; set; }

        [DisplayName("12.您在健身中屬於哪個等級?")]
        [Required(ErrorMessage = "請回答")]
        public string Grade { get; set; }

        [DisplayName("13.您注重的肌肉部位?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Place { get; set; }

        [DisplayName("14.男/女最想看異性的哪個肌肉部位?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Look { get; set; }

        [DisplayName("15.下列哪個選項能讓您更有意欲持續健身?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Want { get; set; }

        [DisplayName("16.您在健身時，對自己的要求為何?(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Request { get; set; }

        [DisplayName("17.未來是否願意增加健身計劃(單選)")]
        [Required(ErrorMessage = "請回答")]
        public string Increase { get; set; }
    }
}
