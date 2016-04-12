using Selkie.Aop.Messages;

namespace Selkie.Services.Aco
{
    public interface IExceptionThrownMessageToStringConverter
    {
        string Convert(ExceptionThrownMessage message);
    }
}