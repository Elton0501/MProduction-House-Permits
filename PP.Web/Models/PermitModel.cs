using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PP.Web.Models
{
    public class PermitModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string WhatsAppNumber { get; set; }
        [Required]
        public string Filmpermitservices { get; set; }
        [Required]
        public bool locationhunting { get; set; }
        public string additionalservices { get; set; }
        [Required]
        public string Assignment { get; set; }
        [Required]
        public DateTime Filmingdate { get; set; }
        [Required]
        public string FILMINGCITY { get; set; }
        [Required]
        public int days { get; set; }
        [Required]
        public int crew { get; set; }
        [Required]
        public string shoot { get; set; }
        public string remarks { get; set; }
        public bool GeneralPermit { get; set; }
        public bool RTAPermit { get; set; }
        public bool Catering { get; set; }
        public int? CateringCrew { get; set; }
        public bool HotelBooking { get; set; }
    }
}