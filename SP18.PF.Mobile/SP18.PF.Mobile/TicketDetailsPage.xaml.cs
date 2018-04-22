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
using ZXing.Net.Mobile.Forms;

namespace SP18.PF.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicketDetailsPage : ContentPage

    {

        public string VenueId;

        void ContactClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:383@envoc.com"));
        }

        ZXingBarcodeImageView barcode;
        public TicketDetailsPage(TicketDto selectedTicket)
        {
            InitializeComponent();
            getVenueIdAsync();



            Label label = new Label
            {
                Text = selectedTicket.@event.eventProperty.ToString() +"\n" + selectedTicket.@event.EventStart.DateTime.ToString() + "\n " + "$" + selectedTicket.purchasePrice.ToString()
                       + "\n" + selectedTicket.user.email.ToString() + "\n" + "General Admission",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold
            };

            async void getVenueIdAsync()
            {
                var queryString = ConnectionString.queryString;
                var VenueIdString = queryString + "api/venues/" + selectedTicket.@event.VenueId.ToString();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(VenueIdString);
                string json = response.Content.ReadAsStringAsync().Result;

                VenueDto venue = JsonConvert.DeserializeObject<VenueDto>(json);

                VenueId = venue.PhysicalAddress.AddressLine1 + " " + venue.PhysicalAddress.City + " " + venue.PhysicalAddress.State + " " +
                          venue.PhysicalAddress.ZipCode;
              

            }


            Button dir = new Button
            {
                Text = "Directions",
                FontSize = 20,
                Margin = 5,
                BackgroundColor = Color.DarkGreen,
                TextColor = Color.White,

            };
            dir.Clicked += DirButton;


            void DirButton(object sender, EventArgs e)
            {
                Device.OpenUri(new Uri("geo:0,0?q=" + VenueId));
            };

            barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeOptions.Margin = 10;
            barcode.BarcodeValue = selectedTicket.@event.eventProperty.ToString() + "\n" + selectedTicket.@event.EventStart.DateTime.ToString() + " \n " + "$" + selectedTicket.purchasePrice.ToString()
                                   + "\n" + selectedTicket.user.email.ToString() + "\n" + "Ticket is: Valid";


            Content = new StackLayout
            {
                Children =
                {
                label,
                barcode,
                dir
                }
            };


        }


        async void About_Selected(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }





    }
}