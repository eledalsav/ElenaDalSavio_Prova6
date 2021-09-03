using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.Core.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Column(TypeName = "nchar(16)")]
        public string Code { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string LastName { get; set; }

        public List<Policy> Policies { get; set; } = new List<Policy>();

    }
}
