using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Web_API_Order.Models;

namespace Web_API_Order.Repository
{
    public class Student : IStudent
    {
        private readonly ApplicationDBContext _db;
        public Student(ApplicationDBContext  context)
        {
            _db = context;
        }
        Task IStudent.AddStudentAsync(Students student)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllFromSqlAsync()
        {
            return await _db.Students
                .FromSqlRaw("SELECT Id, FullName, Age, ClassName FROM Students")
                .AsNoTracking()
                .ToListAsync();
        }

        // 2) Stored procedure that returns rows (map to Student)

        public async Task<IEnumerable<Student>> GetByClassStoredProcAsync(int classId)
        {
            var p = new SqlParameter("@ClassId", classId);
            // Ensure your DbContext has DbSet<Student> Students { get; set; }
            var students = await _db.Students
                .FromSqlRaw("EXEC dbo.GetStudentsByClass @ClassId", p)
                .AsNoTracking()
                .ToListAsync();

            return students;
        }
    }
}
