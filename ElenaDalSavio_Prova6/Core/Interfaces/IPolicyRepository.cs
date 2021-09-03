using ElenaDalSavio_Prova6.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.Core.Interfaces
{
    public interface IPolicyRepository : IRepository<Policy>
    {
        List<Policy> FetchByCLIENT(Models.Client client);
        bool UpdateDate(Policy policyToUpdate, DateTime newDueDate);
    }
}
