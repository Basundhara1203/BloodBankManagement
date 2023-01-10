using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blood_Bank_Management.Models
{
    public class DonorInfo
    {
        [Key]
        public int DonorId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string BloodType{ get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public string MedicalHist { get; set; }







        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }
    }
}