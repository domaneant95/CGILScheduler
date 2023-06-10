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
                    foreach(var assigne in deal.Assignees)
                    {
                        result.Add(new DealDto()
                        {
                            StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            EndDate = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss"),
                            AllDay = false,
                            AssigneeId = assigne.Id,
                            Description = deal.Description,
                            PriorityId = deal.Priority.Id,
                            ReccurenceRule = deal.ReccurenceRule,
                            RecurrenceException = deal.RecurrenceException,
                            Text = deal.Text,
                            RoomId = deal.Headquarter.Id
                        });
                    }
                }

                return result;
            }
        }
    }
}