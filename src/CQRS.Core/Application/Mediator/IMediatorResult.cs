using System.Collections.Generic;
using System.Net;

namespace CQRS.Core.Application.Mediator
{
    public interface IMediatorResult
    {
        IEnumerable<string> Errors { get; }

        IMediatorResult AddError(string error);

        bool IsValid();
    }
}