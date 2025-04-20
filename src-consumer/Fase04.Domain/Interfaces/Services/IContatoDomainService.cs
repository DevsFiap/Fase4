using Fase04.Domain.Entities;
using Fase04.Domain.Enums;

namespace Fase04.Domain.Interfaces.Services;

public interface IContatoDomainService
{
    Task<List<Contato>> BuscarContatos();
    Task<Contato> GetByIdAsync(int id);
    Task<IEnumerable<Contato>> GetByDDDAsync(EnumDDD ddd);
    Task CreateContatoAsync(Contato contato);
    Task UpdateContatoAsync(int id, Contato contato);
    Task DeleteContatoAsync(int id);
}