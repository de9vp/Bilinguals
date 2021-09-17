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

        public void Delete(Sentence sentence)
        {
            _sentenceRepo.Delete(sentence);
        }
    }
}
