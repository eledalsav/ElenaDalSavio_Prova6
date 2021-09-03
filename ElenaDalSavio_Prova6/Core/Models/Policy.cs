using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.Core.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public int PolicyNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public EnumType Type { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
    public enum EnumType
    {
        RCAuto,
        Furto,
        Vita
    }
}
