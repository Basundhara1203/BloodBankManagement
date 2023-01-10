using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blood_Bank_Management.Models
{
    public class RoleUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }

    }
}