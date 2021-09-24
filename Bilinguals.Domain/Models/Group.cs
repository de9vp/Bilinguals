using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class Group
    {
        public Group()
        {
            DateCreated = DateTime.Now;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserId { get; set; }
        public string StrDialogId { get; set; }
        public string Description { get; set; }
    }
}
