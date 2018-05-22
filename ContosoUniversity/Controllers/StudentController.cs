using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        //sortOrder: 添加按条件升降排序功能
        public ActionResult Index(string sortOrder)
        {
            //保存参数给html使用，html点击时改变
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //从数据库学生表中获取所有学生
            var students = from s in db.Students
                           select s;
            switch (sortOrder) {
                case "name_desc":
                    //按照姓氏降序
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "date_desc":
                    //按登记注册日期降序
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                case "Date":
                    //按日期升序
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                default:
                    //按姓氏升序
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            return View(students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]//安全警告-ValidateAntiForgeryToken属性有助于防止跨站点请求伪造攻击。
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,EnrollmentDate")] Student student)
        //Bind属性是一种方法，以防止过度发布中创建的方案。
        //它是使用的安全最佳实践Include参数Bind属性设为允许列表字段。 还有可能要使用Exclude参数阻止列表你想要排除的字段。 原因Include更安全是，当将新属性添加到实体，新的字段不会自动受Exclude列表。
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }


            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        // 实现安全的最佳做法
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentToUpdate, "", 
                new string[] { "LastName","FirstMidName","EnrollmentDate" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */) {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault()){
                ViewBag.ErrorMessage = "删除失败，再试一次，如果问题仍然存在，请反映给系统管理员。";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (DataException/* dex */) {
                //删除异常时，向用户提供机会取消或重试。
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }
            
            return RedirectToAction("Index");
        }

        //关闭数据库连接，并释放持有的资源
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //处理事务
        //默认情况下，实体框架隐式实现事务。 
        //在方案中，对多个行或表进行更改，然后调用SaveChanges，实体框架自动可确保所有所做的更改成功或所有失败。 
        //如果完成某些更改后发生错误，这些更改会自动回退。
    }
}
