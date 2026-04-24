using ExamApplication.Business.Results.Abstract;
using ExamApplication.Entities.Concrete;
using IResult=ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Abstract
{
    public interface IExamService
    {
        Task<IDataResult<List<Exam>>> GetAllExamsAsync();
        Task<IDataResult<Exam?>> GetExamByIdAsync(int id);
        Task<IDataResult<Exam>> CreateExamAsync(Exam exam);
        Task<IDataResult<Exam>> UpdateExamAsync(Exam exam);
        Task<IResult> DeleteExamAsync(Exam exam);
    }
}
