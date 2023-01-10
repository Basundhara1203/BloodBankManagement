using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blood_Bank_Management.Models
{
    public class HospInfo
    {
        [Key]
        public int HospId { get; set; }
        [Required]

        public string HospName { get; set; }
        [Required]
        public string RegNo { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactNo { get; set; }
        






        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }

    }
}