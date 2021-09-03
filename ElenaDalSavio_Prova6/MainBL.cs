using ElenaDalSavio_Prova6.Core.Interfaces;
using ElenaDalSavio_Prova6.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6
{

    public class MainBL
    {
        private IClientRepository _clientRepo;
        private IPolicyRepository _policiesRepo;
        public MainBL(IPolicyRepository policiesRepository, IClientRepository clientRepository)
        {
            _policiesRepo = policiesRepository;
            _clientRepo = clientRepository;
        }

        internal bool AddPolicy(Policy policy)
        {
            //validazione
            if (policy == null) throw new ArgumentNullException();

            bool isAdded = _policiesRepo.Add(policy);
            return isAdded; //darà vero se il saveChanges è andato a buon fine e il metodo
                            //Add del repo restituisce true
        }
        internal bool AddClient(Core.Models.Client client)
        {
            //validazione
            if (client == null) throw new ArgumentNullException();

            bool isAdded = _clientRepo.Add(client);
            return isAdded; //darà vero se il saveChanges è andato a buon fine e il metodo
                            //Add del repo restituisce true
        }

        internal List<Policy> FetchPolicies()
        {
            var policies = _policiesRepo.Fetch();
            return policies;
        }

        internal bool DeletePolicy(Policy policyToDelete)
        {
            //validazione
            if (policyToDelete == null) throw new ArgumentNullException();

            bool isDeleted = _policiesRepo.Delete(policyToDelete);
            return isDeleted;
        }

        internal bool UpdatePolicy(Policy policyToUpdate)
        {
            //validazione input
            if (policyToUpdate == null) throw new ArgumentNullException();

            bool isUpdated = _policiesRepo.Update(policyToUpdate);
            return isUpdated;
        }
        internal bool UpdateDatePolicy(Policy policyToUpdate, DateTime newDate)
        {
            if (policyToUpdate == null) throw new ArgumentNullException();

            bool isUpdatedDate = _policiesRepo.UpdateDate(policyToUpdate, newDate);
            return isUpdatedDate;
        }

        internal List<Core.Models.Client> FetchClient()
        {
            try
            {
                return _clientRepo.Fetch();
            }
            catch
            {
                return null;
            }
        }

        internal Core.Models.Client GetByCode(string code)
        {
            //validazione 
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException();

            var client = _clientRepo.GetByCODE(code);
            return client;
        }

        internal List<Policy> FetchByClient(Core.Models.Client client)
        {
            if (client==null) throw new ArgumentNullException();
           List<Policy> policies = _policiesRepo.FetchByCLIENT(client);
            return policies;

        }

        internal Policy GetById(int choice)
        {

            try
            {
                return _policiesRepo.GetById(choice);
            }
            catch
            {
                return null;
            }
            
        }
    }
}

