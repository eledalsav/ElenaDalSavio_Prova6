using ElenaDalSavio_Prova6.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.EF.Repositories
{
    public class EFClientRepository:IClientRepository
    {
        private readonly PoliciesContext pcx;
        public EFClientRepository()
        {
            pcx = new PoliciesContext();
        }
        public bool Add(Core.Models.Client item)
        {
            if (item == null)
                return false;

            try
            {
                pcx.Clients.Add(new Core.Models.Client
                {
                    Code=item.Code,
                    Name=item.Name,
                    LastName=item.LastName
                });

                pcx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Core.Models.Client item)
        {
            if (item == null)
                return false;

            try
            {
                var spesa =pcx.Clients.Find(item.Id);

                if (spesa != null)
                    pcx.Clients.Remove(item);

                pcx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Core.Models.Client> Fetch()
        {
            try
            {
                var clienti = pcx.Clients
                    .ToList();
                return clienti;
            }
            catch (Exception)
            {
                return new List<Core.Models.Client>();
            }
        }

        public Core.Models.Client GetById(int id)
        {
            if (id <= 0)
                return null;

            return pcx.Clients.Find(id);
        }

        public bool Update(Core.Models.Client item)
        {
            if (item == null)
                return false;

            try
            {
                pcx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Core.Models.Client GetByCODE(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            try
            {
                var client = pcx.Clients.Where(b => b.Code==code).FirstOrDefault();

                return client;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
