using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;
using Microsoft.Identity.Client;

namespace ExamApplication.DataAccess.Concrete.EntityFramework
{
    public class EfExamDal: EfEntityRepositoryBase<Exam>, IExamDal
    {
        public EfExamDal(AppDbContext context) : base(context)
        {
        }
    }
}
