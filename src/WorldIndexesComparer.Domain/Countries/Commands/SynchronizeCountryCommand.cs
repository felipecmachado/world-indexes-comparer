﻿using MediatR;

namespace WorldIndexesComparer.Domain.Countries.Commands
{
    public class SynchronizeCountryCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string CCA2 { get; set; }
        public string CCA3 { get; set; }
        public int Population { get; set; }
    }
}