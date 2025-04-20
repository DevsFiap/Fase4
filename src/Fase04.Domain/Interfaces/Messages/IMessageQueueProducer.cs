using Fase04.Domain.Models;

namespace Fase04.Domain.Interfaces.Messages
{
    public interface IMessageQueueProducer
    {
        void Create(MessageQueueModel model);
    }
}
