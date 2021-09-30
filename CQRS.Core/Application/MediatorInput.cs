using CQRS.Core.Application.Interfaces;

namespace CQRS.Core.Application
{
    public class MediatorInput<TMediatorResult> : IMediatorInput<TMediatorResult> where TMediatorResult : IMediatorResult
    {
    }
}