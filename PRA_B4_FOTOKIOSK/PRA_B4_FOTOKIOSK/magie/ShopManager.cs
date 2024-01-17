using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PRA_B4_FOTOKIOSK.magie
{
    public class ShopManager
    {

        public static List<KioskProduct> Products = new List<KioskProduct>();
        public static List<OrderedProduct> OrderedProducts = new List<OrderedProduct>();
        public static Home Instance { get; set; }

        public static void SetShopPriceList(string text)
        {
            Instance.lbPrices.Content = text;
        }

        public static void AddShopPriceList(string text)
        {
            Instance.lbPrices.Content = Instance.lbPrices.Content + text;
        }
        public static void AddOrderedProduct(OrderedProduct orderedProduct)
        {
            OrderedProducts.Add(orderedProduct);
            UpdateShopReceipt(); // Update the receipt content when a new product is added
        }
        private static void UpdateShopReceipt()
        {
            // Update the receipt content based on the ordered products
            StringBuilder receiptText = new StringBuilder("Eindbedrasasd€\n");

            foreach (var orderedProduct in OrderedProducts)
            {
                receiptText.AppendLine($"{orderedProduct.ProductName} (Foto-ID: {orderedProduct.FotoId}): {orderedProduct.Amount} x €{orderedProduct.TotalPrice}");
            }

            // Add total amount to the receipt
            decimal totalAmount = OrderedProducts.Sum(product => product.TotalPrice);
            receiptText.AppendLine($"Totaalbedrag: €{totalAmount}");

            SetShopReceipt(receiptText.ToString());
        }

        public static void SetShopReceipt(string text)
        {
            Instance.lbReceipt.Content = text;
        }

        public static string GetShopReceipt()
        {
            return (string)Instance.lbReceipt.Content;
        }

        public static void AddShopReceipt(string text)
        {
            SetShopReceipt(GetShopReceipt() + text);
        }

        public static void UpdateDropDownProducts()
        {
            Instance.cbProducts.Items.Clear();
            foreach (KioskProduct item in Products)
            {
                Instance.cbProducts.Items.Add(item.Name);
            }
        }

        public static string GetSelectedProduct()
        {
            if (Instance.cbProducts.SelectedItem == null) return null;
            return Instance.cbProducts.SelectedItem.ToString();
        }


        public static int? GetFotoId()
        {
            int? id = null;
            int amount = -1;
            if (int.TryParse(Instance.tbFotoId.Text, out amount))
            {
                id = amount;
            }
            return id;
        }

        public static int? GetAmount()
        {
            int? id = null;
            int amount = -1;
            if (int.TryParse(Instance.tbAmount.Text, out amount))
            {
                id = amount;
            }
            return id;
        }
        public static List<OrderedProduct> GetOrderedProducts()
        {
            return OrderedProducts;
        }
    }
}
