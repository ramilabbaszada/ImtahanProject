using Core.DataAccess;
using ExamApplication.Entities.Concrete;

namespace ExamApplication.DataAccess.Abstract
{
    public interface IUserDal: IEntityRepository<User>
    {
    }
}