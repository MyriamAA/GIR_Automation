using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using WebApplication2.Interfaces;
using Newtonsoft.Json;
using System.Text;
using System;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Controllers
{
    [Route("newjob")]
    public class NewJobController : Controller
    {

        static readonly HttpClient client = new HttpClient();

        readonly IBufferedFileUploadService _bufferedFileUploadService;

        private IWebHostEnvironment webHostEnvironment;

        public string status;
        public NewJobController (IWebHostEnvironment _webHostEnvironment, IBufferedFileUploadService bufferedFileUploadService)
        {
            webHostEnvironment = _webHostEnvironment;
            _bufferedFileUploadService = bufferedFileUploadService;

        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View("Index", new NewProject());
        }

        [Route("save")]
        [HttpPost]

        // put async 
        // Task<IActionResult> instead of IActionResult
        async public Task<IActionResult> Save(NewProject project, IFormFile excelFile)
        {
            // new 
            string path = "";
            if (excelFile == null || excelFile.Length == 0)
            {
                return Content("File not selected");
            }
            else
            {
                //  var path = Path.Combine(this.webHostEnvironment.WebRootPath, "files", excelFile.FileName);
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "files"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var fileStream = new FileStream(Path.Combine(path, excelFile.FileName), FileMode.Create))
                {
                    await excelFile.CopyToAsync(fileStream);
                }

                //var stream = new FileStream(path, FileMode.Create);
                //excelFile.CopyToAsync(stream);
                
                
                //project.ExcelFile = excelFile.FileName;
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // without the bytes sending it as a project didn't work

                    string projectAsString = JsonConvert.SerializeObject(project);

                    // without the bytes sending as string didn't work
                    using var fileStream2 = excelFile.OpenReadStream();
                    byte[] bytes = new byte[excelFile.Length];
                    fileStream2.Read(bytes, 0, (int)excelFile.Length);
                    // fix string put values in between double quotations
                    status = @"{""ProjectName"": ";
                    status += $@"""{project.ProjectName}""" + @", ""ProjectID"": ";
                    status += $@"""{project.ProjectID}""" + @", ""ReportTitle"": ";
                    status += $@"""{project.ReportTitle}""" + @", ""ReportID"": ";
                    status += $@"""{project.ReportID}""" + @", ""Email"": ";
                    status +=  $@"""{project.Email}""" + @", ""DevelopSummaryExcel"": ";
                    status +=  $@"""{project.DevelopSummaryExcel.ToString()}""" + @", ""DevelopReport"": ";
                    status +=  $@"""{project.DevelopReport.ToString()}""" + @", ""ExcelFile"": ";
                    status +=  $@"""{Encoding.Default.GetString(bytes)}""" + " }";

                    //Console.Write("string: " + status);
                   // JObject json = JObject.Parse(status);


                    //InnerProject p = new InnerProject();

                    //p.ProjectName = project.ProjectName;
                    //p.ProjectID = project.ProjectID;
                    //p.ReportTitle = project.ReportTitle;
                    //p.ReportID = project.ReportID;
                    //p.DevelopReport = project.DevelopReport;
                    //p.DevelopSummaryExcel = project.DevelopSummaryExcel;
                    //p.Email = project.Email;
                    //byte[] newbyte = new byte[] { 123, 123 };
                    //p.fileBytes = newbyte;
                    
                //status = json.ToString();
                //try
                //{
               // HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:8010/GIRReport", p);

                    //status = response.StatusCode.ToString();
                //    status = response.StatusCode.ToString();
                //}
                //catch (Exception e)
                //{
                //    status = "no" + e.Message; 
                //
                }catch (Exception e)
                {
                    status = e.Message;
                    Console.WriteLine($"exception {e}");
                }
            }
            ViewBag.project = project;
            ViewBag.answer = status; // working when sending str with an excel file as input
           
            return View("Success");
        }

    }
}
    

