using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
namespace WebApplication2.Models
{
    public class NewProject
    {
        public bool DevelopSummaryExcel { get; set; }
        public bool DevelopReport { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectID { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportID { get; set; }
        public string? Email { get; set; }
        public IFormFile? ExcelFile { get; set; }
    }
}
