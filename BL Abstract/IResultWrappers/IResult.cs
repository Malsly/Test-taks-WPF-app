namespace BL.Abstract.ResultWrappers
{
    public interface IResult
    {
        ResponseMessageType Message { get; set; }
        ResponseStatusType ResponseStatusType { get; set; }
    }
}
