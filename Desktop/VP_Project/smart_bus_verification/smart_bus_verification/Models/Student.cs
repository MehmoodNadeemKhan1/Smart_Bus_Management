using smart_bus_verification.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smart_bus_verification.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Roll { get; set; }

        public virtual ApplicationUser applicationUser { get; set; }

        public string CNIC { get; set; }
        public string Department { get; set; }
        public string RouteNo { get; set; }
        public string PickUp { get; set; }
        public string image { get; set; }
        public string phonenumber { get; set; }
        public DateTime Card_issued_Date { get; set; }
        public string Voucherimage { get; set; }
        public bool? Availability { get; set; }
    }
}
