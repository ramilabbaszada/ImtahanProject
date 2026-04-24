using ExamApplication.Business.Abstract;
using ExamApplication.Business.Aspects;
using ExamApplication.Business.Dtos;
using ExamApplication.Business.FluentValidation.ValidationRules;
using ExamApplication.Business.Messages;
using ExamApplication.Business.Results.Abstract;
using ExamApplication.Business.Results.Concrete;
using ExamApplication.Business.Security.Hashing;
using ExamApplication.DataAccess.Abstract;
using ExamApplication.Entities.Concrete;
using System.Security.Cryptography;
using System.Text;
using IResult = ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;

        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public async Task<IDataResult<User>> RegisterAsync(UserDto dto)
        {
            try
            {
                HashingHelper.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

                var user = new User
                {
                    UserName = dto.Username,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };

                await userDal.Add(user);

                return new SuccessDataResult<User>(user, UserServiceMessages.UserRegistered);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(ex.Message);
            }
        }

        [ValidationAspect(typeof(UserValidator))]
        public async Task<IDataResult<User>> LoginAsync(UserDto dto)
        {
            try
            {
                var user = await userDal.Get(u => u.UserName == dto.Username);
                if (user == null)
                    return new ErrorDataResult<User>(UserServiceMessages.InvalidUsernameOrPassword);

                if (!HashingHelper.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
                    return new ErrorDataResult<User>(UserServiceMessages.InvalidUsernameOrPassword);

                return new SuccessDataResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(ex.Message);
            }
        }
    }
}
