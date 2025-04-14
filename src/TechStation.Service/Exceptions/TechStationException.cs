namespace TechStation.Service.Exceptions;

public class TechStationException : Exception
{
    public int StatusCode { get; set; }
    public TechStationException(int code,string message) : base(message)
    {
        StatusCode = code;
    }
}
