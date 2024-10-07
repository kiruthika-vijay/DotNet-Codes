using DemoASPNetFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoASPNetFramework.Controllers
{
    public class StudentController : Controller
    {
        static IList<Student> studentList = new List<Student>
        {
            new Student() {StudentId = 1, StudentName = "Kamatchi", Age = 18},
            new Student() {StudentId = 2, StudentName = "Devi", Age = 19},
            new Student() {StudentId = 3, StudentName = "Sundar", Age = 9},
            new Student() {StudentId = 4, StudentName = "Kala", Age = 12},
            new Student() {StudentId = 5, StudentName = "Ram", Age = 16}
        };
        public ActionResult Index()
        {
            ViewBag.HeadCount = studentList.Count();
            return View(studentList);
        }

        //[ActionName("New Student")]
        //[HttpPost]
        ////[AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        //public ActionResult PostAction()
        //{
        //    return View();
        //}

        #region Bind Property - HTTP
        //[HttpPost]
        //public ActionResult Edit([Bind(Exclude = "StudentId, Age")] Student student)
        //{
        //    var id = student.StudentName;
        //    return View();
        //}
        #endregion

        public ActionResult Edit(int id)
        {
            var std = studentList.Where(s => s.StudentId == id).FirstOrDefault();
            return View(std);
        }

        [HttpPost]
        public ActionResult Edit(Student stu)
        {
            var std = studentList.Where(s=>s.StudentId == stu.StudentId).FirstOrDefault();
            studentList.Remove(std);
            studentList.Add(stu);
            return RedirectToAction("Index");
        }
    }
}