using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Deal>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Deal>
        {
            private readonly DataContext context;
            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Deal> Handle(Query request, CancellationToken cancellationToken)
            {
                return null;// await context.Deal.FindAsync(request.Id);
            }
        }
    }
}