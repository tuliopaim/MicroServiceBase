using EasyCqrs.Commands;
using MSBase.Cadastro.API.Entities;
using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.Commands;
using MSBase.Core.RabbitMq;
using MSBase.Core.RabbitMq.Messages;
using MSBase.Core.RabbitMq.Messages.Email;

namespace MSBase.Cadastro.API.Commands.NewPersonCommand;

public class NewPersonCommandHandler : ICommandHandler<NewPersonCommandInput, CreatedCommandResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly RabbitMqProducer _rabbitMqProducer;

    public NewPersonCommandHandler(IPersonRepository personRepository, RabbitMqProducer rabbitMqProducer)
    {
        _personRepository = personRepository;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<CreatedCommandResult> Handle(NewPersonCommandInput command, CancellationToken cancellationToken)
    {
        var pessoa = new Person(command.Nome, command.Email, command.Idade);

        _personRepository.Add(pessoa);

        await _personRepository.UnitOfWork.CommitAsync(cancellationToken);

        var emailMessage = new EmailPessoaCadastradaComSucessoMessage(pessoa.Email, pessoa.Nome);

        _rabbitMqProducer.Publish(emailMessage, MessageType.EmailPessoaCadastradaComSucesso, RoutingKeys.NovoEmail);

        return pessoa.Id ;
    }
}