using AnalyticsServer.Enums;

namespace AnalyticsServer.Models;

public class RequestResult<TType>
{
    public RequestResult(TType? data)
    {
        Result = true;
        Data = data;
    }

    public RequestResult(bool result, ErrorCode errorCode)
    {
        Result = result;
        ErrorCode = errorCode;
    }

    public bool Result { get; }
    public ErrorCode ErrorCode { get; }
    public string? Message { get; }
    public TType? Data { get; }
}