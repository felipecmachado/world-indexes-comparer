using MediatR;

namespace WorldIndexesComparer.Domain
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
