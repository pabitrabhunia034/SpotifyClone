using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Entity;
using Spotify.Models;
using System.Data;

namespace Spotify.Controllers
{
    public class HomeController : Controller
    {
        ArtistsEntity art;
        public ActionResult Index()
        {
            MyAppDB db = new MyAppDB();
            db.dtOnly = db.selectSongs();
            return View(db);
        }
        public ActionResult login()
        {
           
            return View();
        }
        public ActionResult Registration()
        {

            return View();
        }

        public ActionResult artist()
        {
            MyAppDB db = new MyAppDB();
            db.dtOnly = db.selectArtist();
            return View(db);
         
        }

        public ActionResult About()
        {
            

            return View();
        }
        public ActionResult songs()
        {
            MyAppDB db = new MyAppDB();
            db.dtOnly = db.selectArtist();

            return View(db);
        }

        public ActionResult AddSong(FormCollection frm)
        {
            string _path_logo = "";
            string _FileName = "";
            string filePath = "";
            string filename = "";
            SongsEntity obj = new SongsEntity();

            HttpPostedFileBase x = Request.Files["artWork"];
            if (x != null && x.ContentLength > 0)
            {
                _FileName = Path.GetFileName(x.FileName);
                _path_logo = Path.Combine(Server.MapPath("~/Images"), _FileName);
                //WebImage productImagesidimg = new WebImage(x.InputStream);
                //productImagesidimg.Resize(500, 500,false);
                x.SaveAs(_path_logo);
                filePath = @"/Images/" + _FileName;
                filename = _FileName;
            }
            else
            {
                filePath = @"/Images/NoImage";
                filename = "NoImage";
            }
            obj.songName = frm["songName"];
            obj.rlsDate = frm["rlsDate"];
            obj.artists = frm["artist"];
            obj.image = filePath;
            MyAppDB db = new MyAppDB();
            if (db.addSong(obj) > 0)
            {
                TempData["SuccessMessage"] = "Successfully submitted";
                return RedirectToAction("songs", "Home");
            }
            else
            {
                TempData["SuccessMessage"] = "failed";
                return RedirectToAction("songs", "Home");
            }
        }
        public JsonResult addArtist(string name,string dob,string bio)
        {
            
            ArtistsEntity obj = new ArtistsEntity();
            List<ArtistsEntity> lst = new List<ArtistsEntity>();
            obj.artistName =name;
            obj.dob =dob;
            obj.bio = bio;
            MyAppDB db = new MyAppDB();
            DataTable dt = new DataTable();
            if (db.addArtist(obj) > 0)
            {
                dt = db.selectArtist();
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    art = new ArtistsEntity();
                    art.artistName = dt.Rows[i]["ARTNM"].ToString();
                    lst.Add(art);
                }
                return Json(lst,JsonRequestBehavior.AllowGet);
            }
            else
            {
               
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult selectArtist()
        {
            MyAppDB db = new MyAppDB();
            db.dtOnly = db.selectArtist();

            return RedirectToAction("songs", "Home",db);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}