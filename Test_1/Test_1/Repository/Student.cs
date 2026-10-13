using Microsoft.AspNetCore.Mvc.Rendering;
using Test_1.Models;

namespace Test_1.Repository
{
    public class Student : IStudent
    {
        public readonly ApplicationDBContext _db;
        public Student(ApplicationDBContext db)
        {
           _db = db;
        }
        public void AddStudent(Students std)
        {
            _db.Students.Add(std);
            _db.SaveChanges();
        }

        public void DeleteStudent(Guid Id)
        {
            var Std = GetStudentById(Id);
            if(Std!=null)
            {
                _db.Students.Remove(Std);
                _db.SaveChanges();
            }
        }

        public List<Students> GetAll()
        {
            return _db.Students.ToList();
        }

        public List<SelectListItem> GetAllStudentsList()
        {
            return _db.Students.Select(x => new SelectListItem
            {
                Text = x.StdName,
                Value = x.StdId.ToString()
            }).ToList();
        }

        public Students GetStudentById(Guid Id)
        {
            return _db.Students.Where(x => x.StdId == Id).FirstOrDefault();
        }

        public void UpdateStudent(Students std)
        {
            var Std = GetStudentById(std.StdId);

            if (Std!=null) {
                Std.StdName = std.StdName;
                Std.Address = std.Address;
                Std.Standard = std.Standard;
                Std.MobileNo = std.MobileNo;
                Std.Age = std.Age;
            }
            _db.Students.Update(Std);
            _db.SaveChanges();
        }
    }
}
