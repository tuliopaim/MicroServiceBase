using Cadastro.Domain.Entities;
using Cadastro.Domain.Repositories;
using Core.Application.Commands;
using Core.Infrastructure.Email;
using Core.Infrastructure.Kafka;
using Core.Infrastructure.Kafka.KafkaMessageTypes;

namespace Cadastro.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandHandler : ICommandHandler<NovaPessoaCommandInput, NovaPessoaCommandResult>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IKafkaBroker _broker;

        public NovaPessoaCommandHandler(IPessoaRepository pessoaRepository, IKafkaBroker kafkaBroker)
        {
            _pessoaRepository = pessoaRepository;
            _broker = kafkaBroker;
        }

        public async Task<NovaPessoaCommandResult> Handle(NovaPessoaCommandInput command, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(command.Nome, command.Email, command.Idade);

            _pessoaRepository.Add(pessoa);

            await _pessoaRepository.UnitOfWork.CommitAsync(cancellationToken);
                        
            await _broker.PublishAsync(
                KafkaTopics.NovoEmail, 
                EmailMessageTypes.EmailPessoaCadastradaComSucesso, 
                new EmailPessoaCadastradaComSucessoMessage(pessoa.Email, pessoa.Nome),
                cancellationToken);

            return new NovaPessoaCommandResult(pessoa.Id);
        }
    }
}