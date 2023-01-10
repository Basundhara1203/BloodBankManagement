using Blood_Bank_Management.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Blood_Bank_Management.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            truYumContext db = new truYumContext();

            var u = db.Users.Where(x => x.RoleId == 2).ToList();
            Console.WriteLine(u);
            return View(u);
        }
        public ActionResult RegisterHospFromAdmin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterHospFromAdmin(Users m)
        {
            truYumContext ds = new truYumContext();
            ds.Users.Add(m);
            ds.SaveChanges();
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return RedirectToAction("Index");
            
        }

        public ActionResult CreateAsAdmin(int Id)
        {
            truYumContext db = new truYumContext();
            var u = db.HospInfos.Where(x => x.UserId == Id).FirstOrDefault();


            if (u != null)
            {
                TempData["Id"] = Id;
                return RedirectToAction("EditAsAdmin");
            }
            else
            {
                TempData["Id"] = Id;
                return View();
            }
        }
        [HttpPost]
        public ActionResult CreateAsAdmin([Bind(Include = "HospId,HospName,RegNo,Type,Address,City,Email,ContactNo")] HospInfo hospInfo)
        {
            truYumContext db = new truYumContext();

            int UserId = Convert.ToInt32(TempData["Id"]);
            if (ModelState.IsValid)
            {
                hospInfo.UserId = UserId;
                db.HospInfos.Add(hospInfo);
                db.SaveChanges();
                return RedirectToAction("IndexList");
            }
            return View(hospInfo);
        }

        public ActionResult IndexList()
        {
            truYumContext db = new truYumContext();
            var u = db.HospInfos.ToList();
            return View(u);
        }

        public ActionResult AddToStockAdmin(int Id)
        {

           // truYumContext db = new truYumContext();
            TempData["HospId"] = Id;
            return View();

        }
        [HttpPost]
        public ActionResult AddToStockAdmin([Bind(Include = "Id,BloodType,Stock")] BloodStock bloodStock)
        {
            truYumContext db = new truYumContext();
            int HospId = Convert.ToInt32(TempData["HospId"]);
            //var u = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var v = db.HospInfos.Where(x => x.HospId == HospId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                
                bloodStock.UserId = v.UserId;

                bloodStock.HospId = HospId;

                db.BloodStocks.Add(bloodStock);
                db.SaveChanges();
                return RedirectToAction("IndexList");

            }

            return View(bloodStock);
        }

        public ActionResult Successful()
        {
            return View();

        }

        public ActionResult EditAsAdmin()
        {
            int id = Convert.ToInt32(TempData["Id"]);
            truYumContext db = new truYumContext();
            var hospInfo = db.HospInfos.Where(x => x.UserId == id).FirstOrDefault();
            return View(hospInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAsAdmin(HospInfo hospInfo)
        {
            truYumContext db = new truYumContext();

            var hosp = db.HospInfos.Find(hospInfo.HospId);
            if (hosp != null)
            {
                hosp.HospName = hospInfo.HospName;
                hosp.RegNo = hospInfo.RegNo;
                hosp.Type = hospInfo.Type;

                hosp.Address = hospInfo.Address;
                hosp.City = hospInfo.City;
                hosp.Email = hospInfo.Email;
                hosp.ContactNo = hospInfo.ContactNo;


            }


            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult EditAsAdminfromlist(int id)
        {
            //int id = Convert.ToInt32(TempData["Id"]);
            truYumContext db = new truYumContext();
            var hospInfo = db.HospInfos.Where(x => x.HospId == id).FirstOrDefault();
            return View(hospInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAsAdminfromlist(HospInfo hospInfo)
        {
            truYumContext db = new truYumContext();

            var hosp = db.HospInfos.Find(hospInfo.HospId);
            if (hosp != null)
            {
                hosp.HospName = hospInfo.HospName;
                hosp.RegNo = hospInfo.RegNo;
                hosp.Type = hospInfo.Type;

                hosp.Address = hospInfo.Address;
                hosp.City = hospInfo.City;
                hosp.Email = hospInfo.Email;
                hosp.ContactNo = hospInfo.ContactNo;


            }


            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult RegisteredListHosp()
        {
            truYumContext db = new truYumContext();

            var u = db.Users.Where(x => x.RoleId == 2).ToList();
            Console.WriteLine(u);
            return View(u);
        }

        public ActionResult Details(int? id)
        {
            truYumContext db = new truYumContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospInfo hospInfo = db.HospInfos.Find(id);
            if (hospInfo == null)
            {
                return HttpNotFound();
            }
            return View(hospInfo);
        }

        public ActionResult Delete(int id)
        {

            truYumContext db = new truYumContext();
            var em = db.HospInfos.Where(e => e.HospId == id).FirstOrDefault();
            db.HospInfos.Remove(em);
            var es = db.BloodStocks.Where(e => e.HospId == id).ToList();
            foreach (var item in es)
            {
                db.BloodStocks.Remove(item);
            }
            
            db.SaveChanges();
            return RedirectToAction("IndexList");



        }

        public ActionResult DeleteUsers(int id)
        {
            truYumContext db = new truYumContext();

            var em = db.Users.Where(e => e.Id == id).FirstOrDefault();
            db.Users.Remove(em);
            var en = db.HospInfos.Where(e => e.UserId == id).FirstOrDefault();
            if(en!=null)
               db.HospInfos.Remove(en);
            var es = db.BloodStocks.Where(e => e.UserId == id).ToList();
            foreach (var item in es)
            {
                db.BloodStocks.Remove(item);
            }

            db.SaveChanges();
            return RedirectToAction("Index");



        }


        public ActionResult ShowStockDetails(int id)

        {

            using (truYumContext db = new truYumContext())
            {
                HospInfo hospInfo = db.HospInfos.Find(id);

                //return View(hospInfo);
                var u = db.HospInfos.Where(x => x.HospId == hospInfo.HospId).FirstOrDefault();
                List<BloodStock> BloodStock = db.BloodStocks.Where(x => x.HospId == u.HospId).ToList();

                List<HospInfo> HospInfo = db.HospInfos.ToList();


                var MenuRecord = from e in BloodStock
                                 join d in HospInfo on e.UserId equals d.UserId into table1
                                 from d in table1.ToList()

                                 select new ViewModel1
                                 {
                                     bloodstock = e,
                                     hospinfo = d,
                                 };
                return View(MenuRecord);
            }

        }

        public ActionResult EditStock(int id)
        {
            truYumContext db = new truYumContext();
            var bloodStock = db.HospInfos.Find(id);
            return View(bloodStock);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStock(BloodStock bloodStock)
        {

            truYumContext db = new truYumContext();
            var blood = db.BloodStocks.Find(bloodStock.Id);
            if (blood != null)
            {
                blood.BloodType = bloodStock.BloodType;
                blood.Stock = bloodStock.Stock;

            }


            db.SaveChanges();
            return RedirectToAction("Welcome", "Welcome");


        }


        public ActionResult DeleteStock(int id)
        {

            truYumContext db = new truYumContext();

            var es = db.BloodStocks.Where(e => e.Id == id).FirstOrDefault();

            db.BloodStocks.Remove(es);


            db.SaveChanges();
            return RedirectToAction("Welcome", "Welcome");



        }


        public ActionResult ShowDonor()
        {
            truYumContext db = new truYumContext();
            var donorInfo = db.DonorInfos.ToList();
            return View(donorInfo.ToList());
        }
    }
}