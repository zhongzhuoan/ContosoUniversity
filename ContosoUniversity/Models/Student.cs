using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    //学生实体类
    public class Student
    {

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //virtual：虚方法
        //延迟加载，注册纪录列表
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}