using System.ComponentModel.DataAnnotations;

namespace smart_bus_verification
{
    public class ExpendUpload
    {

        [Key]
        public int ExpenditureID { get; set; }

        
        public string Category { get; set; }

        
        public string Description { get; set; }

        
        public int Amount { get; set; }

       
        public DateTime DateOfExpenditure { get; set; }
       
        public IFormFile ReceiptPhoto { get; set; }

       
        public string ReceiptReference { get; set; }

        
 

}
}
