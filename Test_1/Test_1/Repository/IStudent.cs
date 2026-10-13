using Microsoft.AspNetCore.Mvc.Rendering;
using Test_1.Models;

namespace Test_1.Repository
{
    public interface IStudent
    {
        public List<Students> GetAll();
        public void AddStudent(Students std);
        public void UpdateStudent(Students std);
        public void DeleteStudent(Guid Id);
        public Students GetStudentById(Guid Id);

        public List<SelectListItem> GetAllStudentsList();
    }
}
