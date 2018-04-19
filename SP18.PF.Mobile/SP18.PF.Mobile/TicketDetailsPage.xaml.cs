using SP18.PF.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SP18.PF.Mobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TicketDetailsPage : ContentPage
	{
		public TicketDetailsPage (TicketDto selectedTicket)
		{
			InitializeComponent ();

            ticketDetails.BindingContext = selectedTicket;
        }
	}
}