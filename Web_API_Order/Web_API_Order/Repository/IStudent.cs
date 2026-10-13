using Web_API_Order.Models;

namespace Web_API_Order.Repository
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAllFromSqlAsync();
        Task<IEnumerable<Student>> GetByClassStoredProcAsync(int classId);
        Task<Student?> GetByIdStoredProcWithOutputAsync(int id);

        Task AddStudentAsync(Students student);
    }
}
