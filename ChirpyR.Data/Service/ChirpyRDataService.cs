using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChirpyR.Domain.Repository;
using ChirpyR.Domain.Model;

namespace ChirpyR.Data.Service
{
    public class ChirpyRDataService
    {
        IChirpyRRepository _repository;
        public ChirpyRDataService(IChirpyRRepository repository)
        {
            _repository = repository;
        }

        public IList<Domain.Model.Chirp> GetAllChirps()
        {
            return _repository.GetLatestChirps();
        }

        public long AddChirp(Domain.Model.Chirp newChirp)
        {
            return _repository.AddChirp(newChirp);
        }

        public long FollowUser(string currentUserId, string followingUserId)
        {

            return _repository.FollowChirpR(_repository.GetUserById(currentUserId), _repository.GetUserById(followingUserId));
        }

        public long RegisterUser(Domain.Model.ChirpyRUser chirpUser)
        {
            Domain.Model.ChirpyRUser user = _repository.GetUserById(chirpUser.UserId);
            if (user == null)
            {
                return _repository.RegisterUser(chirpUser);
            }
            else
            {
                throw new ApplicationException("User Id - " + chirpUser.UserId + " Already Exists");
            }
        }
    }
}
