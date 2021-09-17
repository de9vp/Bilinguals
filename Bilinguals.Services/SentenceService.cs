using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class SentenceService : ISentenceService
    {
        private readonly IRepository<Sentence> _sentenceRepo;
        public SentenceService(IRepository<Sentence> sentenceRepo)
        {
            _sentenceRepo = sentenceRepo;
        }

        public void Add(Sentence sentence)
        {
            _sentenceRepo.Insert(sentence);
        }

        public void Delete(int id)
        {
            var sentence = _sentenceRepo.Table.FirstOrDefault(x => x.Id == id);
            _sentenceRepo.Delete(sentence);
        }

        public void Edit(Sentence sentence)
        {
            _sentenceRepo.Update(sentence);
        }
    }
}
