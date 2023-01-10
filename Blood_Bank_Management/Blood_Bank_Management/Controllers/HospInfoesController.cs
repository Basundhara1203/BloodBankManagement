using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blood_Bank_Management;
using Blood_Bank_Management.Models;

namespace BLOODBANK.Controllers
{
   // [Authorize(Roles = "Hospital/Organisation")]
    public class HospInfoesController : Controller
    {
        private truYumContext db = new truYumContext();

        // GET: HospInfoes
        public ActionResult Index()
        {
            var hospInfo = db.HospInfos.Include(h => h.Users);
            return View(hospInfo.ToList());
        }

        // GET: HospInfoes/Details/5
        public ActionResult Details(int? id)
        {
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


        public ActionResult Create()
        {

            return View();
        }

        // POST: DonorInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HospId,HospName,RegNo,Type,Address,City,Email,ContactNo")] HospInfo hospInfo)
        {
            var u = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (ModelState.IsValid)
            {
                hospInfo.UserId = u.Id;
                db.HospInfos.Add(hospInfo);
                db.SaveChanges();
                return RedirectToAction("Welcome","Welcome");
            }


            return View(hospInfo);
        }

        // GET: DonorInfoes/Edit/5
        public ActionResult Edit()
        {
            var u = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var v = db.HospInfos.Where(x => x.UserId == u.Id).FirstOrDefault();
            var hospInfo = db.HospInfos.Find(v.HospId);
            return View(hospInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HospInfo hospInfo)
        {


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

        public ActionResult AddToStock()
        {


            return View();

        }

        [HttpPost]
        public ActionResult AddToStock([Bind(Include = "Id,BloodType,Stock")] BloodStock bloodStock)

        {

            var u = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var v = db.HospInfos.Where(x => x.UserId == u.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {

                bloodStock.UserId = u.Id;

                bloodStock.HospId = v.HospId;

                db.BloodStocks.Add(bloodStock);
                db.SaveChanges();
                return RedirectToAction("ShowOwn");
            }


            return View(bloodStock);
        }
        //var em = s.MenuItems.Find(id);




        // GET: DonorInfoes/Delete/5
        public ActionResult Delete(int id)
        {


            var em = db.HospInfos.Where(e => e.HospId == id).FirstOrDefault();
            db.HospInfos.Remove(em);
            var es = db.BloodStocks.Where(e => e.HospId == id).ToList();
            foreach (var item in es)
            {
                db.BloodStocks.Remove(item);
            }

            db.SaveChanges();
            return RedirectToAction("Index");



        }
        public ActionResult DeleteStock(int id)
        {



            var es = db.BloodStocks.Where(e => e.Id == id).FirstOrDefault();
            
            db.BloodStocks.Remove(es);
            

            db.SaveChanges();
            return RedirectToAction("ShowOwn");



        }
        public ActionResult List()
        {
            //var hospInfo = db.HospInfo.Include(h => h.Users);
            //return View(hospInfo.ToList());
            List<HospInfo> HospInfo = db.HospInfos.ToList();



            var MenuRecord = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            return View(MenuRecord);
        }







        public ActionResult ShowOwn()

        {

            using (truYumContext ds = new truYumContext())
            {
                var u = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                List<BloodStock> BloodStock = db.BloodStocks.Where(x => x.UserId == u.Id).ToList();

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

            var bloodStock = db.HospInfos.Find(id);
            return View(bloodStock);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStock(BloodStock bloodStock)
        {


            var blood = db.BloodStocks.Find(bloodStock.Id);
            if (blood != null)
            {
                blood.BloodType = bloodStock.BloodType;
                blood.Stock = bloodStock.Stock;
               
            }


            db.SaveChanges();
            return RedirectToAction("ShowOwn");


        }







    }

}