using System.Collections.Generic;
using System.Net;

namespace CQRS.Core.Application.Mediator
{
    public interface IMediatorResult
    {
        HttpStatusCode? HttpStatusCode { get; }

        IEnumerable<string> Errors { get; }

        IMediatorResult AddError(string error);

        IMediatorResult WithHttpStatusCode(HttpStatusCode httpStatusCode);

        bool IsValid();
    }
}