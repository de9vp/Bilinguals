using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class UserSentenceService : IUserSentenceService
    {
        private readonly IRepository<UserSentence> _userSentenceRepo;
        private readonly ISentenceService _sentenceService;

        public UserSentenceService(IRepository<UserSentence> userSentenceRepo, ISentenceService sentenceService = null)
        {
            _userSentenceRepo = userSentenceRepo;
            _sentenceService = sentenceService;
        }

        public UserSentence AddOrUpdateUserSentence(int sentenceId, int groupId, string userId)
        {
            // case 1: if sentence saved, update name group userSentence
            // case 2: if sentence not save, add new
            // bind for only 1 sentence in 1 group
            var userSentence = _userSentenceRepo.Table.FirstOrDefault(x => x.SentenceId == sentenceId && x.UserId == userId);

            if (userSentence != null)
            {
                userSentence.Id = userSentence.Id;
                userSentence.SentenceId = sentenceId;
                userSentence.UserDialogId = userSentence.UserDialogId;
                userSentence.UserId = userId;
                userSentence.DateCreated = userSentence.DateCreated;
                userSentence.GroupId = groupId; //update

                _userSentenceRepo.Update(userSentence);

                var uso = _userSentenceRepo.Table.Include(icl => icl.Group).FirstOrDefault(x => x.Id == userSentence.Id);
                return uso;
            }

            userSentence = new UserSentence
            {
                SentenceId = sentenceId,
                UserId = userId,
                GroupId = groupId
            };
            _userSentenceRepo.Insert(userSentence);

            var us = _userSentenceRepo.Table.Include(icl => icl.Group).FirstOrDefault(x => x.Id == userSentence.Id);
            return us;
        }

        public void Remove(int userSentenceId)
        {
            var userSentence = _userSentenceRepo.Table.FirstOrDefault(x => x.Id == userSentenceId);
            _userSentenceRepo.Delete(userSentence);
        }

        public IPagedList<Sentence> GetUserSentences(string userId, int pageIndex, int pageSize, string sortOrder)
        {
            var userSentences = _userSentenceRepo.Table.Where(x => x.UserId == userId).Include(x => x.Sentence).Select(x => x);

            return null;
        }
    }
}
