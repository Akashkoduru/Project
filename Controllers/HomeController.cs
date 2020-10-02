using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineLeaveManagement.DAL;

namespace onlineLeaveManagement.Controllers
{
    public class HomeController : Controller
    {
        leave1Entities2 entities = new leave1Entities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult check(string username,string password)
        {
            admin ad = entities.admins.Find(username);
            if(ad!=null)
            {
                if (ad.username.Trim().ToLower().Equals(username.ToLower())&& ad.password.Trim().Equals(password))
                {
                    ViewData["checked"] = "true";
                }
                else
                {
                    ViewData["checked"] = "false";
                }
            }
            else
            {
                ViewData["checked"] = "false";
            }   
            return View();
        }
        public ActionResult checkfac(int id, string password)
        {
            Detail d = entities.Details.Find(id);
            if (d != null)
            {
                if (d.empId.Equals(id) && d.empType.ToLower().Equals("faculty") && d.password.Trim().Equals(password))
                {
                    ViewData["checked"] = "true";
                }
                else
                {
                    ViewData["checked"] = "false";
                }
            }
            else
            {
                ViewData["checked"] = "false";
            }
            return View();
        }
        public ActionResult checkpri(string id, string password)
        {
            admin d = entities.admins.Find(id);
            if (d != null)
            {
                if (d.username.Equals(id)  && d.password.Trim().Equals(password))
                {
                    ViewData["checked"] = "true";
                }
                else
                {
                    ViewData["checked"] = "false";
                }
            }
            else
            {
                ViewData["checked"] = "false";
            }
            return View();
        }
        public ActionResult checkhod(int id, string password)
        {
            Detail d = entities.Details.Find(id);
            if (d != null)
            {
                if (d.empId.Equals(id) && d.empType.ToLower().Equals("hod") && d.password.Trim().Equals(password))
                {
                    ViewData["checked"] = "true";
                }
                else
                {
                    ViewData["checked"] = "false";
                }
            }
            else
            {
                ViewData["checked"] = "false";
            }
            return View();
        }
        public ActionResult password(int id,string email,string password)
        {
            Detail d = entities.Details.Find(id);
            if(d!=null)
            {
                if(d.email.Trim().ToLower().Equals(email))
                {
                    ViewData["password"] = "true";
                    d.password = password;
                    entities.SaveChanges();
                }
            }
            else
            {
                ViewData["password"] = "false";
            }
            return View();
        }
        public  ActionResult request(int empId, string empName, string empType, string dept, string reason, int leaves, DateTime from, DateTime to)
        {
            leaves1 d = new leaves1();
            ViewData["request"] = "true";
            d.empId = empId;
            d.empName = empName;
            d.empType = empType;
            d.dept = dept;
            d.reason = reason ;
            d.leaves = leaves;
            d.from = from;
            d.to = to;
            d.status = "pending"; 
            entities.leaves1.Add(d);
            entities.SaveChanges();
            return View();
        }
        public ActionResult create(int empId,string empName,string empType,string dept,string email,string phone,string password,int available)
        {
            Detail d= new Detail();
            Detail e = entities.Details.Find(empId);
            if (e != null)
            {
                ViewData["create"] = "false";
            }
            else
            {
                ViewData["create"] = "true";
                d.empId = empId;
                d.empName = empName;
                d.empType = empType;
                d.dept = dept;
                d.email = email;
                d.phone = phone;
                d.password = password;
                d.available = available;
                entities.Details.Add(d);
                entities.SaveChanges();
            }
            return View();
        }
        public ActionResult ViewStatus(int id)
        {
            ViewData["id"] = id; 
            return View(entities.leaves1.ToList());
        }
        public ActionResult ViewLeaves(int id)
        {
            Detail d = entities.Details.Find(id);
            if (d != null)
            {
                ViewData["leave"] = "true";
            }
            else
            {
                ViewData["leave"] = "false";
            }
            return View(d);
        }
        public ActionResult delete(int empId)
        {
            Detail d = entities.Details.Find(empId);
            if (d != null)
            {
                Detail detail = entities.Details.Remove(d);
                entities.SaveChanges();
            }
            else
            {
                ViewData["delete"] = "false";
            }
            return View();
        }
        public ActionResult accept(int id)
        {
            leaves1 l = entities.leaves1.Find(id);
            Detail d = entities.Details.Find(l.empId);
            l.status = "Accepted";
            d.available = d.available - l.leaves;
            entities.SaveChanges();
            return View();
        }
        public ActionResult reject(int id)
        {
            leaves1 l = entities.leaves1.Find(id);
            Detail d = entities.Details.Find(id);
            l.status = "Rejected";    
            entities.SaveChanges();
            return View();
        }
        public ActionResult ViewResult()
        {
            return View(entities.Details.ToList());
        }
        public ActionResult ViewRequests()
        { 
            return View(entities.leaves1.ToList());
        }
        public ActionResult ViewHodRequests()
        {
            return View(entities.leaves1.ToList());
        }
        public ActionResult updateName(int empId,string empName)
        {
            Detail d = entities.Details.Find(empId);
            if(d!=null)
            {
                ViewData["Name"] = "true";
                d.empName = empName;
                entities.SaveChanges();
            }
            else
            {
                ViewData["Name"] = "false";
            }
            return View();
        }
        public ActionResult updateType(int empId, string type)
        {
            Detail d = entities.Details.Find(empId);
            if (d != null)
            {
                ViewData["type"] = "true";
                d.empType = type;
                entities.SaveChanges();
            }
            else
            {
                ViewData["type"] = "false";
            }
            return View();
        }
        public ActionResult updateDept(int empId, string dept)
        {
            Detail d = entities.Details.Find(empId);
            if (d != null)
            {
                ViewData["dept"] = "true";
                d.dept = dept;
                entities.SaveChanges();
            }
            else
            {
                ViewData["dept"] = "false";
            }
            return View();
        }
        public ActionResult updateEmail(int empId, string email)
        {
            Detail d = entities.Details.Find(empId);
            if (d != null)
            {
                ViewData["email"] = "true";
                d.email = email;
                entities.SaveChanges();
            }
            else
            {
                ViewData["email"] = "false";
            }
            return View();
        }
        public ActionResult updatePassword(int empId, string pass)
        {
            Detail d = entities.Details.Find(empId);
            if (d != null)
            {
                ViewData["pass"] = "true";
                d.password = pass;
                entities.SaveChanges();
            }
            else
            {
                ViewData["pass"] = "false";
            }
            return View();
        }
        public ActionResult updatePhone(int empId, string phone)
        {
            Detail d = entities.Details.Find(empId);
            if (d != null)
            {
                ViewData["phone"] = "true";
                d.phone = phone;
                entities.SaveChanges();
            }
            else
            {
                ViewData["phone"] = "false";
            }
            return View();
        }
    }
}