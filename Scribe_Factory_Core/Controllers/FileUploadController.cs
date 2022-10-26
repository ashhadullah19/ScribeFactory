using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Scribe_Factory_Core.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUploadController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(IFormFile formFile)
        {
            return Ok();
        }

        // GET: FileUploadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileUploadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileUploadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FileUploadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileUploadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FileUploadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FileUploadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
