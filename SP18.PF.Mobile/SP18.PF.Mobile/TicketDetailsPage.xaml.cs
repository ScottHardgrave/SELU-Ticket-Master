﻿using SP18.PF.Mobile.Models;
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

            

            barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeOptions.Margin = 10;
            barcode.BarcodeValue = selectedTicket.@event.eventProperty.ToString() + " \n " + selectedTicket.purchasePrice.ToString();

            

            Content = barcode;

            

        }


    }
}