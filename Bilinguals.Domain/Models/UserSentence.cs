using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class UserSentence : BaseEntity
    {
        public int SentenceId { get; set; }
        public int? UserDialogId { get; set; }
        public int Reviews { get; set; }
        public int DifficultyLevel { get; set; }
        public bool Featured { get; set; }
        public bool Mastered { get; set; }
        public DateTime? DateFeatured { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }

        public UserDialog UserDialog { get; set; }
        public Sentence Sentence { get; set; }
        public ApplicationUser User { get; set; }
    }
}
