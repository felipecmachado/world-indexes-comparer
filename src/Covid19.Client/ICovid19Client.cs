using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Client
{
    public interface ICovid19Client
    {
        Task<string> GetCasesHistoryByCountry(string name);
        Task<string> GetDeathsHistoryByCountry(string name);
        Task<string> GetRecoveredHistoryByCountry(string name);
    }
}
