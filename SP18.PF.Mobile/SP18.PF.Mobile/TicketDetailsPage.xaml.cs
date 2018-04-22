using SP18.PF.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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



        void ContactClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:383@envoc.com"));
        }

        ZXingBarcodeImageView barcode;
        public TicketDetailsPage (TicketDto selectedTicket)
		{
			InitializeComponent ();

            

            Label label = new Label
            {
                Text = selectedTicket.@event.eventProperty.ToString() + "\n " + "$" + selectedTicket.purchasePrice.ToString() 
                       + "\n" + selectedTicket.user.email.ToString() +"\n" + "General Admission",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold
            };

            var nasa = "";
            if (selectedTicket.@event.VenueId == 1)
            {
                nasa = "9200 University Blvd North Charleston, SC 29406";
            }

            if (selectedTicket.@event.VenueId == 2)
            {
                nasa = "Tiger Stadium Baton Rouge, LA 70803";
            }

            if(selectedTicket.@event.VenueId == 3)
            {
                nasa = "225 Decatur St New Orleans, LA 70130";
            }


            Button dir = new Button
            {
                Text = "Directions",
                FontSize = 20,
                Margin = 5,
                BackgroundColor = Color.DarkGreen,
                TextColor= Color.White,
                


            };
            dir.Clicked += DirButton;


            void DirButton(object sender, EventArgs e)
            {
                Device.OpenUri(new Uri("geo:0,0?q="+nasa));
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
            barcode.BarcodeValue = selectedTicket.@event.eventProperty.ToString() + " \n " + "$" + selectedTicket.purchasePrice.ToString()
                                   + "\n" + selectedTicket.user.email.ToString() + "\n" + "Valid Ticket";


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