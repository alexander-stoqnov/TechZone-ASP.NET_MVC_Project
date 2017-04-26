namespace TechZone.Services
{
    using System;
    using System.Linq;
    using Models.EntityModels;
    using System.Collections.Generic;
    using AutoMapper;
    using Models.ViewModels.Purchase;
    using Dropbox.Api;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.Globalization;

    public class PurchaseService : Service
    {
        public bool ProductExists(int id)
        {
            return this.Context.Products.Find(id) != null;
        }

        public void AddProductForCurrentSession(int id, string sessionId)
        {
            var product = this.Context.Products.Find(id);
            var shoppingCart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    SessionId = sessionId
                };
                this.Context.ShoppingCarts.Add(shoppingCart);
            }
            shoppingCart.Products.Add(product);
            this.Context.SaveChanges();
        }

        public void AddProductToShoppingCart(int id, string userId)
        {
            var product = this.Context.Products.Find(id);
            var user = this.Context.Users.Find(userId);
            var shoppingCart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == userId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    Customer = user
                };
                this.Context.ShoppingCarts.Add(shoppingCart);
            }

            shoppingCart.Products.Add(product);
            this.Context.SaveChanges();
        }

        public int GetNumberOfItemsInCart(string currentUserId, string sessionId)
        {
            ShoppingCart cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (cart != null)
            {
                return cart.Products.Count;
            }

            cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == currentUserId);
            if (cart != null)
            {
                return cart.Products.Count;
            }
            return 0;
        }

        public ShoppingCartViewModel GetCartItems(string userId, string sessionId)
        {
            ShoppingCartViewModel scvm = new ShoppingCartViewModel();
            ShoppingCart cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == userId);
            if (cart != null)
            {
                scvm = new ShoppingCartViewModel
                {
                    ProductsInCart = GetFinalProductInCartInfo(cart.Products)
                };

                scvm.FinalPriceWithoutDiscount = scvm.ProductsInCart.Sum(pr => pr.Price);
                scvm.FinalPriceWithDiscount = scvm.ProductsInCart.Sum(pr => pr.FinalPrice);
                return scvm;
            }

            cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (cart != null)
            {
                scvm = new ShoppingCartViewModel
                {
                    ProductsInCart = GetFinalProductInCartInfo(cart.Products)
                };               
            }
            scvm.FinalPriceWithoutDiscount = scvm.ProductsInCart.Sum(pr => pr.Price);
            scvm.FinalPriceWithDiscount = scvm.ProductsInCart.Sum(pr => pr.FinalPrice);
            return scvm;

        }

        private IEnumerable<ProductInCartViewModel> GetFinalProductInCartInfo(IEnumerable<Product> products)
        {
            var productsInCartVms = new HashSet<ProductInCartViewModel>();

            foreach (var product in products)
            {
                var productVm = Mapper.Map<ProductInCartViewModel>(product);
                productVm.FinalPrice = CalculateFinalPrice(product.Discount, product.Price);
                productsInCartVms.Add(productVm);
            }

            return productsInCartVms;
        }

        private decimal CalculateFinalPrice(int discount, decimal price)
        {
            decimal discountFinal = discount / 100.0m;
            return price - price * discountFinal;
        }

        public bool IsShoppingCartForRegisteredUser(string userId)
        {
            return this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == userId) != null;
        }

        public void RemoveProductFromCart(string currentUserId, string sessionId, int productId)
        {
            var cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (cart == null)
            {
                cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == currentUserId);
            }
            var productToRemove = cart.Products.FirstOrDefault(pr => pr.Id == productId);
            cart.Products.Remove(productToRemove);
            this.Context.SaveChanges();
        }

        public FinalCheckoutViewModel CalculatePriceWithoutShipment(string currentUserId)
        {
            var customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            var cart = this.Context.ShoppingCarts.First(c => c.Customer.Id == currentUserId);
            var finalCheckoutVm = new FinalCheckoutViewModel { CurrentCustomerBalance = customer.Credits };
            foreach (var product in cart.Products)
            {
                finalCheckoutVm.FinalPrice += this.CalculateFinalPrice(product.Discount, product.Price);
            }
            return finalCheckoutVm;
        }

        public bool EnoughCredits(string currentUserId, decimal totalPrice)
        {
            var customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            return customer.Credits > totalPrice;
        }

        public void FinalizePurchase(string currentUserId, decimal finalPrice, string dropboxKey)
        {
            var customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            var cart = this.Context.ShoppingCarts.First(c => c.Customer.Id == currentUserId);

            Purchase purchase = new Purchase
            {
                Customer = customer,
                FinalPrice = finalPrice,
                PurchaseDate = DateTime.Now,
            };

            foreach (var product in cart.Products)
            {
                product.Quantity--;
                if (product.Quantity <= 0)
                {
                    product.IsAvailable = false;
                }
                purchase.Products.Add(product);
            }

            customer.Credits -= finalPrice;
            this.Context.Purchases.Add(purchase);
            this.Context.ShoppingCarts.Remove(cart);
            this.Context.ShoppingCarts.Add(new ShoppingCart {Customer = customer.User});
            this.Context.SaveChanges();
            
            byte[] pdfData = CreatePdf(purchase);
            string fileName =
                $"{purchase.Customer.User.UserName}_{purchase.PurchaseDate.ToString("yyyyMMdd")}_{purchase.Id.ToString("00000000")}.pdf";
            Upload(new DropboxClient(dropboxKey), $"/Users/{customer.User.UserName}/Orders", fileName, pdfData);
        }

        public bool ContainsItemsNotInStock(string currentUserId)
        {
            var cart = this.Context.ShoppingCarts.First(c => c.Customer.Id == currentUserId);
            return cart.Products.Any(c => !c.IsAvailable || c.Quantity <= 0);
        }

        /// <summary>
        /// Helper Method to Generate Pdfs
        /// </summary>
        public byte[] CreatePdf(Purchase purchase)
        {
            MemoryStream workStream = new MemoryStream();
            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table with 4 columns  
            PdfPTable tableLayout = new PdfPTable(3);
            doc.SetMargins(0f, 0f, 0f, 0f);

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(Add_Content_To_PDF(tableLayout, purchase));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            return byteInfo;
        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, Purchase purchase)
        {
            float[] headers = { 75, 17, 15 };       //Header Widths  
            tableLayout.SetWidths(headers);         //Set the pdf headers  
            tableLayout.WidthPercentage = 85;       //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;

            //Add Title to the PDF file at the top  
            tableLayout.AddCell(new PdfPCell(new Phrase($"ORDER #{purchase.Id.ToString("00000000")}", new Font(Font.FontFamily.HELVETICA, 14, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER
            });

            tableLayout.AddCell(new PdfPCell(new Phrase($"Purchase Date: {purchase.PurchaseDate.ToString("D", new CultureInfo("en-US"))}", new Font(Font.FontFamily.COURIER, 12, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase($"Customer: {purchase.Customer.User.FullName}", new Font(Font.FontFamily.COURIER, 11, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER
            });
            ////Add header  
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Price");
            AddCellToHeader(tableLayout, "Guarantee");
            ////Add body  

            var purchasesVms = GetFinalProductInCartInfo(purchase.Products);

            foreach (var prod in purchasesVms)
            {
                AddCellToBody(tableLayout, prod.Name);
                AddCellToBody(tableLayout, "$" + prod.FinalPrice);
                AddCellToBody(tableLayout, (int)prod.Guarantee + "M");
            }
            return tableLayout;
        }

        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 11, 1, BaseColor.WHITE)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = new BaseColor(105, 105, 105)
            });
        }

        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 10, 1, BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                BackgroundColor = new BaseColor(255, 255, 255)
            });
        }
    }
}