using System;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Events;
using SP18.PF.Core.Features.Shared;
using SP18.PF.Core.Features.Tickets;
using SP18.PF.Core.Features.Tours;
using SP18.PF.Core.Features.Users;
using SP18.PF.Core.Features.Venues;
using SP18.PF.Web.Helpers;

namespace SP18.PF.Web.Data
{
    public static class DbInitilizer
    {
        public static void SeedData(DbContext dataContext)
        {
            SeedUsers(dataContext);
            SeedVenues(dataContext);
            SeedTours(dataContext);
            SeedScheduledEvents(dataContext);
            SeedTickets(dataContext);
        }

        private static void SeedTickets(DbContext dataContext)
        {
            var tickets = dataContext.Set<Ticket>();
            if (tickets.Any())
            {
                return;
            }
            var events = dataContext.Set<Event>().ToArray();
            var users = dataContext.Set<User>().ToArray();
            for (int i = 0; i < 5000; i++)
            {
                var @event = events[i % events.Length];
                var user = users[i % users.Length];

                tickets.Add(new Ticket
                {
                    Event = @event,
                    User = user,
                    PurchasePrice = @event.TicketPrice
                });
            }
            dataContext.SaveChanges();
        }

        private static void SeedScheduledEvents(DbContext dataContext)
        {
            var events = dataContext.Set<Event>();
            if (events.Any())
            {
                return;
            }
            var tours = dataContext.Set<Tour>().Select(x => x.Id).ToArray();
            var venues = dataContext.Set<Venue>().Select(x => x.Id).ToArray();
            for (int i = 0; i < 100; i++)
            {
                var tour = tours[i];
                for (int j = 0; j < 10; j++)
                {
                    var venue = venues[(i * 10 + j * 7) % venues.Length];
                    var start = DateTimeOffset.Now.AddDays(1 + i).AddHours(i);
                    events.Add(new Event
                    {
                        TourId = tour,
                        VenueId = venue,
                        TicketPrice = (i + 1) * 3,
                        EventStart = start,
                        EventEnd = start.AddHours(j + 1)
                    });
                }
            }
            dataContext.SaveChanges();
        }

        private static void SeedTours(DbContext dataContext)
        {
            var tours = dataContext.Set<Tour>();
            if (tours.Any())
            {
                return;
            }
            for (int i = 0; i < 100; i++)
            {
                tours.Add(new Tour
                {
                    Name = $"tour{i}",
                    Description = $"Tour Description {i}"
                });
            }
            dataContext.SaveChanges();
        }

        private static void SeedVenues(DbContext dataContext)
        {
            var venues = dataContext.Set<Venue>();
            if (venues.Any())
            {
                return;
            }
            for (int i = 0; i < 100; i++)
            {
                venues.Add(new Venue
                {
                    Name = $"venue{i}",
                    PhysicalAddress = new Address
                    {
                        AddressLine1 = "123 place St",
                        City = "Hammond",
                        State = "LA",
                        ZipCode = "70403"
                    },
                    Capacity = 10 * i,
                    Description = $"Venue Description {i}"
                });
            }
            dataContext.SaveChanges();
        }

        private static void SeedUsers(DbContext dataContext)
        {
            var users = dataContext.Set<User>();

            if (users.Any())
            {
                AddAdminUser(dataContext);
                return;
            }
            AddAdminUser(dataContext);
            for (int i = 0; i < 100; i++)
            {

                users.Add(new User
                {
                    Email = $"email{i}@envoc.com",
                    Password = CryptoHelpers.HashPassword($"password{i}"),
                    Role = Roles.Customer,
                    BillingAddress = new Address
                    {
                        AddressLine1 = "123 place St",
                        City = "Hammond",
                        State = "LA",
                        ZipCode = "70403"
                    }
                });
            }
            dataContext.SaveChanges();
        }

        private static void AddAdminUser(DbContext dataContext)
        {
            var users = dataContext.Set<User>();
            if (users.Any(x => x.Email == "admin@envoc.com"))
            {
                return;
            }
            users.Add(new User
            {
                Email = $"admin@envoc.com",
                Password = CryptoHelpers.HashPassword("password"),
                Role = Roles.Admin,
                BillingAddress = new Address
                {
                    AddressLine1 = "123 place St",
                    City = "Hammond",
                    State = "LA",
                    ZipCode = "70403"
                }
            });
            dataContext.SaveChanges();
        }
    }
}