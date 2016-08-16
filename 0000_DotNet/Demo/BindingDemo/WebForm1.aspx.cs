using System;
using System.Collections.Generic;
using System.Linq;

namespace BindingDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var students = new[] { 
                 new Student{Name="mike",Age=23},
                 new Student{Name="jane", Age=12},
                 new Student{Name="frank",Age=25},
                 new Student{Name="susan",Age=32}
            };
            rptStudents.DataSource = students;

            var group0 = new Group();
            group0.Students = students.Take(2);
            var group1 = new Group();
            group1.Students = students.Skip(2).Take(2);
            rptGroups.DataSource = new[] { group0, group1 };

            DataBind();
        }

        protected TResult Stu<TResult>(Func<Student, TResult> func)
        {
            return ExpHelper<Student, TResult>(func);
        }

        protected TResult Grp<TResult>(Func<Group, TResult> func)
        {
            return ExpHelper<Group, TResult>(func);
        }

        protected virtual TResult ExpHelper<TEntity, TResult>(Func<TEntity, TResult> func)
        {
            var itm = GetDataItem();
            return func((TEntity)itm);
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Group
    {
        public IEnumerable<Student> Students { get; set; }
    }
}