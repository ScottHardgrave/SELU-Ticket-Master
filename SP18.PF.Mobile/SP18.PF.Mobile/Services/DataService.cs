using Newtonsoft.Json;
using SP18.PF.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SP18.PF.Mobile.Services
{
    public class DataService
    {
        public static async Task<List<TicketDto>> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<TicketDto>>(json);
            }

            return data;
        }
    }
}
