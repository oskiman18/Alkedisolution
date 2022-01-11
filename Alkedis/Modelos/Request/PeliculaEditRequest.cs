using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkedis.Modelos.Request
{
    public class PeliculaEditRequest
    {
        public string titulo { get; set; }
        public string imagen { get; set; }
        public DateTime fCreacion { get; set; }
        public int generoid { get; set; }
        public int calificacion { get; set; }
        
    }
}
