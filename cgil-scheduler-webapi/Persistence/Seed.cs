using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using Domain;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

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

            return;

            var danUser = userManager.Users.FirstOrDefault(x => x.DisplayName == "Dan Perta");

            var priorities = new List<Priority>()
            {
                new Priority()
                {
                    Code = 1,
                    Color = "green",
                    Text = "Low priority"
                },
                new Priority()
                {
                    Code = 2,
                    Color = "orange",
                    Text = "Average priority"
                },
                new Priority()
                {
                    Code = 3,
                    Color = "orange",
                    Text = "High priority"
                }
            };

            var headQuarters = new List<Headquarter>()
            {
                new Headquarter()
                {
                    HdName = "CAAF CGIL Albignasego",
                    HdColor = "#000",
                    HdRegion = "Veneto",
                    HdAddress = "Vicolo Trieste",
                    HdProvince = "Padova",
                    HdCity = "Albignasego",
                    HdZipCode = 35020,
                    StreetNumber = "1"
                },
                new Headquarter()
                {
                    HdName = "CGIL Legnaro",
                    HdColor = "#000",
                    HdRegion = "Veneto",
                    HdAddress = "Via Antonio Vivaldi",
                    HdProvince = "Padova",
                    HdCity = "Legnaro",
                    HdZipCode = 35020,
                    StreetNumber = "2"
                }
            };

            var assignee = new Assignee()
            {
                AppUser = danUser
            };

            var headQuarter = headQuarters.FirstOrDefault();

            var deal = new Deal()
            {
                AppUser = danUser,
                DlStartDate = DateTime.Now,
                DlEndDate = DateTime.Now.AddDays(1),
                Assignees = new List<Assignee>() { assignee },
                Headquarter = headQuarter,
                Priority = priorities.FirstOrDefault(),
            };

            await context.AddRangeAsync(priorities);
            await context.AddRangeAsync(headQuarters);
            await context.AddAsync(assignee);
            await context.AddAsync(deal);
            await context.SaveChangesAsync();
        }
    }
}