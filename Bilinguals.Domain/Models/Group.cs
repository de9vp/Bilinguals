using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public IList<UserSentence> UserSentences { get; set; }

        [NotMapped]
        public int? UserSentenceId { get; set; }

       

    }
}
