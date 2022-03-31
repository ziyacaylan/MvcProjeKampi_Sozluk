using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using MvcProjeKampi.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        ImageFileManager im = new ImageFileManager(new EfImageFileDal());
        [Authorize]
        public ActionResult Index(int? page)
        {
            var files = im.GetList().ToPagedList(page ?? 1, 21); //? işaretleri boş gelme/boş olma durumuna ;
            return View(files);
        }

        [HttpGet]
        public ActionResult AddImage()
        {
            ImageViewModel imageModel = new ImageViewModel() { FileAttach = null, Message = string.Empty, IsValid = false };

            try { }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return this.View(imageModel);
        }
        [HttpPost]
        public ActionResult AddImage(ImageViewModel imageModel, ImageFile imageFile)
        {
            //Eger dosya yolu mevcut degilse,yeni dosya yolu olustur
            string folderPath = Server.MapPath("~/Images/Gallery/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            try
            {
                // Dogrulama Islemleri
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string expansion = Path.GetExtension(Request.Files[0].FileName);
                    string path = "/Images/Gallery/" + fileName + expansion;

                    //Dosya yükleme istek kontrolü
                    if (Request.Files.Count > 0)
                    {
                        var fullPath = Server.MapPath("/Images/Gallery/") + fileName + expansion;
                        //Resim dosyasi isim kontrolü
                        if (System.IO.File.Exists(fullPath))
                        {
                            ViewBag.ActionMessage = "Bu dosya adında başka bir resim mevcuttur";
                        }
                        else
                        {
                            Request.Files[0].SaveAs(Server.MapPath(path));
                            imageFile.ImageName = fileName;
                            imageFile.ImagePath = "/Images/Gallery/" + fileName + expansion;
                            imageFile.ImageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                            im.ImageAdd(imageFile);
                            //ViewBag.ActionMessage = "Resim yükleme başarıyla gerçekleşmiştir.";
                            imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarılı.   ";
                            return RedirectToAction("Index");
                        }
                    }

                    // Ayarlar - Dogrulama gecerliyse 
                    //imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarılı.   ";
                    imageModel.IsValid = true;
                }
                else
                {
                    // Ayarlar - Dogrulama gecersizse  
                    imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarısız!   ";
                    imageModel.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View(imageModel);
        }
        public ActionResult DeleteImage(int id)
        {
            var imageValues = im.GetByIdImageFile(id);
            im.ImageDelete(imageValues);
            return RedirectToAction("Index");
        }
    }
}