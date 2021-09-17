using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
