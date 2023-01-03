using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.IO;
namespace WebApplication2.Controllers
{
   

    public class JobController : Controller
    {

        private class Project
        {
            public bool DevelopSummaryExcel { get; set; }
            public bool DevelopReport { get; set; }
            public string? ProjectName { get; set; }
            public string? ProjectID { get; set; }
            public string? ReportTitle { get; set; }
            public string? ReportID { get; set; }
            public string? Email { get; set; }
            public byte[] ExcelFile { get; set; }
        }
        static readonly HttpClient client = new HttpClient();
       
        readonly IBufferedFileUploadService _bufferedFileUploadService;

        public JobController(IBufferedFileUploadService bufferedFileUploadService)
        {
            _bufferedFileUploadService = bufferedFileUploadService;
        }
        public IActionResult Index()
        {
            return View();
        }


//was task<ActionResult>
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file
            , bool DevelopSummaryExcel, bool DevelopReport, string ProjectName, string ProjectID, string ReportTitle, string ReportID, string Email
            )
        {
            Project project = new Project();

            using var fileStream = file.OpenReadStream();
             byte[] bytes = new byte[file.Length];
            fileStream.Read(bytes, 0, (int)file.Length);

            try
            {
               // if (await _bufferedFileUploadService.UploadFile(file))
               // {
                    Console.WriteLine($"FileName: {file.FileName}");           
                    project.ProjectName = "sdasd";
                    project.ProjectID = "sadasd";
                    project.ReportTitle = "sadasD";
                    project.ReportID = "dasdas";
                    project.Email = "Asdsds@dar.com";
                    project.DevelopReport = true;
                    project.DevelopSummaryExcel = true;
                    //ExcelFile = new byte[] { 123, 123, 123 };

                    byte [] bytes2 = new byte [] { 123, 123 };
                    project.ExcelFile = bytes;
                    //project.ExcelFile = bytes;
                    string projectAsString = JsonConvert.SerializeObject(project);

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
                    Console.WriteLine($"Project: {projectAsString}");
                    HttpResponseMessage response =await client.PostAsJsonAsync("http://localhost:8010/GIRReport", project);


                      Console.WriteLine($"Response: {response.StatusCode}");
                Console.WriteLine("ahs");
                    ViewBag.Message = "Local file upload successful: " + response.StatusCode +"\nproject: " + projectAsString;

               // }
               // else
               // {
               //     ViewBag.Message = "File Upload Failed";
               // }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed" + ex;
            }
            //return View();
            return View();
        }

    
    }

}