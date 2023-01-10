using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blood_Bank_Management.Models;
using Blood_Bank_Management;


namespace Blood_Bank_Management.Controllers
{
    //[Authorize(Roles ="User/Donor")]
    public class DonorInfoesController : Controller
    {
        private truYumContext db = new truYumContext();


        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult DetailsDonor()
        {
            truYumContext ds = new truYumContext();
            var u = ds.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var v = ds.DonorInfos.Where(x => x.UserId == u.Id).FirstOrDefault();
            
            if (v == null)
            {
                return HttpNotFound();
            }
            return View(v);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorInfo donorInfo = db.DonorInfos.Find(id);
            if (donorInfo == null)
            {
                return HttpNotFound();
            }
            return View(donorInfo);
        }

        // GET: DonorInfoes/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: DonorInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonorId,Name,Age,Contact,Email,BloodType,Address,City,Weight,MedicalHist")] DonorInfo donorInfo)
        {
            var u = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (ModelState.IsValid)
            {
                donorInfo.UserId = u.Id;
                db.DonorInfos.Add(donorInfo);
                db.SaveChanges();
                ViewBag.SuccessMsg = "Successfully Added";
                return RedirectToAction("DetailsDonor");
            }


            return View(donorInfo);
        }

        // GET: DonorInfoes/Edit/5
        public ActionResult Edit(int id)
        {

            var donorInfo = db.DonorInfos.Find(id);
            return View(donorInfo);
        }

        // POST: DonorInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DonorInfo donorInfo)
        {


            var donor = db.DonorInfos.Find(donorInfo.DonorId);
            if (donor != null)
            {
                donor.Name = donorInfo.Name;
                donor.Age = donorInfo.Age;
                donor.Contact = donorInfo.Contact;
                donor.Email = donorInfo.Email;
                donor.BloodType = donorInfo.BloodType;
                donor.Address = donorInfo.Address;
                donor.City = donorInfo.City;
                donor.Weight = donorInfo.Weight;
                donor.MedicalHist = donorInfo.MedicalHist;

            }


            db.SaveChanges();
            return RedirectToAction("DetailsDonor");






        }

       
        public ActionResult Delete(int id)
        {


            var em = db.DonorInfos.Where(e => e.DonorId == id).FirstOrDefault();
            db.DonorInfos.Remove(em);
            db.SaveChanges();
            return RedirectToAction("Index");



        }

        
        public ActionResult Search(string option, string search1,string search2)
        {
           // truYumContext db = new truYumContext();
            //if a user choose the radio button option as Subject  

            if (option == "Hospital")
            {
                //int search1 = Convert.ToInt32(search);
                List<BloodStock> BloodStock = db.BloodStocks.ToList();

                List<HospInfo> HospInfo = db.HospInfos.ToList();


                var MenuRecord = from e in BloodStock
                                 join db in HospInfo
                                 on e.HospId equals db.HospId into table1
                                 from db in table1.ToList()

                                 select new ViewModel1
                                 {
                                     bloodstock = e,
                                     hospinfo = db,
                                 };

                var d = MenuRecord.Where(x => x.bloodstock.BloodType == search1
                ||   x.hospinfo.City == search2
                || x.bloodstock.BloodType == search1 && x.hospinfo.City == search2
                ).ToList();

                if (search1 == "")
                {
                    var r = MenuRecord.Where(x => x.hospinfo.City == search2).ToList();
                    return View(r);
                }
                else if (search2 == "")
                {
                    var rr = MenuRecord.Where(x => x.bloodstock.BloodType == search1).ToList();
                    return View(rr);
                }
                else
                {
                    var rrr = MenuRecord.Where(x => x.bloodstock.BloodType == search1 && x.hospinfo.City == search2
                   ).ToList();
                    return View(rrr);
                }


                
            }
            else if(option=="Donor")
            {
                if (search1 == "")
                {
                    var r = db.DonorInfos.Where(x => x.City == search2).ToList();
                    return View("SearchDonor", r);
                }
                else if (search2 == "")
                {
                    var rr = db.DonorInfos.Where(x => x.BloodType == search1).ToList();
                    return View("SearchDonor", rr);
                }
                else
                {
                    var rrr = db.DonorInfos.Where(x => x.BloodType == search1 && x.City == search2
                   ).ToList();
                    return View("SearchDonor", rrr);
                }
            }
            else
            {
                List<BloodStock> BloodStock = db.BloodStocks.ToList();

                List<HospInfo> HospInfo = db.HospInfos.ToList();


                var MenuRecord = from e in BloodStock
                                 join db in HospInfo
                                 on e.HospId equals db.HospId into table1
                                 from db in table1.ToList()

                                 select new ViewModel1
                                 {
                                     bloodstock = e,
                                     hospinfo = db,
                                 };

                var d = MenuRecord.Where(x => x.bloodstock.BloodType == search1 && search2 == null || search1 == null && x.hospinfo.City == search2 || x.bloodstock.BloodType == search1 && x.hospinfo.City == search2 ).ToList();

                return View(d);
            }

        }

        public ActionResult DeleteOwn()
        {
            var u = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var v = db.DonorInfos.Where(x => x.UserId == u.Id).FirstOrDefault();

            
            db.DonorInfos.Remove(v);
            var es = db.BloodStocks.Where(e => e.UserId == v.UserId).ToList();
            foreach (var item in es)
            {
                db.BloodStocks.Remove(item);
            }

            db.SaveChanges();
            return RedirectToAction("Welcome", "Welcome");
        }

    }




}
