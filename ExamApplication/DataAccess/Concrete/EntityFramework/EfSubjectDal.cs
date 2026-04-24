using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;

namespace ExamApplication.DataAccess.Concrete.EntityFramework
{
    public class EfSubjectDal : EfEntityRepositoryBase<Subject>, ISubjectDal
    {
        public EfSubjectDal(AppDbContext context) : base(context)
        {
        }
    }
}
