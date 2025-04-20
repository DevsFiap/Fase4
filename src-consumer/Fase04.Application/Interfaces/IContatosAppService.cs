using Fase04.Application.Commands;
using Fase04.Application.Dto;
using Fase04.Domain.Enums;

namespace Fase04.Application.Interfaces;

public interface IContatosAppService
{
    Task<ContatoDto> CriarContatoAsync(CriarContatoCommand dto);
    Task<ContatoDto> AtualizarContatoAsync(int id, AtualizarContatoCommand dto);
    Task ExcluirContatoAsync(int id);
}