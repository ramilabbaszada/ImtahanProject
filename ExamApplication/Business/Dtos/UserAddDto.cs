namespace ExamApplication.Business.Dtos
{
    public record UserDto()
    {
        public string Username { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}
