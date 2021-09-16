using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class Dialog : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Sentence> Sentences { get; set; }
    }
}
