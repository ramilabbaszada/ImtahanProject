using ExamApplication.Business.Results.Abstract;
using ExamApplication.Entities.Concrete;
using IResult = ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Abstract
{
    public interface IStudentService
    {
        Task<IDataResult<List<Student>>> GetAllStudentsAsync();
        Task<IDataResult<Student?>> GetStudentByIdAsync(int id);
        Task<IDataResult<Student?>> GetStudentByNumberAsync(int number);
        Task<IDataResult<Student>> CreateStudentAsync(Student student);
        Task<IDataResult<Student>> UpdateStudentAsync(Student student);
        Task<IResult> DeleteStudentAsync(Student student);
    }
}
