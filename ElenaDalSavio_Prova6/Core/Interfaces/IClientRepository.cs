using ElenaDalSavio_Prova6.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.Core.Interfaces
{
    public interface IClientRepository:IRepository<Core.Models.Client>
    {
        Core.Models.Client GetByCODE(string code);
    }
}
