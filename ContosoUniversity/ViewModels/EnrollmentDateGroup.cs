using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels
{
    //ViewModel类
    //创建一个视图模型类，该视图类是需要传递到该视图的数据的抽象。
    //该类不需写入到数据库，只是用作显示数据的模型
    public class EnrollmentDateGroup
    {
        //配合 Html.DisplayFor 使用
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }

    }
}