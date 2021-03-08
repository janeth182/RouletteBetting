using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteBetting.Models.Entity
{
    public class Client
    {
        public int IdClient { get; set; }
        public string ClientName { get; set; }
        public decimal Credit { get; set; }
    }
}
