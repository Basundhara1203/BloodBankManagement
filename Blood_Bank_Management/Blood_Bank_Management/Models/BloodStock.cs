using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blood_Bank_Management.Models
{
    public class BloodStock
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(12)]
        public string BloodType { get; set; }
        [Required]
        public int Stock { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }
        [ForeignKey("HospInfo")]
        public int HospId { get; set; }
        public virtual HospInfo HospInfo { get; set; }

    } 
}