using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TechZone.Services
{
    using System;
    using System.Linq;
    using Models.EntityModels;
    using System.Collections.Generic;
    using AutoMapper;
    using Models.ViewModels.Purchase;
    using System.Threading.Tasks;
    using Dropbox.Api;
    using Dropbox.Api.Files;
    using System.IO;

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

            //byte[] imageByteData = File.ReadAllBytes("C:\\Users\\Petar\\Downloads\\IMG_1770-2.jpg");
            byte[] pdfData = CreatePdf(customer.Purchases);
            this.Upload(new DropboxClient(dropboxKey), @"/Images", "testPdfFirstTry.pdf", pdfData);
        }

        public bool ContainsItemsNotInStock(string currentUserId)
        {
            var cart = this.Context.ShoppingCarts.First(c => c.Customer.Id == currentUserId);
            return cart.Products.Any(c => !c.IsAvailable);
        }

        async Task Upload(DropboxClient dbx, string folder, string file, byte[] content)
        {
            using (var mem = new MemoryStream(content))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
            }
        }

        /// <summary>
        /// Helper Method to Generate Pdfs
        /// </summary>
        /// <param name="tableLayout"></param>
        /// <returns></returns>

        public byte[] CreatePdf(ICollection<Purchase> purchases)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("SamplePdf" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table with 5 columns  
            PdfPTable tableLayout = new PdfPTable(5);
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table  

            //file will created in this path  
            //string strAttachment = Server.MapPath("~/Downloadss/" + strPDFFileName);

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(Add_Content_To_PDF(tableLayout, purchases));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            return byteInfo;
            //workStream.Write(byteInfo, 0, byteInfo.Length);
            //workStream.Position = 0;
            //return File(workStream, "application/pdf", strPDFFileName);
        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, ICollection<Purchase> purchasess)
        {
            float[] headers = { 50, 24, 45, 35, 50 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;

            //Add Title to the PDF file at the top  
            tableLayout.AddCell(new PdfPCell(new Phrase("Creating Pdf using ItextSharp", new Font(Font.FontFamily.HELVETICA, 8, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER
            });

            ////Add header  
            AddCellToHeader(tableLayout, "Id");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Price");

            ////Add body  

            foreach (var prod in purchasess)
            {
                AddCellToBody(tableLayout, prod.Id.ToString());
                AddCellToBody(tableLayout, prod.FinalPrice.ToString());
                AddCellToBody(tableLayout, prod.PurchaseDate.ToString());
            }
            return tableLayout;
        }

        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, BaseColor.YELLOW)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new BaseColor(128, 0, 0)
            });
        }

        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new BaseColor(255, 255, 255)
            });
        }
    }
}