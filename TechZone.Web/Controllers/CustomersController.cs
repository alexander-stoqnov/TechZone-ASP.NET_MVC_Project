﻿using System;

namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
    using Models.ViewModels.Customer;
    using Microsoft.AspNet.Identity;
    using Attributes;
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
            //byte[] imageByteData = System.IO.File.ReadAllBytes("C:\\Users\\Petar\\Downloads\\IMG_1770-2.jpg");
            //string imageBase64Data = Convert.ToBase64String(imageByteData);
            //string imageDataURL = $"data:image/png;base64,{imageBase64Data}";
            //ViewBag.ImageData = imageDataURL;

            var currentUserId = this.User.Identity.GetUserId();
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            CustomerProfileViewModel customerProfileVm = this._service.GetCurrentUserProfile(currentUserId, apikey[1]);
            return View(customerProfileVm);
        }

        [Route("UploadPicture")]
        [HttpPost]
        public ActionResult UploadPicture()
        {
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            string currentUserId = User.Identity.GetUserId();
            CustomerProfileViewModel customerProfileVm = this._service.GetCurrentUserProfile(currentUserId, apikey[1]);

            HttpPostedFileBase file = this.Request.Files["picture"];

            if (file == null)
            {
                ModelState.AddModelError("", "Please browse for a valid image file, and then click Upload.");
                return this.View("UserProfile", customerProfileVm);
            }

            if (file.ContentLength > 2097152)
            {
                ModelState.AddModelError("", "Image size should be less than 2mb.");
                return this.View("UserProfile", customerProfileVm);
            }

            string fileName = Path.GetFileName(file.FileName);
            if (!fileName.EndsWith(".jpg"))
            {
                ModelState.AddModelError("", "Please upload only .jpg pictures.");
                return this.View("UserProfile", customerProfileVm);
            }

            MemoryStream memstr = new MemoryStream();
            file.InputStream.CopyTo(memstr);
            byte[] imageData = memstr.ToArray();

            this._service.UploadUserProfilePicture(currentUserId, apikey[1], fileName, imageData);
            return RedirectToAction("UserProfile", "Customers");
        }
    }
}