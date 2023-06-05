using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using Domain;
using System.Xml.Linq;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>()
                    {
                        new AppUser(){DisplayName="Bob", UserName="bob", Email="bob@test.com"},
                        new AppUser(){DisplayName="Tom", UserName="tom", Email="tom@test.com"},
                        new AppUser(){DisplayName="Jane", UserName="jane", Email="jane@test.com"},
                        new AppUser(){DisplayName="Dan Perta", UserName="dan", Email="dan.perta@gmail.com"}
                    };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            //if (context.Deal.Any()) return;

            var priorities = new List<Priority>()
            {
                new Priority()
                {
                    PrId = 1,
                    Code = 1,
                    Color = "#32131",
                    Text = "Low priority"
                },
                new Priority()
                {
                    PrId = 2,
                    Code = 2,
                    Color = "#32131",
                    Text = "Medium priority"
                },
                new Priority()
                {
                    PrId = 3,
                    Code = 3,
                    Color = "#32131",
                    Text = "High priority"
                }
            };

            var headQuarters = new List<Headquarter>()
            {
                new Headquarter()
                {
                    HdId = 1,
                    HdName="CGIL Legnaro",
                    HdProvince = "Padova",
                    HdRegion = "Veneto",
                    HdCity = "Legnaro",
                    HdZipCode = 35020,
                    HdAddress = "Via Antonio Vivaldi",
                    StreetNumber = 2
                },
                new Headquarter()
                {
                    HdId = 2,
                    HdName="CAAF CGIL Piove di Sacco",
                    HdProvince = "Padova",
                    HdRegion = "Veneto",
                    HdCity = "Piove di Sacco",
                    HdZipCode = 35028,
                    HdAddress = "Via Antonio Gramsci",
                    StreetNumber = 2
                },
                new Headquarter()
                {
                    HdId = 3,
                    HdName="CGIL Padova Centro",
                    HdProvince = "Padova",
                    HdRegion = "Veneto",
                    HdCity = "Padova",
                    HdZipCode = 35121 ,
                    HdAddress = "Via Angello Riello",
                    StreetNumber = 4
                }
            };

            var deals = new List<Deal>()
            {
                new Deal()
                {
                    DlId = 1,
                    DlPriorityId = priorities.Take(1).First().PrId,
                    DlText = "Amazon",
                    DlHdId = headQuarters.Skip(2).First().HdId,
                    AppUserId = userManager.Users.FirstOrDefault(x => x.Email.Equals("dan.perta@gmail.com")).Id,
                    DlStartDate = DateTime.Now,
                    DlEndDate = DateTime.Now.AddHours(5),
                },
                new Deal()
                {
                    DlId = 2,
                    DlPriorityId = priorities.Skip(1).Take(1).First().PrId,
                    DlText = "IKEA",
                    DlHdId = headQuarters.Skip(2).First().HdId,
                    AppUserId = userManager.Users.FirstOrDefault(x => x.Email.Equals("dan.perta@gmail.com")).Id,
                    DlStartDate = DateTime.Now.AddDays(1),
                    DlEndDate = DateTime.Now.AddDays(1).AddHours(5),
                }
            };

            var assignees = new List<Assignee>()
            {
                new Assignee()
                {
                    AeId = 1,
                    AeUsId = userManager.Users.FirstOrDefault(x => x.Email.Equals("dan.perta@gmail.com")).Id
                }
            };

            var dealAssignee = new List<DealAssigne>()
            {
                new DealAssigne()
                {
                    DaDlId = deals.FirstOrDefault().DlId,
                    DaAeId = assignees.FirstOrDefault().AeId,
                }
            };

            //await context.Priority.AddRangeAsync(priorities);
            //await context.Headquarter.AddRangeAsync(headQuarters);
            //await context.Assignee.AddRangeAsync(assignees);
            await context.Deal.AddRangeAsync(deals);
            //await context.DealAssigne.AddRangeAsync(dealAssignee);
            await context.SaveChangesAsync();
        }
    }
}