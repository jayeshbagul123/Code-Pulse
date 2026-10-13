using WebAPI_AtlasCopCo.Model;

namespace WebAPI_AtlasCopCo.Repository.Interface
{
    public interface IStudent
    {
        public Task<IEnumerable<Students>> GetAllStudentsAsync();

        public Task<Students> GetStudentByIdAsync(Guid id);
        public Task<Students> AddStudentAsync(Students student);

    }
}
