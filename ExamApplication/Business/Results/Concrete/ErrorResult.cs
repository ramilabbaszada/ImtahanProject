namespace ExamApplication.Business.Results.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult(string Message) : base(false, Message)
        {

        }
        public ErrorResult() : base(false)
        {

        }
    }
}
