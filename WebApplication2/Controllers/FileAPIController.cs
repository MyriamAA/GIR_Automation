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
        IFormFile postedFile = Request.Form.Files[0];
        string ProjectName = Request.Form["ProjectName"] + Path.GetExtension(postedFile.FileName);
        string ProjectID = Request.Form["ProjectID"].ToString();
        string ReportTitle = Request.Form["ReportTitle"].ToString();
        string ReportID = Request.Form["ReportID"].ToString();
        string DevelopSummaryExcel = "true";
        string DevelopReport = "true";
        string Email = Request.Form["Email"].ToString();

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
      IFormFile postedFile = Request.Form.Files[0];
      string ProjectName = Request.Form["ProjectName"].ToString() + Path.GetExtension(postedFile.FileName);
      string ProjectID = Request.Form["ProjectID"].ToString();
      string ReportTitle = Request.Form["ReportTitle"].ToString();
      string ReportID = Request.Form["ReportID"].ToString();
      string DevelopSummaryExcel = "true";
      string DevelopReport = "true";
      string Email = Request.Form["Email"].ToString();

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
      var client = new HttpClient();
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
            try{
          
              var jsonString = JsonConvert.SerializeObject(status);
              var content = new StringContent(jsonString);
            
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
                      string path2 =Path.Combine(this.Environment.WebRootPath, "Uploads2\\") + postedFile.FileName ;
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
                HttpResponseMessage response = await client.PostAsync(url, multipartContent);
                str = response.StatusCode.ToString();
                }
                catch(Exception e){
                    Console.Write("Exception" + e.Message);
                }
            }

            catch(Exception e)
            {
                Console.Write($"exception {e}");
            }
        return Ok(str);
    }

}