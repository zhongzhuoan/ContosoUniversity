namespace ContosoUniversity.Models
{
    //等级枚举类
    public enum Grade
    {
        A, B, C, D, F
    }

    //注册纪录实体类
    public class Enrollment
    {

        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        //?可为null，null 意味着一个等级是未知的或未尚未分配。
        public Grade? Grade { get; set; }


        //virtual：虚方法
        //延迟加载
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

    }
}