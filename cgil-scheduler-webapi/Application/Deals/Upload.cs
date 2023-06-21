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
    public class Upload
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
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if(request.Deal != null)
                {
                    Deal deal = context.Deal.Include(x => x.Attachments).FirstOrDefault(x => x.Code == Guid.Parse(request.Deal.code));

                    if (deal != null)
                    {   
                        foreach(var attachment in request.Deal.attachment)
                        {
                            if(deal.Attachments.Any(x => x.Name == attachment.Name))
                            {
                                Console.WriteLine($"Oops, a file with same name already exists");
                                continue;
                            }

                            deal.Attachments.Add(attachment);
                        }
                    }

                }

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}