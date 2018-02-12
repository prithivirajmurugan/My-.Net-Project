using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMyProject.Models;
using System.Web.Security;

namespace MvcMyProject.Controllers
{
    [Authorize(Roles = "Owner")]
    
    public class OwnerController : Controller
    {
        ownerDaL dal = new ownerDaL();
        DAL customerdal = new DAL();
        public ActionResult addPg()
        {

            List<CityModel> list = new List<CityModel>(); 
            list=customerdal.getCities();
            List<SelectListItem> ls = new List<SelectListItem>();
            ls.Add(new SelectListItem{Text="Select",Value=""});
            foreach (CityModel item in list)
            {
                ls.Add(new SelectListItem { Text = item.cityName,Value=item.cityID.ToString()});
            }
            ViewBag.city=ls;
            return View();
        }
        [HttpPost]
        public ActionResult addPg(PgModel model,HttpPostedFileBase filename)
        {
            model.OBuserID = User.Identity.Name;
            if (dal.addPg(model))
            {
                filename.SaveAs(Server.MapPath(model.pgImagesAddress));
                return RedirectToAction("PgAdded", "Owner");
                
            }
            else {
                return RedirectToAction("PgNotAdded", "Owner");
                
            }
        }
        public ActionResult PgAdded()
        {
            return View();
        }
        public ActionResult PgNotAdded()
        {
            return View();
        }

        public ActionResult addRooms()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            List<SelectListItem> roomavail = new List<SelectListItem>();
            roomavail.Add(new SelectListItem { Text = "Select", Value = "" });
            roomavail.Add(new SelectListItem { Text = "Available", Value = "Available" });
            roomavail.Add(new SelectListItem { Text = "Not Available", Value = "Not Available" });
            ls.Add(new SelectListItem { Text = "Select", Value = "" });
            string ownerid = User.Identity.Name;
            foreach (PgModel item in dal.getPgofowner(ownerid))
            {
                ls.Add(new SelectListItem { Text = item.pgName, Value = item.pgID.ToString()});
            }
            List<SelectListItem> occ = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                occ.Add(new SelectListItem {Text=i.ToString(),Value=i.ToString() });
            }
                ViewBag.list = ls;
                ViewBag.occ = occ;
                ViewBag.roomavail = roomavail;
            return View();
        }

        [HttpPost]
        public ActionResult addRooms(RoomModel model,HttpPostedFileBase filename1,HttpPostedFileBase filename2)
        {
            
            if (dal.addRoom(model))
            {
                filename1.SaveAs(Server.MapPath(model.roomimage1));
                filename2.SaveAs(Server.MapPath(model.roomimage2));
                return View("roomAdded");
            }
            else {
                return View("roomNotAdded");
            }
        }

        public ActionResult PgList()
        {
            string ownerid = User.Identity.Name;
            List<PgModel> list_pg = new List<PgModel>();
            list_pg = dal.getPgofowner(ownerid);
            
            return View(list_pg);
            

        }
     
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getRoomsDetails(int pgid)
        {
            return PartialView(dal.getRoomsonpgid(pgid));
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
    }
}
