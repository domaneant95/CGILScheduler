using MediatR;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Dto;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<DealDto>>
        {
            //--
        }

        public class Handler : IRequestHandler<Query, List<DealDto>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<DealDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Handle request");
                var deals = await this.context.Deal
                    .Include(x => x.AppUser)
                    .Include(x => x.Priority)
                    .Include(x => x.Priority)
                    .Include(x => x.Assignees)
                    .Include(x => x.Headquarter)
                    .ToListAsync();
              
    
                List<DealDto> result = new List<DealDto>();
                foreach(var deal in deals) 
                {
                    result.Add(new DealDto()
                    {
                        startDate = deal.DlStartDate,
                        endDate = deal.DlEndDate,
                        allDay = false,
                        assigneeId = deal.Assignees.Select(x => x.Id).ToArray(),
                        description = deal.Description,
                        priorityId =  new int[] { deal.Priority.Id },
                        reccurenceRule = deal.ReccurenceRule,
                        recurrenceException = deal.RecurrenceException,
                        text = deal.Text,
                        roomId = deal.Headquarter.Id 
                    });
                }

                return result;
            }
        }
    }
}