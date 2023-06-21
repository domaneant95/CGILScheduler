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
    }
}