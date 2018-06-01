using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        //添加数据库上下文
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            return View();
        }

        //修改关于成注册日期统计
        public ActionResult About()
        {
            //LINQ 语句将学生实体按修读日期分组，计算每个组中的实体数并将结果存储在EnrollmentDateGroup视图模型对象的集合中。
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "联系页面";

            return View();
        }

        //复写父类方法
        protected override void Dispose(bool disposing)
        {
            //关闭数据库，并释放持有资源
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}