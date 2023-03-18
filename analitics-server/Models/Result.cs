namespace AnalyticsServer.Models;

public class RequestResult<T, TErrorModel>
{
    public RequestResult(bool result)
    {
        Result = result;
    }

    public RequestResult(bool result, string message)
    {
        Result = result;
        Message = message;
    }

    public RequestResult(bool result, bool status, T? data)
    {
        Result = result;
        Status = status;
        Data = data;
    }

    public RequestResult(bool result, bool status, TErrorModel? errorModelData = default, T? data = default)
    {
        Result = result;
        Status = status;
        Data = data;
        ErrorModelData = errorModelData;
    }

    public bool Result { get; }
    public bool Status { get; }
    public string? Message { get; }

    public T? Data { get; }
    public TErrorModel? ErrorModelData { get; }
}