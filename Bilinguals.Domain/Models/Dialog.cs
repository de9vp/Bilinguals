using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Models
{
    public class Dialog : BaseEntity
    {
        public Dialog()
        {
            Sentences = new List<Sentence>();
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IList<Sentence> Sentences { get; set; }  //navigation properties
        
        [NotMapped]
        public int? UserDialogId { get; set; }
        
        
        
    
    
    }
}
