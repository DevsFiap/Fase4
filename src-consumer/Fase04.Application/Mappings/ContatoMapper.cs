using Fase04.Application.Commands;
using Fase04.Domain.Entities;
using Fase04.Domain.Enums;

namespace Fase04.Application.Mappings;

public static class ContatoMapper
{
    public static Contato ConverterParaContato(AtualizarContatoCommand atualizarContatoDto, Contato contatoExistente)
    {
        if (atualizarContatoDto == null)
            throw new ArgumentNullException(nameof(atualizarContatoDto));

        contatoExistente.Nome = atualizarContatoDto.Nome;

        // Formatar e separar o telefone
        if (!string.IsNullOrWhiteSpace(atualizarContatoDto.Telefone))
        {
            var ddd = atualizarContatoDto.Telefone.Substring(0, 2);
            var numeroTelefone = atualizarContatoDto.Telefone.Substring(2);
            contatoExistente.Telefone = numeroTelefone;
            contatoExistente.DDDTelefone = (EnumDDD)int.Parse(ddd);
        }

        contatoExistente.Email = atualizarContatoDto.Email;

        return contatoExistente;
    }
}