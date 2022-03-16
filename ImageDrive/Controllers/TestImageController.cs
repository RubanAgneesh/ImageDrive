using ImageDrive.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageDrive.Controllers
{
    public class TestImageController : Controller
    {
        // GET: TestImage
        public ActionResult Index()
        { 
            using (DBmodel dbmodel = new DBmodel())
            {
                return View(dbmodel.TBLimages.ToList());
            }
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(TBLimage tblimage)
        {
            string fileName = Path.GetFileNameWithoutExtension(tblimage.ImageFile.FileName);
            string extension = Path.GetExtension(tblimage.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            tblimage.Image = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            tblimage.ImageFile.SaveAs(fileName);
            using (DBmodel db = new DBmodel())
            {
                if (db.TBLimages.Any(m => m.Image.Contains(tblimage.Image)))
                {
                    ViewBag.DuplicateMessage = "File Name Already Exist.!";
                    return View();
                }
                db.TBLimages.Add(tblimage);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SucessMessage = "Image Added Successfully!";
            return View();
        }
    }
}