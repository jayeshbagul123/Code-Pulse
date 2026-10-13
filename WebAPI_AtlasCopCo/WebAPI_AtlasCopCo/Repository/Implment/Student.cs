using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAPI_AtlasCopCo.DBContext;
using WebAPI_AtlasCopCo.Model;
using WebAPI_AtlasCopCo.Repository.Interface;

namespace WebAPI_AtlasCopCo.Repository.Implment
{
    public class Student : IStudent
    {
        private readonly ApplicationDBContext _context;
        public Student(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Students> AddStudentAsync(Students student)
        {
            var stdId = student.StdId == Guid.Empty ? Guid.NewGuid() : student.StdId;

            var parameters = new[]
            {
        new SqlParameter("@StdId", stdId),
        new SqlParameter("@StdName", student.StdName ?? (object)DBNull.Value),
        new SqlParameter("@Address", student.Address ?? (object)DBNull.Value),
        new SqlParameter("@Standard", student.Standard ?? (object)DBNull.Value),
        new SqlParameter("@MobileNo", student.MobileNo ?? (object)DBNull.Value),
        new SqlParameter("@Age", student.Age)
    };

            // EXEC is non‑composable; load the result set to memory then pick the first item
            var students = await _context.Students
                .FromSqlRaw("EXEC dbo.spAddStudent @StdId, @StdName, @Address, @Standard, @MobileNo, @Age", parameters)
                .AsNoTracking()
                .ToListAsync();

            var addedStudent = students.FirstOrDefault();
            if (addedStudent != null)
                return addedStudent;

            // Fallback if SP didn't return the row
            student.StdId = stdId;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<IEnumerable<Students>> GetAllStudentsAsync()
        {
            return await _context.Students
                .FromSqlRaw("SELECT * FROM dbo.Students")
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Students> GetStudentByIdAsync(Guid id)
        {
            var Guid =new  SqlParameter("@StdId", id);
            var Student = _context.Students.FromSqlRaw("SELECT * FROM Students WHERE StdId = @StdId", Guid).FirstOrDefaultAsync();
            return Student;
        }
    }
}