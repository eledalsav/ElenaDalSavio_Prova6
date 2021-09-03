using ElenaDalSavio_Prova6.Core.Interfaces;
using ElenaDalSavio_Prova6.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.EF.Repositories
{
    class EFPolicyRepository:IPolicyRepository
    {
        private readonly PoliciesContext pcx;

        public EFPolicyRepository()
        {
            pcx = new PoliciesContext();
        }

        public bool Add(Policy policy)
        {
            if (policy == null)
                return false;

            try
            {
                pcx.Policies.Add(new Policy
                {
                    PolicyNumber=policy.PolicyNumber,
                    DueDate=policy.DueDate,
                    MonthlyPayment=policy.MonthlyPayment,
                    Type=policy.Type,                  
                    ClientId = policy.ClientId
                });

                pcx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Policy policyToDelete)
        {
            if (policyToDelete == null)
                return false;

            try
            {
                var policy = pcx.Policies.Find(policyToDelete.Id);

                if (policy != null)
                    pcx.Policies.Remove(policy);

                pcx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Policy> Fetch()
        {
            try
            {
                var policies =pcx.Policies.Include(s => s.Client)
                    .ToList();
                return policies;
            }
            catch (Exception)
            {
                return new List<Policy>();
            }
        }

        public List<Policy> FetchByCLIENT(Core.Models.Client client)
        {
            try
            {
                var policies = pcx.Policies.Where(s => s.Client == client).ToList();
                return policies;
            }
            catch (Exception)
            {
                return new List<Policy>();
            }
        }

        public Policy GetById(int id)
        {
            if (id <= 0)
                return null;

            return pcx.Policies.Find(id);
        }


        public bool Update(Policy policyToUpdate)
        {
            if (policyToUpdate == null)
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
        public bool UpdateDate(Policy policyToUpdate, DateTime newDueDate)
        {
            if (policyToUpdate == null)
                return false;

            try
            {
                Delete(policyToUpdate);
                pcx.Policies.Add(new Policy
                {
                    PolicyNumber = policyToUpdate.PolicyNumber,
                    DueDate = newDueDate,
                    MonthlyPayment = policyToUpdate.MonthlyPayment,
                    Type = policyToUpdate.Type,
                    ClientId = policyToUpdate.ClientId
                });
                pcx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
