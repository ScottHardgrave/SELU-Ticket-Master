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
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();
            Label header = new Label
            {
                Text = "Thank you for using our App!",
                FontSize = 25,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            Label label1 = new Label
            {
                Text = "Tickets R' Us SELU Mobile App is all about making your ticket purchasing a breeze!",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            Label label2 = new Label
            {
                Text = "The intent of this mobile app is to allow the user " +
                       "the ability to buy their tickets on the web site and then " +
                       "show that ticket at the door of the event.",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            Label label3 = new Label
            {
                Text = "Just click on your ticket and show the event worker " +
                       "the QR code made for your ticket. " +
                       "It's that simple!",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            Label label4 = new Label
            {
                Text = "Workers on the app: \n" +
                     " Scott Hardgrave: Group Leader \n " +
                     "Connor Raiford \n" +
                     " Blake Weimer \n " +
                     "Cole Westenhiser",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            Padding = new Thickness(10, 0);
            Content = new StackLayout
            {
                Children =
                {
                    header,
                    label1,
                    label2,
                    label3,
                    label4
                }
            };
        }

        async void MyTicket_Selected(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}