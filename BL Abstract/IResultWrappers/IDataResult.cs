namespace BL.Abstract.ResultWrappers
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; set; }
    }
}
