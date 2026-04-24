namespace ExamApplication.Business.Results.Abstract
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get; }
    }
}
