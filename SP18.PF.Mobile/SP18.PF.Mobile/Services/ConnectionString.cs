using SP18.PF.Mobile.Models;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System;


namespace SP18.PF.Mobile.Services
{
    public class ConnectionString
    {

        public static string queryString = "https://sp18-pf-g05.azurewebsites.net/"; 


        public static async Task<TicketDto> GetTicket()
        {
            var ticketString = queryString + "allTickets";

            dynamic results = await DataService.getDataFromService(ticketString).ConfigureAwait(false);
            Console.WriteLine(results);

           if (results[0] != null)
            {
                TicketDto ticket = new TicketDto
                {
                    Id = (string)results[0]["id"],
                    Name = (string)results[0]["event"]["tourName"] +", "+ (string)results[0]["event"]["venueName"],
                    Description = (string)results[0]["purchasePrice"]
                };

                return ticket;
            }
            else
            { 
                return null;
            }
            
        }

    }
}
