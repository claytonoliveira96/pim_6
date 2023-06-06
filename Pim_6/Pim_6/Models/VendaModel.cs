using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pim_6.Models
{
    internal class VendaModel
    {
        public int codigo  { get; set; }
        public int codigocliente { get; set; }
        public DateTime data { get; set; }
        public string status { get; set; }
    }
}
