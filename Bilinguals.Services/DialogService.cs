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
    public class DialogService : IDialogService
    {
        private readonly IRepository<Dialog> _dialogRepo;
        private readonly IRepository<UserDialog> _userDialogRepo;
        private readonly IRepository<UserSentence> _userSentenceRepo;

        public DialogService(IRepository<Dialog> dialogRepo, IRepository<UserDialog> userDialogRepo, IRepository<UserSentence> userSentenceRepo)
        {
            _dialogRepo = dialogRepo;
            _userDialogRepo = userDialogRepo;
            _userSentenceRepo = userSentenceRepo;
        }


        public void Add(Dialog dialog)
        {
            _dialogRepo.Insert(dialog);
        }
        public void Delete(int id)
        {
            var dialog = _dialogRepo.Table.FirstOrDefault(x => x.Id == id);
            
            _dialogRepo.Delete(dialog);
        }
        public void Edit(Dialog dialog)
        {
            _dialogRepo.Update(dialog);
        }

        public Dialog GetById(int id)
        {
            var dialog = _dialogRepo.GetById(id);
            return dialog;
        }

        public List<Dialog> GetAll()
        {
            return _dialogRepo.Table.ToList();
        }

        public IPagedList<Dialog> GetDialogList(int pageIndex, int pageSize, string searchText, string sortOrder, string userId)
        {
            // Converts every character to lowercase, if null return empty string ( ?. return Null | ?? if Null else return "" )
            var search = searchText?.ToLower() ?? "";

            //Strip non alpha numeric charactors out
            search = Regex.Replace(search, @"[^\w\.@\- ]", "");    // Apply to search by VN language

            // Split word
            var splitSearch = search.Split(' ').ToList();

            var query = _dialogRepo.Table.Where(x => true);

            foreach (var term in splitSearch)
            {
                query = query.Where(x => x.Name.ToLower().Contains(term));
            }

            var q = from d in query.ToList()
                    from ud in _userDialogRepo.Table.Where(x => x.UserId == userId && x.DialogId == d.Id).DefaultIfEmpty()
                    select new Dialog
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        Subcribers = _userDialogRepo.Table.Where(x => x.DialogId == d.Id).Count(), // subcribe
                        DateModified = d.DateModified,
                        UserDialogId = ud == null ? (int?)null : ud.Id, //purpose
                    };

            switch (sortOrder)
            {
                //case "DateCreate": //sorted by datecreate
                //    query = query.OrderByDescending(x => x.DateCreated);
                //    break;
                default:
                    q = q.OrderBy(x => x.Id);
                    break;
            }

            return q.ToPagedList(pageIndex, pageSize);
        }

        public void FromTextFile(string allTexts)
        {
            var groups = allTexts.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);  //xuong dong 2 lan ( ki tu xuong dong tren window )
            Dialog dialog = null;
            var index = 0;

            foreach (var g in groups)
            {
                if (g.Contains(" | "))
                {
                    var pair1 = g.Split(new string[] { " | " }, StringSplitOptions.None); 
                    // create new dialog
                    dialog = new Dialog
                    {
                        Name = pair1[0],
                        Description = pair1.Length == 2 ? pair1[1] : null,
                    };
                    _dialogRepo.Insert(dialog);
                    index = 1;
                    continue;
                }

                if (dialog == null) // exit function
                    return; 

                var pairOfTexts = g.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (pairOfTexts.Length == 2)
                {
                    dialog.Sentences.Add(new Sentence
                    {
                        EnText = pairOfTexts[0].Trim(),
                        ViText = pairOfTexts[1].Trim(),
                        SortOrder = index,
                    });

                    _dialogRepo.Update(dialog);

                    index++;
                }
            }
        }

        public Dialog GetDialogDetailAndSentences(int dialogId, string userId)
        {
            var dialog = _dialogRepo.GetById(dialogId);
            if (dialog == null)
                return null;

            //https://stackoverflow.com/questions/3404975/left-outer-join-in-linq
            var dialogSentenceDetail = from s in dialog.Sentences
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
                                           GroupName = us == null ? (string)null : us.Group.Name //purpose
                                       };

            dialog.Sentences = dialogSentenceDetail.ToList();

            return dialog;
        }
    }
}
