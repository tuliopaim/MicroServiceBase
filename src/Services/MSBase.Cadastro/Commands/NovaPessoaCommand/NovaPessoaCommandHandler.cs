using MSBase.Cadastro.API.Entities;
using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.Application.Commands;
using MSBase.Core.Infrastructure.RabbitMq;
using MSBase.Core.Infrastructure.RabbitMq.Messages.Email;

namespace MSBase.Cadastro.API.Commands.NovaPessoaCommand;

public class NovaPessoaCommandHandler : ICommandHandler<NovaPessoaCommandInput, NovaPessoaCommandResult>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly RabbitMqProducer _rabbitMqProducer;

    public NovaPessoaCommandHandler(IPessoaRepository pessoaRepository, RabbitMqProducer rabbitMqProducer)
    {
        _pessoaRepository = pessoaRepository;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<NovaPessoaCommandResult> Handle(NovaPessoaCommandInput command, CancellationToken cancellationToken)
    {
        var pessoa = new Pessoa(command.Nome, command.Email, command.Idade);

        _pessoaRepository.Add(pessoa);

        await _pessoaRepository.UnitOfWork.CommitAsync(cancellationToken);

        var emailMessage = new EmailPessoaCadastradaComSucessoMessage(pessoa.Email, pessoa.Nome);

        _rabbitMqProducer.Publish(emailMessage, RoutingKeys.NovoEmail);
        
        return new NovaPessoaCommandResult(pessoa.Id);
    }
}