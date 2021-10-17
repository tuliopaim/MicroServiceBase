using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public HttpStatusCode? HttpStatusCode { get; private set; }
        
        public IEnumerable<string> Errors => _errors;
    }
}