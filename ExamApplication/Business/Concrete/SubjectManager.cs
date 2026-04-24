using ExamApplication.Business.Abstract;
using ExamApplication.Business.Aspects;
using ExamApplication.Business.FluentValidation.ValidationRules;
using ExamApplication.Business.Results.Abstract;
using ExamApplication.Business.Results.Concrete;
using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;
using IResult = ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Concrete
{
    public class SubjectManager : ISubjectService
    {
        private readonly ISubjectDal subjectDal;

        public SubjectManager(ISubjectDal subjectDal)
        {
            this.subjectDal = subjectDal;
        }

        [ValidationAspect(typeof(SubjectValidator))]
        public async Task<IDataResult<Subject>> CreateSubjectAsync(Subject subject)
        {
            try
            {
                await subjectDal.Add(subject);
                return new SuccessDataResult<Subject>(subject);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Subject>(ex.Message);
            }
        }

        [ValidationAspect(typeof(SubjectValidator))]
        public async Task<IDataResult<Subject>> UpdateSubjectAsync(Subject subject)
        {
            try
            {
                await subjectDal.Update(subject);
                return new SuccessDataResult<Subject>(subject);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Subject>(ex.Message);
            }
        }

        [ValidationAspect(typeof(SubjectValidator))]
        public async Task<IResult> DeleteSubjectAsync(Subject subject)
        {
            try
            {
                await subjectDal.Delete(subject);
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IDataResult<List<Subject>>> GetAllSubjectsAsync()
        {
            try
            {
                var subjects = await subjectDal.GetAll();
                return new SuccessDataResult<List<Subject>>(subjects);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Subject>>(ex.Message);
            }
        }

        public async Task<IDataResult<Subject?>> GetSubjectByIdAsync(int id)
        {
            try
            {
                var subject = await subjectDal.Get(s => s.Id == id);
                return new SuccessDataResult<Subject?>(subject);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Subject?>(ex.Message);
            }
        }

        public async Task<IDataResult<Subject?>> GetByCodeAsync(string code)
        {
            try
            {
                var subject = await subjectDal.Get(s => s.Code == code);
                return new SuccessDataResult<Subject?>(subject);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Subject?>(ex.Message);
            }
        }
    }
}
