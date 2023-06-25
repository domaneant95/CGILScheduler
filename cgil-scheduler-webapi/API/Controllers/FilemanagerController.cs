using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Filemanager;

namespace API.Controllers
{
    [AllowAnonymous]
    public class FilemanagerController : BaseApiController
    {
        public FilemanagerController() 
        {
        }

        [HttpPost]
        public IActionResult Process(FileSystem itemInfo)
        {
            var result = DbFileProvider.Process(itemInfo);
            return Ok(result);
        }

        [HttpPost("upload")]
        public IActionResult ProcessUpload()
        {
            var formData = this.Request.Form;
            formData.TryGetValue("command", out var command);
            formData.TryGetValue("itemInfo", out var item);
            formData.TryGetValue("uploadInfo", out var upload);
            var file = this.Request.Form.Files[0];
            var itemInfo = System.Text.Json.JsonSerializer.Deserialize<Iteminfo>(item);
            var uploadInfo = System.Text.Json.JsonSerializer.Deserialize<UploadInfo>(upload);
            var memStream = new MemoryStream();
            file.OpenReadStream().CopyTo(memStream);

            var fileSystem = new FileSystem()
            {
                command = command,
                args = new Args()
                {
                    itemInfo = itemInfo,
                    fileData = new FileData()
                    {
                        size = file.Length,
                        lastModifiedDate = DateTime.Now,
                        name = file.FileName,
                        type = file.ContentType,
                        lastModified = DateTime.Now.Ticks,
                        file = memStream.ToArray()
                    }
                }
            };

            var result = DbFileProvider.Process(fileSystem);
            return Ok(result);
        }
    }
}