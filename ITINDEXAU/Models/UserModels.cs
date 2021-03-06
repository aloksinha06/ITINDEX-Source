﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ITINDEXAU.Models
{
    //public class UserContext : DbContext
    //{
    //    public DbSet<Register> Users { get; set; }
    //}
    public class Register
    {
        [Required(ErrorMessage="Please Enter First Name")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Id")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Please Enter Valid Email Address")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(20, ErrorMessage = "Password Must be between 6 and 20 characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password",ErrorMessage="Password And Confirm Password Must Be Match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display (Name="Terms & Condition")]
        public string termscondition { get; set; }

        [Required(ErrorMessage = "Please Check Ters & Condition ")]
        [Display(Name="I agree to the")]
        public bool terms { get; set; }
    }
    public class Login
    {
        [Required(ErrorMessage = "Please Enter Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Emial Id")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Keep me logged in")]
        public bool RememberMe { get; set; }
    }
    public class Forgot
    {
        [Required(ErrorMessage = "Please Enter Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter Valid Email Address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
    public class Setting
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Office Phone No")]
        [Display(Name = "Office Phone")]
        public string OfficePhone { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Phone No")]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Please Enter Current Password")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }
        
        [Required(ErrorMessage = "Please Enter New Password")]
        [StringLength(20, ErrorMessage = "Password Must be between 6 and 20 characters", MinimumLength = 6)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Password And Confirm Password Must Be Match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
    public class ResetPassword
    {
        public int ID { get; set; }       
        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(20, ErrorMessage = "Password Must be between 6 and 20 characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "Password And Confirm Password Must Be Match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}