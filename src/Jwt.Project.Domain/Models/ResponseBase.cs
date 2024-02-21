
namespace Jwt.Project.Domain.Models;

public class ResponseBase
{
    public object Data { get; set; }
    public string[] Errors { get; set; } = [];

    private ResponseBase(object data, string[] errors)
    {
        Data = data;
        Errors = errors;
    }   

    private ResponseBase(object data)
    {
        Data = data;
    }

    public static ResponseBase GenerateResponse(object? data = default,  string[]? errors = default)
    {
        if(errors == default)
        {
            return new ResponseBase(data!);
        }

        return new ResponseBase(data!, errors!);
    }

}
