using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;

namespace ExamApplication.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal:EfEntityRepositoryBase<User>, IUserDal
    {
        public EfUserDal(AppDbContext context) : base(context)
        {
        }
    }
}
