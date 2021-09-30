using System.Collections.Generic;
using System.Linq;
using System.Net;
using CQRS.Core.Application.Interfaces;

namespace CQRS.Core.Application
{
    public class MediatorResult : IMediatorResult
    {
        private readonly List<string> _errors = new();

        public IMediatorResult WithHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            return this;
        }

        public IMediatorResult AddError(string error)
        {
            _errors.Add(error);
            return this;
        }

        public bool IsValid()
        {
            return !Errors.Any();
        }

        public HttpStatusCode? HttpStatusCode { get; private set; }

        public IEnumerable<string> Errors => _errors;
    }
}