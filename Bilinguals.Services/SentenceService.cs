using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public Sentence GetById(int id)
        {
            var sentence = _sentenceRepo.GetById(id);
            return sentence;
        }

        public IPagedList<Sentence> GetAll(int pageIndex, int pageSize, string searchText, string sortOrder)
        {
            // Converts every character to lowercase, if null return empty string ( ?. return Null | ?? if Null else return "" )
            var search = searchText?.ToLower() ?? "";

            //Strip non alpha numeric charactors out
            search = Regex.Replace(search, "[^A-Za-z0-9 -]", "");    //https://stackoverflow.com/questions/3210393/how-do-i-remove-all-non-alphanumeric-characters-from-a-string-except-dash

            // Split word
            var splitSearch = search.Split(' ').ToList();

            var query = _sentenceRepo.Table.Where(x => true);

            foreach (var term in splitSearch)
            {
                query = query.Where(x => x.ViText.ToLower().Contains(term) || x.EnText.ToLower().Contains(term) || x.Dialog.Name.ToLower().Contains(term));
            }

            switch (sortOrder)
            {
                case "DateCreate":
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
                default:
                    query = query.OrderBy(x => x.Id);
                    break;
            }

            return query.ToPagedList(pageIndex, pageSize);
        }
    }
}
