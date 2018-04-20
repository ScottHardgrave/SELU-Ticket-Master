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
            //AddAdminUser(dataContext);
            //AddCustomerUser(dataContext);
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
            for (int i = 0; i < 50; i++)
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
            /*
            var tours = dataContext.Set<Tour>().Select(x => x.Id).ToArray();
            var venues = dataContext.Set<Venue>().Select(x => x.Id).ToArray();
            for (int i = 0; i < 100; i++)
            {
                var tour = tours[i];
                for (int j = 0; j < 10; j++)
                {
                    var venue = venues[(i * 10 + j * 7) % venues.Length];
                    var start = DateTimeOffset.Now.AddDays(1 + i).AddHours(i);
                    */
            events.Add(new Event
            {
                TourId = 1,
                VenueId = 1,
                TicketPrice = 22,
                EventStart = new DateTime(2018, 05, 10, 18, 30, 00),
                EventEnd = new DateTime(2018, 05, 10, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 2,
                VenueId = 1,
                TicketPrice = 19,
                EventStart = new DateTime(2018, 05, 11, 15, 00, 00),
                EventEnd = new DateTime(2018, 05, 11, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 3,
                VenueId = 1,
                TicketPrice = 10,
                EventStart = new DateTime(2018, 05, 12, 17, 00, 00),
                EventEnd = new DateTime(2018, 05, 12, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 4,
                VenueId = 1,
                TicketPrice = 25,
                EventStart = new DateTime(2018, 05, 13, 13, 30, 00),
                EventEnd = new DateTime(2018, 05, 13, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 5,
                VenueId = 1,
                TicketPrice = 13,
                EventStart = new DateTime(2018, 05, 14, 12, 00, 00),
                EventEnd = new DateTime(2018, 05, 14, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 1,
                VenueId = 2,
                TicketPrice = 17,
                EventStart = new DateTime(2018, 05, 11, 16, 00, 00),
                EventEnd = new DateTime(2018, 05, 11, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 1,
                VenueId = 3,
                TicketPrice = 15,
                EventStart = new DateTime(2018, 05, 12, 18, 30, 00),
                EventEnd = new DateTime(2018, 05, 12, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 3,
                VenueId = 2,
                TicketPrice = 12,
                EventStart = new DateTime(2018, 05, 10, 12, 00, 00),
                EventEnd = new DateTime(2018, 05, 10, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 3,
                VenueId = 3,
                TicketPrice = 22,
                EventStart = new DateTime(2018, 05, 11, 17, 00, 00),
                EventEnd = new DateTime(2018, 05, 11, 19, 00, 00)
            });
            events.Add(new Event
            {
                TourId = 5,
                VenueId = 3,
                TicketPrice = 20,
                EventStart = new DateTime(2018, 05, 22, 13, 30, 00),
                EventEnd = new DateTime(2018, 05, 22, 19, 00, 00)
            });
            /*

                }
            }
            */
            dataContext.SaveChanges();
        }

        private static void SeedTours(DbContext dataContext)
        {
            var tours = dataContext.Set<Tour>();
            if (tours.Any())
            {
                return;
            }

            tours.Add(new Tour
            {
                Name = $"Sandy Tour",
                Description = $"A tour for all the beach bums."
            });
            tours.Add(new Tour
            {
                Name = $"Not Your Fathers Tour",
                Description = $"Not for the faint of heart. 18+ only."
            });
            tours.Add(new Tour
            {
                Name = $"Meat Eaters Tour",
                Description = $"Can I have a steak with that steak?"
            });
            tours.Add(new Tour
            {
                Name = $"Heartwrench Tour",
                Description = $"Gone through a breakup? Don't wallow in your self-pity alone."
            });
            tours.Add(new Tour
            {
                Name = $"アニメ音楽",
                Description = $"Do you think animes have the best music? So do we!"
            });

            dataContext.SaveChanges();
        }

        private static void SeedVenues(DbContext dataContext)
        {
            var venues = dataContext.Set<Venue>();
            if (venues.Any())
            {
                return;
            }

            venues.Add(new Venue
            {
                Name = $"Dimmsdale Dimmadome",
                PhysicalAddress = new Address
                {
                    AddressLine1 = "9200 University Blvd",
                    City = "North Charleston",
                    State = "SC",
                    ZipCode = "29406"
                },
                Capacity = 100,
                Description = $"Owned by Doug Dimmadome"
            });
            venues.Add(new Venue
            {
                Name = $"Tiger Stadium",
                PhysicalAddress = new Address
                {
                    AddressLine1 = "Tiger Stadium",
                    City = "Baton Rouge",
                    State = "LA",
                    ZipCode = "70803"
                },
                Capacity = 15000,
                Description = $"Home of the tigers"
            });
            venues.Add(new Venue
            {
                Name = $"House of Blues",
                PhysicalAddress = new Address
                {
                    AddressLine1 = "225 Decatur St",
                    City = "New Orleans",
                    State = "LA",
                    ZipCode = "70130"
                },
                Capacity = 500,
                Description = $"Keeping Jazz alive"
            });

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
            /*
            for (int i = 0; i < 100; i++)
            {
            */
            users.Add(new User
            {
                Email = $"customer1@envoc.com",
                Password = CryptoHelpers.HashPassword("password"),
                Role = Roles.Customer,
                BillingAddress = new Address
                {
                    AddressLine1 = "123 other place St",
                    City = "Ponchatoula",
                    State = "LA",
                    ZipCode = "70454"
                }
            });
            users.Add(new User
            {
                Email = $"customer2@envoc.com",
                Password = CryptoHelpers.HashPassword("password"),
                Role = Roles.Customer,
                BillingAddress = new Address
                {
                    AddressLine1 = "123 That one St",
                    City = "Ponchatoula",
                    State = "LA",
                    ZipCode = "70454"
                }
            });
            users.Add(new User
            {
                Email = $"customer3@envoc.com",
                Password = CryptoHelpers.HashPassword("password"),
                Role = Roles.Customer,
                BillingAddress = new Address
                {
                    AddressLine1 = "123 Not that place St",
                    City = "Ponchatoula",
                    State = "LA",
                    ZipCode = "70454"
                }
            });
            //}
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