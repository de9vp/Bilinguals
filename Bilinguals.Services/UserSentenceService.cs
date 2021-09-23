using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class UserSentenceService : IUserSentenceService
    {
        private readonly IRepository<UserSentence> _userSentenceRepo;

        public UserSentenceService(IRepository<UserSentence> userSentenceRepo)
        {
            _userSentenceRepo = userSentenceRepo;
        }

        public UserSentence AddOrUpdateUserSentence(int sentenceId, string userId, bool featured)
        {
            var userSentence = _userSentenceRepo.Table.FirstOrDefault(x => x.SentenceId == sentenceId && x.UserId == userId);
            if (userSentence == null)
            {
                //add
                userSentence = new UserSentence
                {
                    SentenceId = sentenceId,
                    UserId = userId,
                    Featured = featured,
                    DateFeatured = DateTime.Now
                };
                _userSentenceRepo.Insert(userSentence);
            }
            else
            {
                //update
                userSentence.Featured = false;
                userSentence.DateFeatured = DateTime.Now;
                _userSentenceRepo.Update(userSentence);
            }
            return userSentence;
        }

        public void Remove(int userSentenceId)
        {
            var userSentence = _userSentenceRepo.Table.FirstOrDefault(x => x.Id == userSentenceId);
            _userSentenceRepo.Delete(userSentence);
        }
    }
}
