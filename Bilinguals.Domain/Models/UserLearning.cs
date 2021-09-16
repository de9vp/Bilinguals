using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class UserLearning
    {
        public int Id { get; set; }
        public string LearningUrl { get; set; }
        public string LearningData { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
