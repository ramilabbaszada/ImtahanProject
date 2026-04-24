using ExamApplication.Business.Dtos;
using ExamApplication.Business.Results.Abstract;
using ExamApplication.Entities.Concrete;

namespace ExamApplication.Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<User>> RegisterAsync(UserDto dto);
        Task<IDataResult<User>> LoginAsync(UserDto dto);
    }
}
