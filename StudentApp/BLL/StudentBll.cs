using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.BOL;
using StudentApp.DAL;

namespace StudentApp.BLL
{

    public class StudentBll
    {
        private DataAccessLayer _dal;

        public StudentBll()
        {
            _dal = new DataAccessLayer();
        }

        public List<Student> GetAll()
        {
            var queryTable = _dal.GetQueryResult("SELECT * FROM Student;");

            var studentList = (from row in queryTable.AsEnumerable()
                select new Student()
                {
                    Id = Convert.ToInt32(row["PK_StudentID"]),
                    FirstName = Convert.ToString(row["Firstname"]),
                    LastName = Convert.ToString(row["Lastname"]),
                    Major = Convert.ToString(row["Major"])
                }).ToList<Student>();

            return studentList;
        }

        public Student Get(int id)
        {
            var queryTable = _dal.GetQueryResult($"SELECT * FROM Student WHERE PK_StudentID = {id}");

            return new Student()
            {
                Id = Convert.ToInt32(queryTable.Rows[0]["PK_StudentID"]),
                FirstName = Convert.ToString(queryTable.Rows[0]["Firstname"]),
                LastName = Convert.ToString(queryTable.Rows[0]["Lastname"]),
                Major = Convert.ToString(queryTable.Rows[0]["Major"])
            };
        }

        public void Update(int id, Student student)
        {
             _dal.ExecuteNonQuery($"UPDATE Student SET Firstname = '{student.FirstName}', Lastname = '{student.LastName}', Major = '{student.Major}' WHERE PK_StudentID = {id};");
        }

        public void Delete(int id)
        {
            _dal.ExecuteNonQuery($"DELETE FROM Student WHERE PK_StudentID = {id};");
        }

        public void Create(Student student)
        {
            _dal.ExecuteNonQuery($"INSERT INTO Student (Firstname, Lastname, Major) VALUES('{student.FirstName}', '{student.LastName}', '{student.Major}'); ");
            
        }
    }
}
