using MediatR;

namespace WorldIndexesComparer.Domain
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
