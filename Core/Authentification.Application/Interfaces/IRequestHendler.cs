namespace Authentification.Application.Interfaces
{
    public interface IRequestHendler<in IRequest>
    {
        Task HendlerAsynk(IRequest request, CancellationToken cancellationToken = default);
    }

    public interface IRequestHendler<in IRequest, IRespons>
    {
        Task<IRespons> HendlerAsynk(IRequest request, CancellationToken cancellationToken = default);
    }
}
