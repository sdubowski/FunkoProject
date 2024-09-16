using FunkoProject.Common.Enums;

namespace FunkoProject.Models;

[Serializable]
public class ErrorMessage
{
    public string Id { get; set; }
    public string Error { get; set; }
    public ErrorTypeEnum ErrorType { get; set; }
}