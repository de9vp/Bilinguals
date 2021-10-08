using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class SentenceService : ISentenceService
    {
        private readonly IRepository<Sentence> _sentenceRepo;
        private readonly IRepository<UserSentence> _userSentenceRepo;

        public SentenceService(IRepository<Sentence> sentenceRepo, IRepository<UserSentence> userSentenceRepo)
        {
            _sentenceRepo = sentenceRepo;
            _userSentenceRepo = userSentenceRepo;
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

        public IPagedList<Sentence> GetSentenceList(int pageIndex, int pageSize, string searchText, string sortOrder)
        {
            // Converts every character to lowercase, if null return empty string ( ?. return Null | ?? if Null else return "" )
            var search = searchText?.ToLower() ?? "";

            //Strip non alpha numeric charactors out
            search = Regex.Replace(search, @"[^\w\.@\- ]", "");    // Apply to search by VN language

            // Split word
            var splitSearch = search.Split(' ').ToList();

            var query = _sentenceRepo.Table.Where(x => true);

            foreach (var term in splitSearch)
            {
                query = query.Where(x => x.ViText.ToLower().Contains(term) || x.EnText.ToLower().Contains(term) || x.Dialog.Name.ToLower().Contains(term));
            }

            switch (sortOrder)
            {
                case "DateCreate": //sorted by datecreate
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
                default:
                    query = query.OrderBy(x => x.Id);
                    break;
            }

            return query.ToPagedList(pageIndex, pageSize);
        }

        public IPagedList<Sentence> GetSentenceHome(int pageIndex, int pageSize, string searchText, string sortOrder, string userId)
        {
            var search = searchText?.ToLower() ?? "";

            search = Regex.Replace(search, @"[^\w\.@\- ]", "");  

            var splitSearch = search.Split(' ').ToList();

            var query = _sentenceRepo.Table.Where(x => !String.IsNullOrEmpty(searchText));  // value Null when search text empty

            foreach (var term in splitSearch)
            {
                query = query.Where(x => x.ViText.ToLower().Contains(term) || x.EnText.ToLower().Contains(term));
            }

            var q = from s in query.ToList()
                    from us in _userSentenceRepo.Table.Where(x => x.UserId == userId && x.SentenceId == s.Id).DefaultIfEmpty().Include(icl => icl.Group)
                    select new Sentence
                    {
                        Id = s.Id,
                        EnText = s.EnText,
                        ViText = s.ViText,
                        DateCreated = s.DateCreated,
                        DateModified = s.DateModified,
                        SortOrder = s.SortOrder,
                        DialogId = s.DialogId,
                        UserSentenceId = us == null ? (int?)null : us.Id, //purpose
                        GroupName = us == null ? (string)null : us.Group.Name,
                        DialogName = s.Dialog.Name,
                    };

            switch (sortOrder)
            {
                //case "DateCreate": //sorted by datecreate or optionally
                //    query = query.OrderByDescending(x => x.DateCreated);
                //    break;
                default:
                    q = q.OrderBy(x => x.Id);
                    break;
            }

            return q.ToPagedList(pageIndex, pageSize);
        }
    }
}
