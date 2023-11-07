using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftAML_UpperLinkAPI.Areas.UploadView.Controllers
{
    //[Authorize]
    public class UploadsController : Controller
    {
        [HttpGet]
        public ActionResult AccountsUploads()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignatoriesUploads()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TransactionsUploads()
        {
            return View();
        }

        public ActionResult ViewOne()
        {
            return View();
        }

    }
}