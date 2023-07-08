using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Filemanager;
using NPOI.HSSF.Record.Aggregates;
using NPOI.Util;
using System.Data;
using Org.BouncyCastle.Crypto.IO;

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

            var tempDir = Path.Combine(Environment.CurrentDirectory, "upload");

            if (uploadInfo.chunkIndex < uploadInfo.chunkCount)
            {
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }

                string filePath = Path.Combine(tempDir, $"{file.FileName}");  

                try
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        file.OpenReadStream().CopyTo(stream);
                    }
                }
                catch (IOException ex)
                {
                    throw ex;
                }

                if(uploadInfo.chunkIndex == uploadInfo.chunkCount - 1)
                {
                    return Ok(file);    
                }

                return Ok(file);
            }

            //string[] inputFilePaths = Directory.GetFiles(tempDir);
            //Console.WriteLine("Number of files: {0}.", inputFilePaths.Length);

            //MemoryStream outputStream = null;

            //using (outputStream = new MemoryStream())
            //{
            //    foreach (var inputFilePath in inputFilePaths)
            //    {
            //        using (var inputStream = System.IO.File.OpenRead(inputFilePath))
            //        {
            //            inputStream.CopyTo(outputStream);
            //        }

            //        Console.WriteLine("The file {0} has been processed.", inputFilePath);
            //    }
            //}

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
                        //file = outputStream.ToArray()
                    }
                }
            };

            var result = DbFileProvider.Process(fileSystem);
            return Ok(result);
        }
    }
}