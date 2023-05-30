using MediatR;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Deal>>
        {
            //--
        }

        public class Handler : IRequestHandler<Query, List<Deal>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<Deal>> Handle(Query request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Handle request");
                return await this.context.Deal.ToListAsync();
            }
        }
    }
}