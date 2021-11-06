using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int DialogId { get; set; }

        [NotMapped]
        public string UserFullname { get; set; }
        [NotMapped]
        public string TimeConvert { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
