namespace Fase04.Infra.Messages.Helpers;

public interface IMailHelper
{
    void Send(string mailTo, string subject, string body);
}