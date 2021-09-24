using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class UserWord : BaseEntity
    {
        public string UserId { get; set; }
        public string EnWord { get; set; }
        public string Phonetically { get; set; } //phien am
        public string ViWord { get; set; }
        public string Description { get; set; }

        public ApplicationUser User { get; set; }
    }
}
