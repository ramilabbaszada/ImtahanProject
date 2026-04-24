using ExamApplication.Business.Abstract;
using ExamApplication.Business.Aspects;
using ExamApplication.Business.FluentValidation.ValidationRules;
using ExamApplication.Business.Messages;
using ExamApplication.Business.Results.Abstract;
using ExamApplication.Business.Results.Concrete;
using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;
using IResult=ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Concrete
{
    public class ExamManager : IExamService
    {
        IExamDal examDal { get; set; }
        IStudentService studentService { get; set; }
        ISubjectService subjectService { get; set; }

        public ExamManager(IExamDal examDal, IStudentService studentService, ISubjectService subjectService)
        {
            this.examDal = examDal;
            this.studentService = studentService;
            this.subjectService = subjectService;
        }

        [ValidationAspect(typeof(ExamValidator))]// Is used for validation of the Exam entity before creating it. It ensures that the Exam entity meets the defined validation rules in the ExamValidator class.
        public async Task<IDataResult<Exam>> CreateExamAsync(Exam exam)
        {
            try
            {
                var relationValidation = await ValidateStudentSubjectClassAsync(exam);
                if (relationValidation != null)
                    return relationValidation;

                await examDal.Add(exam);

                return new SuccessDataResult<Exam>(exam);

            }
            catch(Exception ex) 
            {
                // Handle the exception(such as logging) and return an error result or handle it in logging aspect
                return new ErrorDataResult<Exam>(ex.Message);
            }
        }

        [ValidationAspect(typeof(ExamValidator))]// Is used for validation of the Exam entity before deleting it. It ensures that the Exam entity meets the defined validation rules in the ExamValidator class.
        public async Task<IResult> DeleteExamAsync(Exam exam)
        {
            try
            {

                await examDal.Delete(exam);
                return new SuccessResult();
            }
            catch (Exception ex) {
                // Handle the exception(such as logging) and return an error result or handle it in logging aspect
                return new ErrorDataResult<Exam>(ex.Message);
            }
        }

        public async Task<IDataResult<List<Exam>>> GetAllExamsAsync()
        {
            try
            {
                return new SuccessDataResult<List<Exam>>(await examDal.GetAll());
            }
            catch (Exception ex)
            {
                // Handle the exception(such as logging) and return an error result or handle it in logging aspect
                return new ErrorDataResult<List<Exam>>(ex.Message);
            }
        }

        public async Task<IDataResult<Exam?>> GetExamByIdAsync(int id)
        {
            try 
            {
                return new SuccessDataResult<Exam?>(await examDal.Get(e => e.Id == id));
            }
            catch (Exception ex)
            {
                // Handle the exception(such as logging) and return an error result or handle it in logging aspect
                return new ErrorDataResult<Exam?>(ex.Message);
            }
        }
        [ValidationAspect(typeof(ExamValidator))]
        public async Task<IDataResult<Exam>>  UpdateExamAsync(Exam exam)
        {
            try 
            {
                var relationValidation = await ValidateStudentSubjectClassAsync(exam);
                if (relationValidation != null)
                    return relationValidation;

                await examDal.Update(exam);
                return new SuccessDataResult<Exam>(exam);
            }
            catch (Exception ex)
            {
                // Handle the exception(such as logging) and return an error result or handle it in logging aspect
                return new ErrorDataResult<Exam>(ex.Message);
            }
        }

        private async Task<IDataResult<Exam>?> ValidateStudentSubjectClassAsync(Exam exam)
        {
            var student = await studentService.GetStudentByNumberAsync(exam.StudentNumber);
            if (student.Data==null)
                return new ErrorDataResult<Exam>(StudentServiceMessages.StudentNotFound);

            var subject = await subjectService.GetByCodeAsync(exam.SubjectCode);
            if (subject.Data==null)
                return new ErrorDataResult<Exam>(SubjectServiceMessages.SubjectIsNotFound);

            if (student.Data.Class != subject.Data.Class)
                return new ErrorDataResult<Exam>(ExamServiceMessages.StudentAndSubjectConsistencyError);

            return null;
        }
    }
}
