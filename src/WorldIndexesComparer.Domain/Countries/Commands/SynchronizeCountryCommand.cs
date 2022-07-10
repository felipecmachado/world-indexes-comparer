namespace WorldIndexesComparer.Domain.Countries.Commands
{
    public class SynchronizeCountryCommand : ICommand<bool>
    {
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string CCA2 { get; set; }
        public string CCA3 { get; set; }
        public string Slug { get; set; }
        public int Population { get; set; }
    }
}
