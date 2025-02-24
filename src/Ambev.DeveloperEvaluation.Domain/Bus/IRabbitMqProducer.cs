using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Bus;

public interface IRabbitMqProducer
{
    void Publish(string evento, Sale sale);
}
