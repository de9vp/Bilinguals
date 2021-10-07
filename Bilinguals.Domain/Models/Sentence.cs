using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class Sentence : BaseEntity
    {
        public string EnText { get; set; }
        public string ViText { get; set; }
        public int DialogId { get; set; }
        public virtual Dialog Dialog { get; set; }  //using virtual in here, not include
        public int SortOrder { get; set; }
        public string FavoriteUser { get; set; }

        public IList<UserSentence> UserSentences { get; set; }

        [NotMapped]
        public int? UserSentenceId { get; set; }

        [NotMapped]
        public string GroupName { get; set; }

        [NotMapped]
        public string DialogName { get; set; }
    }
}
