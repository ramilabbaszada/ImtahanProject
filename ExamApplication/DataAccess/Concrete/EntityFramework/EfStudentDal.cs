using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;

namespace ExamApplication.DataAccess.Concrete.EntityFramework
{
    public class EfStudentDal: EfEntityRepositoryBase<Student>, IStudentDal     
    {
        public EfStudentDal(AppDbContext context) : base(context)
        {
        }
    }
}
