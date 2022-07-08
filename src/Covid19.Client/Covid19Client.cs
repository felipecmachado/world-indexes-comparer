using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Client
{
    public class Covid19Client : ICovid19Client
    {
        public Task<string> GetCasesHistoryByCountry(string name)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDeathsHistoryByCountry(string name)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRecoveredHistoryByCountry(string name)
        {
            throw new NotImplementedException();
        }
    }
}
