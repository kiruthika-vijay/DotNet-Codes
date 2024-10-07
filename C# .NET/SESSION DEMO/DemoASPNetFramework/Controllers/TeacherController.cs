using DemoASPNetFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoASPNetFramework.Controllers
{
    public class TeacherController : Controller
    {
        static IList<Teacher> teacherList = new List<Teacher>()
        {
            new Teacher() {TeacherId = 1, Name = "Shyamala", Gender = "Female", Email = "shyamala@gmail.com", Phone = "9834021389", Address = "Ragava Nagar, CH", Department = "Software Developer", Status = "Active", Salary = 890000, ExpYears = 3 },
            new Teacher() {TeacherId = 2, Name = "Rishikesh", Gender = "Male", Email = "rishik@gmail.com", Phone = "7829892838", Address = "StPaul Street, CH", Department = "Data Analytics", Status = "Active", Salary = 1200000, ExpYears = 4 },
            new Teacher() {TeacherId = 3, Name = "Jayanthi", Gender = "Female", Email = "jayanthi@gmail.com", Phone = "9128740378", Address = "Lanson Nagar, CH", Department = "SalesForce Developer", Status = "Active", Salary = 845000, ExpYears = 5 },
            new Teacher() {TeacherId = 4, Name = "Kishore", Gender = "Male", Email = "kishore@gmail.com", Phone = "6891209378", Address = "Stephen Junction, CH", Department = "DevOps Engineer", Status = "Active", Salary = 2460000, ExpYears = 5 },
            new Teacher() {TeacherId = 5, Name = "Lavanya", Gender = "Female", Email = "lavanya@gmail.com", Phone = "7289016748", Address = "Ransom Road, CH", Department = "Cloud Architect", Status = "Active", Salary = 1347000, ExpYears = 3 },
            new Teacher() {TeacherId = 6, Name = "Seetha", Gender = "Female", Email = "seetham@gmail.com", Phone = "7289016748", Address = "Ransom Road, CH", Department = "Java Core", Status = "Active", Salary = 1347000, ExpYears = 3 },
            new Teacher() {TeacherId = 7, Name = "Sundar", Gender = "Male", Email = "sundarp@gmail.com", Phone = "7289016748", Address = "Ransom Road, CH", Department = "DotNet Developer", Status = "Active", Salary = 1347000, ExpYears = 3 },
            new Teacher() {TeacherId = 8, Name = "Bhoopesh", Gender = "Male", Email = "bhoopesh@gmail.com", Phone = "7289016748", Address = "Ransom Road, CH", Department = "Python Developer", Status = "Active", Salary = 1347000, ExpYears = 3 }
        };
        //GET: Teacher
        public ActionResult Index()
        {
            ViewBag.HeadCount = teacherList.Count();
            return View(teacherList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            teacherList.Add(teacher);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var teacher = teacherList.Where(t => t.TeacherId == id).FirstOrDefault();
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            var teach = teacherList.Where(t => t.TeacherId == teacher.TeacherId).FirstOrDefault();
            teacherList.Remove(teach);
            teacherList.Add(teacher);
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            var teach = teacherList.Where(t => t.TeacherId == id).FirstOrDefault();
            teacherList.Remove(teach);
            return RedirectToAction("Index");
        }

        #region ACTION PARAMS
        //[ActionName("find")] // Aliasing the action method name for URL simplification
        //public ActionResult GetById(int id)
        //{
        //    return View();
        //}

        //[NonAction] // If it needs to be a public method but don't want to be considered as Action Method
        //public string GetName(int id)
        //{
        //    return "Hello";
        //}
        #endregion
    }
}