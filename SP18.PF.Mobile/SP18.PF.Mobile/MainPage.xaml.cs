using Newtonsoft.Json;
using SP18.PF.Mobile.Models;
using SP18.PF.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SP18.PF.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
        int heightRowsList = 90;

        public MainPage()
		{
			InitializeComponent();

            getMyTickets();

           

        }

        void ContactClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:383@envoc.com"));
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public async void getMyTickets()
        {
            try
            {

                activity_indicator.IsRunning = true;
                Console.WriteLine("hello");
                var queryString = ConnectionString.queryString;
                var ticketstring = queryString + "allTickets";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(ticketstring);
                string json = response.Content.ReadAsStringAsync().Result;

                var convert = JsonConvert.DeserializeObject<List<TicketDto>>(json);

                ObservableCollection<TicketDto> myTickets = new ObservableCollection<TicketDto>(convert);
                myList.ItemsSource = myTickets;


                int i = myTickets.Count;
                if (i > 0)
                {
                    activity_indicator.IsRunning = false;
                }
                i = (myTickets.Count * heightRowsList);
                activity_indicator.HeightRequest = i;

            }
            catch (Exception e)
            {
                throw e;
            }


        }

        private void ticket_selected(object sender, SelectedItemChangedEventArgs e)
        {
            DisplayAlert("Your Ticket", "Please present this at your event!", "Ok");
            var itemSelectedData = e.SelectedItem as TicketDto;
            Navigation.PushAsync(new TicketDetailsPage(itemSelectedData));
        }

    }
}
