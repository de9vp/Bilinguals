using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class IrregularVerb : BaseEntity
    {
        public string Infinitive { get; set; }
        public string SimplePast { get; set; }
        public string PastParticiple { get; set; }
        public string Mean { get; set; }
    }
}
