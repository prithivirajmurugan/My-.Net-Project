using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMyProject.Models;
using System.Web.Security;

namespace MvcMyProject.Controllers
{
    [Authorize(Roles = "Customer")]
  
    public class CustomerController : Controller
    {
        DAL customerdal = new DAL();
        ownerDaL dal = new ownerDaL();
        public ActionResult getpgofcity()

        {
            List<CityModel> city = new List<CityModel>();
            city = customerdal.getCities();
            List<SelectListItem> cities = new List<SelectListItem>();
            cities.Add(new SelectListItem { Text = "Select", Value = "" });
            foreach (CityModel item in city)
            {

                cities.Add(new SelectListItem { Text = item.cityName, Value = item.cityID.ToString() });

            }
            ViewBag.listcity = cities;
            return View();
        }
        public ActionResult pgbycity(string givencity)

        {
            List<PgModel> list = customerdal.get_list_pg_on_cities(givencity);
            return Json(list, JsonRequestBehavior.AllowGet);

 
        }
        public ActionResult SearchPg()
        {
            return View();
        }
        public ActionResult cities(string cityname)
        {
            List<string> city = new List<string>();
            List<CityModel> listcityname = customerdal.getCities();
            foreach (CityModel item in listcityname)
            {
                city.Add(item.cityName);
            }
            List<string> newciti = new List<string>();
            foreach (string item in city)
            {
                if (item.ToLower().StartsWith(cityname))
                {
                    newciti.Add(item);
                }
 
            }
            return Json(newciti, JsonRequestBehavior.AllowGet);

        }
        public ActionResult nearbyarea(string cityname, string nearby)
        {
            List<string> list = customerdal.getnearbyarea(cityname);
            List<string> newlis = new List<string>();
            foreach (string item in list)
            {
                if (item.ToLower().StartsWith(nearby))
                {
                    newlis.Add(item);
                }
            }
            return Json(newlis, JsonRequestBehavior.AllowGet);
 
        }
        public ActionResult getlistpgs(string cityname, string nearby)
        {
          List<PgModel> ls=customerdal.get_list_pg_on_nearbyarea(cityname, nearby);
          return PartialView("PartialPgListView",ls);
        }
        public ActionResult getRoomsDetails1(int pgid)
        {
            return PartialView(dal.getRoomsonpgid(pgid));
        }
        public ActionResult addToMyFavourites(string pgid, string roomid)
        {
            string userid = User.Identity.Name;

            if (customerdal.addtofavourites(userid, Convert.ToInt32( pgid),Convert.ToInt32 (roomid)))
            {
                return Json("Added in Favourites Successfully", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Not Added Try Again", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getFavourites()
        {
            string userid = User.Identity.Name;
            List<FavouritesModel> ls = new List<FavouritesModel>();
            ls = customerdal.showfavourites(userid);
            return View(ls);
        }
        public ActionResult getFavDetails(string pgid,string roomid)
        {
            return PartialView(customerdal.getFavDetails(Convert.ToInt32(pgid),Convert.ToInt32( roomid)));
        }
        public ActionResult customerSignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public string deletefromfavourites(string pgid, string roomid)
        {
            customerdal.deletefromfavourites(pgid, roomid);
            return "Deleted From Favourities";
        }
    }
 
}
