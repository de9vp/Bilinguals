using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class UserDialog : BaseEntity
    {
        public int Reviews { get; set; }
        public string LearningProgress { get; set; }
        public bool Featured { get; set; }
        public bool Mastered { get; set; }
        public bool IsLearning { get; set; }
        public int? DialogId { get; set; }
        public string UserId { get; set; }

        public Dialog Dialog { get; set; }
        public ApplicationUser User { get; set; }

        public List<UserSentence> UserSentences { get; set; } = new List<UserSentence>();
    }
}
