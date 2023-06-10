using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public DealDto Deal { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;   

            public Handler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var assignee = context.Assignee.Include(x => x.AppUser).FirstOrDefault(x => x.Id == request.Deal.AssigneeId);
                var user = assignee.AppUser;
                var headQuarter = context.Headquarter.FirstOrDefault(x => x.Id == request.Deal.RoomId);
                var priority = context.Priority.FirstOrDefault(x => x.Id == request.Deal.PriorityId);

                var deal = new Deal()
                {
                    DlStartDate = DateTime.Parse(request.Deal.StartDate.ToString()),
                    DlEndDate = DateTime.Parse(request.Deal.EndDate.ToString()),
                    AppUser = user,
                    Description = request.Deal.Description,
                    Text = request.Deal.Text,
                    Headquarter = headQuarter,
                    Priority = priority,
                    Assignees = new List<Assignee>() { assignee },
                    ReccurenceRule = request.Deal.ReccurenceRule,
                    RecurrenceException = request.Deal.RecurrenceException,
                };

                context.Deal.Add(deal);
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}