using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Project
    {
        [Required]
        [Display(Name = "Would you like to develop an excel summary?")]
        public bool DevelopSummaryExcel { get; set; }

        [Required]
        [Display(Name = "Would you like to develop a report?")]
        public bool DevelopReport { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string? ProjectName { get; set; }

        [Required]
        [Display(Name = "Project ID")]
        public string? ProjectID { get; set; }

        [Required]
        [Display(Name = "Report Title")]
        public string? ReportTitle { get; set; }

        [Required]
        [Display(Name = "Report ID")]
        public string? ReportID { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Enter the excel file")]
        public IFormFile? ExcelFile { get; set; }
    }
}
