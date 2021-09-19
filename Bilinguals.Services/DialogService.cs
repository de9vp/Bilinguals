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
    public class DialogService : IDialogService
    {
        private readonly IRepository<Dialog> _dialogRepo;
        public DialogService(IRepository<Dialog> dialogRepo)
        {
            _dialogRepo = dialogRepo;
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

        public IPagedList<Dialog> GetDialogList(int pageIndex, int pageSize, string searchText, string sortOrder)
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

            switch (sortOrder)
            {
                //case "DateCreate": //sorted by datecreate
                //    query = query.OrderByDescending(x => x.DateCreated);
                //    break;
                default:
                    query = query.OrderBy(x => x.Id);
                    break;
            }

            return query.ToPagedList(pageIndex, pageSize);
        }

        public void FromExcel(List<string> allLines)
        {
            Dialog dialog = null;
            int index = 0;

            foreach (var row in allLines)
            {
                var pairOfTexts = row.Split('\t');
                if (pairOfTexts.Length == 1 || string.IsNullOrWhiteSpace(pairOfTexts[1]))
                {
                    // create new topic
                    dialog = new Dialog
                    {
                        Name = pairOfTexts[0],
                    };
                    _dialogRepo.Insert(dialog);
                    index = 1;
                    continue;
                }

                if (dialog == null)
                    continue;

                dialog.Sentences.Add(new Sentence
                {
                    EnText = pairOfTexts[0],
                    ViText = pairOfTexts[1],
                    SortOrder = index,
                });

                _dialogRepo.Update(dialog);

                index++;
            }
        }
    }
}
