using SP18.PF.Mobile.Models;
using SP18.PF.Mobile.Services;
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
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BindingContext = new TicketDto();
		}

        private async void GetTicketBtn_Clicked(object sender, EventArgs e)
        {
            
            TicketDto ticket = await ConnectionString.GetTicket();
            BindingContext = ticket;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
