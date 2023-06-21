using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NPOI.SS.Formula.Functions;

using Domain;
using Domain.DTOs;
using Persistence;
using Application.Activities;
using Domain.Dto;


namespace API.Controllers
{
    [AllowAnonymous]
    public class DealsController : BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<ActionResult<List<DealDto>>> GetDeals()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")] //api/activities/dsadas
        public async Task<ActionResult<Deal>> GetDeal(int id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeal()
        {
            var form = this.Request.Form;
            var values = form["values"];
            var fo = System.Text.Json.JsonSerializer.Deserialize<DealDto>(values.ToString());
            return Ok(await Mediator.Send(new Create.Command { Deal = fo }));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> EditDeal(string code, DealDto dealDto)
        {
            dealDto.code = code;
            return Ok(await Mediator.Send(new Edit.Command { Deal = dealDto }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeal(int id, Deal activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Delete.Command { Deal = activity }));
        }

        [HttpPost("upload/{dealCode}")]
        public async Task<IActionResult> UploadDealDocs(string dealCode)
        {
            var formFileCollection = this.Request.Form.Files;

            DealDto deal = new DealDto();
            deal.code = dealCode;
            deal.attachment = new List<FileItem>();

            foreach (var formFile in formFileCollection)
            {
                using var file = formFile.OpenReadStream();
                using var memStream = new MemoryStream();
                file.CopyTo(memStream);

                deal.attachment.Add(new FileItem()
                {
                    File = memStream.ToArray(),
                    Size = formFile.Length,
                    Name = formFile.FileName,
                    Extension = Path.GetExtension(formFile.FileName),
                    Type = Path.HasExtension(formFile.FileName) ? FileType.File : FileType.Folder,
                    Created = DateTime.Now
                });
            }

            return Ok(await Mediator.Send(new Upload.Command { Deal = deal }));
        }
    }
}