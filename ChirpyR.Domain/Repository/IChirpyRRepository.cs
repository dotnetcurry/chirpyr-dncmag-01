using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChirpyR.Domain.Model;

namespace ChirpyR.Domain.Repository
{
    public interface IChirpyRRepository
    {
        IList<Chirp> GetLatestChirps();
        IList<Chirp> GetLatestChirpsFor(string user);
        IList<ChirpyRUser> GetFollowers(ChirpyRUser user);
        IList<ChirpyRUser> GetFollowing(ChirpyRUser user);
        IList<ChirpTag> GetChirpTags(int topCount);
        long AddChirp(Chirp chirp);
        long RegisterUser(ChirpyRUser newUser);
        long UpdateUserAccount(ChirpyRUser updatedUser);
        long UnRegisterUser(ChirpyRUser removeUser);
        long FollowChirpR(ChirpyRUser currentUser,
            ChirpyRUser followeUser);
        long UnfollowChirpR(ChirpyRUser currentUser,
            ChirpyRUser unfollowUser );
        ChirpyRUser GetUserById(string userId);        
        /// <summary>
        /// Testing API only. Service method should not be 
        /// implemented
        /// </summary>
        /// <param name="deletedUser"></param>
        /// <returns></returns>
        long DeleteUserAccount(ChirpyRUser deletedUser);
    }
}
