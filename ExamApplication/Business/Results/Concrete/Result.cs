using IResult= ExamApplication.Business.Results.Abstract.IResult;

namespace ExamApplication.Business.Results.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; }

        public string Message { get; }
        public Result(bool Success, string Message) : this(Success)
        {
            this.Message = Message;
        }
        public Result(bool Success)
        {
            this.Success = Success;
        }
    }
}
