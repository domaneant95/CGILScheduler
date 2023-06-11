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
                foreach(var assigneeId in request.Deal.assigneeId)
                {
                    var assignee = context.Assignee.Include(x => x.AppUser).FirstOrDefault(x => x.Id == assigneeId);
                    var user = assignee.AppUser;
                    var headQuarter = context.Headquarter.FirstOrDefault(x => x.Id == request.Deal.roomId);
                    var priority = context.Priority.FirstOrDefault(x => x.Id == (request.Deal.priorityId != null && request.Deal.priorityId.Length > 0 ? request.Deal.priorityId[0] : 1));

                    var deal = new Deal()
                    {
                        DlStartDate = DateTime.Parse(request.Deal.startDate.ToString()),
                        DlEndDate = DateTime.Parse(request.Deal.endDate.ToString()),
                        AppUser = user,
                        Description = request.Deal.description,
                        Text = request.Deal.text,
                        Headquarter = headQuarter,
                        Priority = priority,
                        Assignees = new List<Assignee>() { assignee },
                        ReccurenceRule = request.Deal.reccurenceRule,
                        RecurrenceException = request.Deal.recurrenceException,
                    };

                    context.Deal.Add(deal);
                }

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}