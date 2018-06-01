using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    //课程实体类
    public class Course
    {
        //自行指定主键的值，而不是让数据库自动生成值
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        //该课程可得学分
        public int Credits { get; set; }

        //virtual：虚方法
        //延迟加载
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}