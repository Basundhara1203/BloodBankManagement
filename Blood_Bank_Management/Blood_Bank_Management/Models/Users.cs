using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blood_Bank_Management.Models
{
    public class Users
    {

        [Key]
        public int Id { get; set; }
        [Required]

        public string UserName{ get; set; }

        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{6,}", ErrorMessage = "Your password must be at least 6 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field is Required!")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field is Required!")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("RoleUser")]
        public int RoleId { get; set; }
        public virtual RoleUser RoleUser { get; set; }

    }
}