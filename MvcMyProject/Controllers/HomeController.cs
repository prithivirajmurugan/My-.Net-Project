using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMyProject.Models;
using System.Web.Security;

namespace MvcMyProject.Controllers
{
    public class HomeController : Controller
    {
        DAL dal=new DAL();
        ownerDaL ownerdal = new ownerDaL();



        public ActionResult UserCreated()
        {
            return View();
 
        }

        public ActionResult CreateUser()
        {
            List<CityModel> city = new List<CityModel>();
            city = dal.getCities();
            List<SelectListItem> cities = new List<SelectListItem>();
            cities.Add(new SelectListItem { Text = "Select", Value = "" });
            foreach (CityModel item in city)
            {
               
                cities.Add(new SelectListItem { Text = item.cityName, Value = item.cityID.ToString()});
                
            }
            ViewBag.listcity = cities;
            List<SelectListItem> type = new List<SelectListItem>();
            type.Add(new SelectListItem { Text = "Select", Value = "" });
            type.Add(new SelectListItem { Text = "Customer", Value = "1" });
            type.Add(new SelectListItem { Text = "Owner", Value = "2" });
            ViewBag.type = type;
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserModel model)
        {
            if (model.userType == "1")
            {
                if (dal.addCustomers(model))
                {
                    return RedirectToAction("UserCreated","Home");
                }
                else
                {
                    List<CityModel> city = new List<CityModel>();
                    city = dal.getCities();
                    List<SelectListItem> cities = new List<SelectListItem>();
                    cities.Add(new SelectListItem { Text = "Select", Value = "" });
                    foreach (CityModel item in city)
                    {

                        cities.Add(new SelectListItem { Text = item.cityName, Value = item.cityID.ToString() });

                    }
                    ViewBag.listcity = cities;
                    List<SelectListItem> type = new List<SelectListItem>();
                    type.Add(new SelectListItem { Text = "Select", Value = "" });
                    type.Add(new SelectListItem { Text = "Customer", Value = "1" });
                    type.Add(new SelectListItem { Text = "Owner", Value = "2" });
                    ViewBag.type = type;
                    return View();
                }
            }
            else if (model.userType == "2")
            {
                if (ownerdal.addOwner(model))
                {
                    return RedirectToAction("UserCreated","Home");
                }
                else {
                    List<CityModel> city = new List<CityModel>();
                    city = dal.getCities();
                    List<SelectListItem> cities = new List<SelectListItem>();
                    foreach (CityModel item in city)
                    {

                        cities.Add(new SelectListItem { Text = item.cityName, Value = item.cityID.ToString() });

                    }
                    ViewBag.listcity = cities;
                    List<SelectListItem> type = new List<SelectListItem>();
                    type.Add(new SelectListItem { Text = "Select", Value = "" });
                    type.Add(new SelectListItem { Text = "Customer", Value = "1" });
                    type.Add(new SelectListItem { Text = "Owner", Value = "2" });
                    ViewBag.type = type;
                    return View();
 
                }
            }
            return View("Customer Not Added");
        }
        public ActionResult Login()
        {
            List<SelectListItem> logintype = new List<SelectListItem>();
            logintype.Add(new SelectListItem{Text="Customer",Value="1"});
            logintype.Add(new SelectListItem { Text = "Owner/Broker", Value = "2" });
            ViewBag.logintype = logintype;
            return View();
        }


        public ActionResult CheckEmail(string EmailId)
        {
           if( dal.checkEmail(EmailId))
           {
               return Json(true, JsonRequestBehavior.AllowGet);
           }
           else
           {
               return Json(false, JsonRequestBehavior.AllowGet);
           }
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            if (dal.login(model))
            {
                FormsAuthentication.SetAuthCookie(model.emailID, model.rememberMe);
                if (model.loginType == "1")
                {
                    return RedirectToAction("HomePage", "Customer");
                }
                else
                {
                    return RedirectToAction("HomePage", "Owner");
                }
            }
            else
            {
                List<SelectListItem> logintype = new List<SelectListItem>();
                logintype.Add(new SelectListItem { Text = "Customer", Value = "1" });
                logintype.Add(new SelectListItem { Text = "Owner/Broker", Value = "2" });
                ViewBag.logintype = logintype;
                return View();
            }
        }

    }
}
