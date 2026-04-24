using ExamApplication.Entities.Abstract;

namespace ExamApplication.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
    }
}
