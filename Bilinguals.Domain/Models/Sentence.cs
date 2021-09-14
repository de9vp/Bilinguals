using System;
using System.Collections.Generic;
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
        public Dialog Dialog { get; set; }
        public int SortOrder { get; set; }
    }
}
