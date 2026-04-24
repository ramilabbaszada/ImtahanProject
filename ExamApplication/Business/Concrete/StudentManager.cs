using ExamApplication.Business.Abstract;
using ExamApplication.Business.Aspects;
using ExamApplication.Business.FluentValidation.ValidationRules;
using ExamApplication.Business.Messages;
using ExamApplication.Business.Results.Abstract;
using ExamApplication.Business.Results.Concrete;
using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;
using IResult = ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Concrete
{
    public class StudentManager : IStudentService
    {
        protected IStudentDal studentDal;
        public StudentManager(IStudentDal studentDal)
        {
            this.studentDal = studentDal;
        }

        [ValidationAspect(typeof(StudentValidator))]
        public async Task<IDataResult<Student>> CreateStudentAsync(Student student)
        {
            try
            {
                await studentDal.Add(student);
                return new SuccessDataResult<Student>(student);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Student>(ex.Message);
            }
        }

        [ValidationAspect(typeof(StudentValidator))]
        public async Task<IDataResult<Student>> UpdateStudentAsync(Student student)
        {
            try
            {
                await studentDal.Update(student);
                return new SuccessDataResult<Student>(student);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Student>(ex.Message);
            }
        }

        [ValidationAspect(typeof(StudentValidator))]
        public async Task<IResult> DeleteStudentAsync(Student student)
        {
            try
            {
                await studentDal.Delete(student);
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IDataResult<List<Student>>> GetAllStudentsAsync()
        {
            try
            {
                var students = await studentDal.GetAll();
                return new SuccessDataResult<List<Student>>(students);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Student>>(ex.Message);
            }
        }

        public async Task<IDataResult<Student?>> GetStudentByIdAsync(int id)
        {
            try
            {
                var student = await studentDal.Get(s => s.Id == id);
                return new SuccessDataResult<Student?>(student);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Student?>(ex.Message);
            }
        }

        public async Task<IDataResult<Student?>> GetStudentByNumberAsync(int number)
        {
            try
            {
                return new SuccessDataResult<Student?>(await studentDal.Get(s => s.Number == number));
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Student?>(ex.Message);
            }
        }
    }
}