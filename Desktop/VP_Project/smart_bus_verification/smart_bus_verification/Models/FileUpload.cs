using Microsoft.AspNetCore.Http;
using smart_bus_verification.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace smart_bus_verification.Models
{
    public class FileUpload
    {


        [Key]
        public int StudentId { get; set; }

        public string Name { get; set; }
       
        public string Roll { get; set; }
      
        public string CNIC { get; set; }
        public string phonenumber { get; set; }
        public DateTime Card_issued_Date { get; set; }
        public string Department { get; set; }
        public string RouteNo { get; set; }
        public string PickUp { get; set; }
        public IFormFile photo { get; set; }
        public IFormFile Voucherphoto { get; set; }
        public bool? Availability { get; set; }
    }
}
