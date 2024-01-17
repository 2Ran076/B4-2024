using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {

        public static Home Window { get; set; }

        public List<KioskProduct> Products { get; set; }

        public void Initialize()
        {
            Start();
        }

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: €2.55\nFoto 30x30: €9,55");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Totaalbedrag€\n");

            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 30x30" });
            // Vul de productlijst met producten
            Products = new List<KioskProduct>
        {
            new KioskProduct() { Name = "Foto 10x15", Price = 2.55M, Description = "Foto van formaat 10x15" },
            new KioskProduct() { Name = "Foto 30x30", Price = 9.55M, Description = "Foto van formaat 10x15" }
            // Voeg hier meer producten toe indien nodig
        };

            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
        }

        private void UpdateShopPriceList()
        {
            // Loop through the list of products and add them to the price list
            foreach (KioskProduct product in Products ?? Enumerable.Empty<KioskProduct>())
            {
                ShopManager.AddShopPriceList($"{product.Name}: €{product.Price} - {product.Description}");
            }
        }

        public void AddButtonClick()
        {
            // Haal de geselecteerde productinformatie op
            int? fotoId = ShopManager.GetFotoId();
            int? amount = ShopManager.GetAmount();

            if (fotoId.HasValue && amount.HasValue)
            {
                KioskProduct selectedProduct = Products?.FirstOrDefault(p => p.Name == ShopManager.GetSelectedProduct());
                if (selectedProduct != null)
                {
                    // Create an OrderedProduct
                    OrderedProduct orderedProduct = new OrderedProduct
                    {
                        FotoId = fotoId.Value,
                        ProductName = selectedProduct.Name,
                        Amount = amount.Value,
                        TotalPrice = amount.Value * selectedProduct.Price
                    };

                    // Add the OrderedProduct to the ShopManager
                    ShopManager.AddOrderedProduct(orderedProduct);
                }
            }
        }

        private decimal GetProductPrice(string productName)
        {
            return Products?.FirstOrDefault(p => p.Name == productName)?.Price ?? 0;
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
            ResetShop();
        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            // Get the receipt text
            string receiptText = ShopManager.GetShopReceipt();

            // Save the receipt to a text file
            SaveReceiptToFile(receiptText);
        }

        private void SaveReceiptToFile(string receiptText)
        {
            // Set the directory where you want to save the receipt files
            string directoryPath = @"../../../Kassabonnen";

            try
            {
                // Create the directory if it doesn't exist
                Directory.CreateDirectory(directoryPath);

                // Define the base filename
                string baseFileName = "Kassabon";

                // Find the next available index
                int index = 1;
                string fileName;
                do
                {
                    fileName = $"{baseFileName}_{index}.txt";
                    index++;
                } while (File.Exists(Path.Combine(directoryPath, fileName)));

                // Combine the directory path and the file name
                string filePath = Path.Combine(directoryPath, fileName);

                // Write the receipt text to the file
                File.WriteAllText(filePath, receiptText);

                // Inform the user that the receipt has been saved successfully
                MessageBox.Show($"Kasssabon opgeslagen in: {filePath}", "Save Receipt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error");
            }
        }

        private void ResetShop()
        {

        }
    }
}
