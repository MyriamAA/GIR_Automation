using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;


[Route("api/[controller]")]
[ApiController]
public class FileAPIController : ControllerBase
{
    private IWebHostEnvironment Environment;
    static string str = "";
    private class Project
    {
        public string? DevelopSummaryExcel { get; set; }
        public string? DevelopReport { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectID { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportID { get; set; }
        public string? Email { get; set; }
        public IFormFile? ExcelFile { get; set; }
    }
    public FileAPIController(IWebHostEnvironment _environment)
    {
        Environment = _environment;
        str = "Sample response";
    }
    [Route("UploadFiles")]
    [HttpPost]
     public async Task<IActionResult> UploadFiles()
    {
        //Create the Directory.
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads\\");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //Fetch the File.
        IFormFile postedFile = Request.Form.Files[0];

        //Fetch the File Name.
        string ProjectName = Request.Form["ProjectName"] + Path.GetExtension(postedFile.FileName);
        string ProjectID = Request.Form["ProjectID"];
        string ReportTitle = Request.Form["ReportTitle"];
        string ReportID = Request.Form["ReportID"];
        string DevelopSummaryExcel = "true";
        string DevelopReport = "true";
        string Email = Request.Form["Email"];

        Project project = new Project
        {
            ProjectName = ProjectName,
            ProjectID = ProjectID,
            ReportTitle = ReportTitle,
            ReportID = ReportID,
            Email = Email,
            DevelopReport = DevelopReport,
            DevelopSummaryExcel = DevelopSummaryExcel,
           ExcelFile = postedFile
        };
        //Save the File.
        using (FileStream stream = new FileStream(Path.Combine(path, ProjectName), FileMode.Create))
        {
            postedFile.CopyTo(stream);
        }
        var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        var url = "http://localhost:8010/GIRReport";


        using (var httpClient = new HttpClient())
        {
         var response = await httpClient.PostAsJsonAsync(url, project);
            str = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
                {
                
                var id = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Success");
            }
            
        }        


        //Send OK Response to Client.
        return Ok(str);
    }
    public static byte[] ReadFully(Stream input)
    {
        using (var ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }

    [Route("UploadFiles2")]
    [HttpPost]
    public async Task<IActionResult> UploadFiles2()
    {
        //Create the Directory.
      string path = Path.Combine(this.Environment.WebRootPath, "Uploads2\\");
      if (!Directory.Exists(path))
      {
          Directory.CreateDirectory(path);
      }

        //Fetch the File.
      IFormFile postedFile = Request.Form.Files[0];

      string ProjectName = Request.Form["ProjectName"] + Path.GetExtension(postedFile.FileName);
      string ProjectID = Request.Form["ProjectID"];
      string ReportTitle = Request.Form["ReportTitle"];
      string ReportID = Request.Form["ReportID"];
      string DevelopSummaryExcel = "true";
      string DevelopReport = "true";
      string Email = Request.Form["Email"];

      Project project = new Project
      {
          ProjectName = ProjectName,
          ProjectID = ProjectID,
          ReportTitle = ReportTitle,
          ReportID = ReportID,
          Email = Email,
          DevelopReport = DevelopReport,
          DevelopSummaryExcel = DevelopSummaryExcel,
          ExcelFile = postedFile
      };
        //Save the File.
      var client = new HttpClient();
      // client.DefaultRequestHeaders.Add("Accept", "application/json");
      var url = "http://localhost:8010/GIRReport";

   	  string status = @"{ProjectName: 'projectProjectName',ProjectID: 'project.ProjectID',ReportTitle: 'project.ReportTitle',ReportID: 'project.ReportID',Email: 'project.Email',DevelopSummaryExcel: 'project.DevelopSummaryExcel',DevelopReport: 'project.DevelopReport'}";

         StringContent stringContent = new StringContent(status);          
         MultipartContent multipartContent = new MultipartContent();
          multipartContent.Add(stringContent);

        using (FileStream stream = new FileStream(Path.Combine(path, postedFile.FileName), FileMode.Create))
        {
            postedFile.CopyTo(stream);
            ByteArrayContent byteArrayContent = new ByteArrayContent(ReadFully(stream));//MemoryStream
              multipartContent.Add(byteArrayContent);
              stream.Close();
        }
            try
            {
           

              // Send JOBject using jsonasync
		
              //  string status = @"{ProjectName: ";
              //       status += $@"""{project.ProjectName}""" + @", ProjectID: ";
              //       status += $@"""{project.ProjectID}""" + @", ReportTitle: ";
              //       status += $@"""{project.ReportTitle}""" + @", ReportID: ";
              //       status += $@"""{project.ReportID}""" + @", Email: ";
              //       status +=  $@"""{project.Email}""" + @", DevelopSummaryExcel: ";
              //       status +=  $@"""{project.DevelopSummaryExcel.ToString()}""" + @", ""DevelopReport"": ";
              //       status +=  $@"""{project.DevelopReport.ToString()}""";
                  
                string p = @"{ProjectName:'Name'}";

                var jsonString = JsonConvert.SerializeObject(status);
                var content = new StringContent(jsonString);
              
                // StringContent stringContent = new StringContent(JsonConvert.SerializeObject(project)); // one object 
              
              
                //multipartContent.Add(stringContent);
                DateTime start = DateTime.Now;

                try {
                
                using (var client2 = new HttpClient()){
                  using (var multipartFormDataContent = new MultipartFormDataContent()){
                      var values = new []{
                        new KeyValuePair<string,string>("ProjectName","Name1"),
                        new KeyValuePair<string, string>("ProjectID","ID"),
                        new KeyValuePair<string, string>("ReportID", "IDR"),
                        new KeyValuePair<string, string>("ReportTitle","Title"),
                        new KeyValuePair<string, string>("Email","email"),
                        new KeyValuePair<string, string>("DevelopSummaryExcel","true"),
                        new KeyValuePair<string, string>("DevelopReport","true")
                      };

                      foreach (var keyValuePair in values){
                        multipartFormDataContent.Add(new StringContent(keyValuePair.Value), String.Format("\"{0}\"", keyValuePair.Key));
                       // multipartFormDataContent.Add(byteArrayContent);
                       string path2 =Path.Combine(this.Environment.WebRootPath, "Uploads2\\") + postedFile.FileName ;
                      //   multipartFormDataContent.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(postedFile.FileName)),
                      //    '"' + "ExcelFile" + '"', 
                      // '"' + postedFile.FileName + '"');

                        var result = client2.PostAsync(url, multipartFormDataContent).Result;

                        str = result.StatusCode.ToString();
                      }
                  }
                }
                } catch(Exception e){
                  string s = e.Message;
                  Console.WriteLine(e.Message);
                }

                try {
                JObject json = JObject.Parse(status);
                HttpResponseMessage response = await client.PostAsync(url, multipartContent); // takes url and jobject
                str = response.StatusCode.ToString();
                }
                catch(Exception e){
                    Console.Write("Exception" + e.Message);
                }
                // str = response.StatusCode.ToString();
                // if (!response.IsSuccessStatusCode) {
                //     str = "Bad";
                //     return BadRequest();
                // }
                // str = "Good";
                // string results = await response.Content.ReadAsStringAsync();
            }

            catch(Exception e)
            {
                Console.Write($"exception {e}");
            }
        
      
        //Send OK Response to Client.
        return Ok(str);
    }

}