using System;
using System.ComponentModel.DataAnnotations;

namespace smart_bus_verification.Models
{
    public class Expenditures
    {
        [Key]
        public int ExpenditureID { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)] // Specify the type of data as Date
        public DateTime DateOfExpenditure { get; set; }

        [Required(ErrorMessage = "Receipt Image is required.")]
        public string ReceiptImage { get; set; }

        [Required(ErrorMessage = "Receipt reference is required.")]
        public string ReceiptReference { get; set; }

        // Additional tag helpers, assuming you are working with Razor views
    }
}