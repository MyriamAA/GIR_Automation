using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.IO;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Job2Controller : Controller
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
            public IFormFile ExcelFile { get; set; }
        }

        static HttpClient client = new HttpClient();
       
        readonly IBufferedFileUploadService _bufferedFileUploadService;
   public Job2Controller(IBufferedFileUploadService bufferedFileUploadService)
        {
            _bufferedFileUploadService = bufferedFileUploadService;
        }
      [HttpPost]
         public async Task<ActionResult> Index(string ProjectName, string ProjectID, string ReportTitle, string ReportID, string Email,  bool DevelopReport, bool DevelopSummaryExcel, IFormFile file)
        {
            Project project = new Project();
            try
            {     
                    project.ProjectName = "sdasd";
                    project.ProjectID = "sadasd";
                    project.ReportTitle = "sadasD";
                    project.ReportID = "dasdas";
                    project.Email = "Asdsds@dar.com";
                    project.DevelopReport = true;
                    project.DevelopSummaryExcel = true;
                    //ExcelFile = new byte[] { 123, 123, 123 };

                   
                    //byte [] bytes2 = new byte [] { 123, 123 };
                    //project.ExcelFile = bytes2;
                   
                    string projectAsString = JsonConvert.SerializeObject(project);

            
                    Console.WriteLine($"Project: {projectAsString}");
                    HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:8010/GIRReport", project);
                      response.EnsureSuccessStatusCode();

                   Console.WriteLine($"Response: {response}");
                    ViewBag.Message = "Hey";
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