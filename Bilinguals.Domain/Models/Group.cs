using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string StrSentenceId { get; set; }
        public string Description { get; set; }
    }
}
