namespace DevXpert.Modulo3.API.Tests.Config;

public class BaseResponseModel
{
    public bool Success { get; set; }
    public int Status { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Detail{ get; set; }
    public string TraceId{ get; set; }
    public IEnumerable<string> Errors { get; set; }
}
