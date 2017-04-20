namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
    using System.Collections.Generic;
    using Models.ViewModels.Customer;
    using Microsoft.AspNet.Identity;
    using Attributes;
    using System;
    using System.IO;
    using System.Web;

    [RoutePrefix("Customers")]
    [CustomAuthorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly CustomersService _service;

        public CustomersController()
        {
            this._service = new CustomersService();
        }

        [Route("UserProfile")]
        public ActionResult UserProfile()
        {
            //string path = Server.MapPath("~/images/computer.png");
            byte[] imageByteData = System.IO.File.ReadAllBytes("C:\\Users\\Petar\\Downloads\\IMG_1770-2.jpg");
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageDataURL = $"data:image/png;base64,{imageBase64Data}";
            ViewBag.ImageData = imageDataURL;

            var currentUserId = this.User.Identity.GetUserId();
            IEnumerable<CustomerPurchaseHistoryViewModel> purchaseHistory = this._service.GetCurrentUserPurchases(currentUserId);
            return View(purchaseHistory);
        }

        [Route("Upload")]
        [HttpPost]
        public ActionResult Upload()
        {
            string currentUserId = User.Identity.GetUserId();
            //StudentProfileViewModel studentVm = this.service.GetUserProfile(currentUserId);

            HttpPostedFileBase file = this.Request.Files["exam"];

            if (file.ContentLength > 2097152)
            {
                ModelState.AddModelError("", "File cannot be larger than 2Mb!");
                //return this.View("Profile", studentVm);
            }

            string fileName = Path.GetFileName(file.FileName);
            if (!fileName.EndsWith(".rar"))
            {
                ModelState.AddModelError("", "Please upload only .rar files.");
                //return this.View("Profile");
            }
            //string fullFileName = service.GetFullFileName(id, currentUserId, fileName);

            string path = Server.MapPath("~/Exams");
           // string fullPath = Path.Combine(path, fullFileName);
            //file.SaveAs(fullPath);
            //return RedirectToAction("Profile", studentVm);
            return null;
        }
    }
}