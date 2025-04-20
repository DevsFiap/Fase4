using Fase04.Application.Dto;
using Fase04.Domain.Enums;

namespace TechChallengeFase01.Tests.Builders
{
    public class ContatoDtoBuilder
    {
        private ContatoDto _contatoDto;

        public ContatoDtoBuilder()
        {
            _contatoDto = new ContatoDto
            {
                Nome = "Nome",
                NumeroTelefone = "81999999999",
                Email = "email@email.com",
                DDDTelefone = EnumDDD.Recife_PE,
                DataCriacao = DateTime.UtcNow
            };
        }

        public ContatoDto Build()
        {
            return _contatoDto;
        }
    }
}
