using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FotoKiosk
{
    public partial class CheckoutPage : UserControl
    {
        public static CheckoutPage Instance;

        public CheckoutPage()
        {
            this.InitializeComponent();
            Instance = this;
        }
        double totalPrice = 0.0;
        private void AddEl_Click(object sender, RoutedEventArgs e)
        {
            if (Product.SelectedItem is ComboBoxItem selectedProductItem)
            {
                string selectedProduct = selectedProductItem.Content.ToString();

                if (int.TryParse(AantalProduct.Text, out int quantity))
                {
                    double productPrice = GetProductPrice(selectedProduct);
                    double totalAmount = productPrice * quantity;                  
                    totalPrice += totalAmount;

                    EindBedrag.Text = "Eindbedrag: \n" + totalPrice.ToString("C");



                    ListBoxItem receiptItem = new ListBoxItem();
                    receiptItem.Content = $"{quantity}x {selectedProduct} - {totalAmount:C}";

                    // Add the ListBoxItem to the receipt ListBox
                    KassaBon.Items.Add(receiptItem);
                }
            }
        }


        private double GetProductPrice(string product)
        {
            Dictionary<string, double> productPrices = new Dictionary<string, double>()
    {
        { "Foto 10x15", 2.55 },
        { "Foto 20x30", 4.95 },
        { "Mok met foto", 9.95 },
        { "Sleutelhanger", 6.12 },
        { "T-shirt", 11.99 }
    };

            if (productPrices.ContainsKey(product))
            {
                return productPrices[product];
            }

            return 0.0; // Default to 0 if the product is not found
        }




        private void ResetEl_Click(object sender, RoutedEventArgs e)
        {
            EindBedrag.Text = string.Empty;
            KassaBon.Items.Clear();
            AantalProduct.Text = string.Empty;
            totalPrice = 0.0;
        }
    }
}
