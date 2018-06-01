using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

//新文件夹命名DAL （适用于数据访问层）
namespace ContosoUniversity.DAL
{
    //数据库Context，指定Model，可供Controller使用
    public class SchoolContext : DbContext
    {
        //定义数据库名称（稍后将添加到 Web.config 文件）
        public SchoolContext() : base("SchoolContext")
        {
        }

        //DbSet：指定实体集，即指定数据库的表
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        //复写父类
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //指定采用单数形式的表名称
            //表名将为Student， Course，和Enrollment
            //如果你未执行此操作，数据库中生成的表将被命名为Students， Courses，和Enrollments
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}