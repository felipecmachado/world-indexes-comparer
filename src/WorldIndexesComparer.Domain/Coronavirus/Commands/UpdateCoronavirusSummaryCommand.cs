using MediatR;

namespace WorldIndexesComparer.Domain.Coronavirus.Commands
{
    public class UpdateCoronavirusSummaryCommand : IRequest<bool>
    {
        public Summary Summary { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public int Active { get; set; }
        public DateTime Date { get; set; }
    }
}
