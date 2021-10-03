using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class UserSentence : BaseEntity
    {
        public int SentenceId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public int? UserDialogId { get; set; }
        
        [Required]
        public string UserId { get; set; }

        public UserDialog UserDialog { get; set; }
        public Sentence Sentence { get; set; }
        public virtual Group Group { get; set; }
        public ApplicationUser User { get; set; }
    }
}
