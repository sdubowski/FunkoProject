using FunkoProject.Common.Enums;

namespace FunkoProject.Models;

[Serializable]
public class ServiceMessage
{
    public List<ErrorMessage> ErrorMessages { get; set; } = new List<ErrorMessage>();

    public bool Success => ErrorMessages.Any(x => x.ErrorType == ErrorTypeEnum.Error ||
                                                  x.ErrorType == ErrorTypeEnum.CriticalError ||
                                                  x.ErrorType == ErrorTypeEnum.Alert);

    public bool HasErrors => ErrorMessages.Any();
}

[Serializable]
public class ServiceMessage<T> : ServiceMessage
{
    public T Content { get; set; }
    public int Id { get; set; }
}