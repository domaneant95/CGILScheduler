using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Domain;
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
        public async Task<IActionResult> CreateDeal(DealDto activity)
        {
            return Ok(await Mediator.Send(new Create.Command { Deal = activity }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditDeal(int id, Deal activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Deal = activity }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeal(int id, Deal activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Delete.Command { Deal = activity }));
        }
    }
}