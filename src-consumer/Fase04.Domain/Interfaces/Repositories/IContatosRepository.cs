using Fase04.Domain.Core;
using Fase04.Domain.Entities;
using Fase04.Domain.Enums;

namespace Fase04.Domain.Interfaces.Repositories;

public interface IContatosRepository : IBaseRepository<Contato>
{
    Task<IEnumerable<Contato>> GetByDDDAsync(EnumDDD ddd);
}