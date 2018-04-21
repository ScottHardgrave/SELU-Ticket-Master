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
                barcode
                }
            };


        }

        async void About_Selected(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }



    }
}