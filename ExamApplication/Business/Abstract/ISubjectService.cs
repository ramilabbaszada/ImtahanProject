using ExamApplication.Business.Results.Abstract;
using ExamApplication.Entities.Concrete;
using IResult = ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Abstract
{
    public interface ISubjectService
    {
        Task<IDataResult<List<Subject>>> GetAllSubjectsAsync();
        Task<IDataResult<Subject?>> GetSubjectByIdAsync(int id);
        Task<IDataResult<Subject?>> GetByCodeAsync(string code);
        Task<IDataResult<Subject>> CreateSubjectAsync(Subject subject);
        Task<IDataResult<Subject>> UpdateSubjectAsync(Subject subject);
        Task<IResult> DeleteSubjectAsync(Subject subject);
    }
}
