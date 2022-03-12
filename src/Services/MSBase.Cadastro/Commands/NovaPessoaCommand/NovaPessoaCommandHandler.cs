using MSBase.Cadastro.API.Entities;
using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.Application.Commands;
using MSBase.Core.Infrastructure.RabbitMq.Messages.Email;

namespace MSBase.Cadastro.API.Commands.NovaPessoaCommand;

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