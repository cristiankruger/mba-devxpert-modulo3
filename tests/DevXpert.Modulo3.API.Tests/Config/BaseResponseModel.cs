namespace DevXpert.Modulo3.API.Tests.Config;

public class BaseResponseModel
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
