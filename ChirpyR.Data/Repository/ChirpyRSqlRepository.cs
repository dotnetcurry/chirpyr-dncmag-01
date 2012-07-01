using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChirpyR.Domain.Repository;
using ChirpyR.Data.Context;
using ChirpyR.Data.Model;

namespace ChirpyR.Data.Repository
{
    public class ChirpyRSqlRepository : IChirpyRRepository
    {
        string _schemaName, _connectionName;

        public ChirpyRSqlRepository(string connectionName, string schemaName)
        {
            _schemaName = schemaName;
            _connectionName = connectionName;
        }

        public IList<Domain.Model.Chirp> GetLatestChirps()
        {
            using (ChirpyRDataContext context = 
                new ChirpyRDataContext(_connectionName, _schemaName))
            {
                IEnumerable<Data.Model.Chirp> chirps = 
                    context.Chirps.Include("Replies");
                IList<Domain.Model.Chirp> list = 
                    (from chirp in chirps
                     select chirp.ToDomainEntity()).Take<Domain.Model.Chirp>(20)
                        .ToList<Domain.Model.Chirp>();
                return list;
            }
        }

        public long AddChirp(Domain.Model.Chirp chirp)
        {
            using (ChirpyRDataContext context = 
                new ChirpyRDataContext(_connectionName, _schemaName))
            {
                Data.Model.Chirp replyChirp = 
                    chirp.InReplyTo != null ? 
                    context.Chirps.Find(chirp.InReplyTo.Id) : 
                    null;
                Data.Model.ChirpyRUser chirpUser = 
                    chirp.ChirpBy != null ? 
                    context.ChirpyRUsers.Find(chirp.ChirpBy.Id) : 
                    null;
                Data.Model.Chirp newChirp = new Model.Chirp();
                newChirp.LoadFromDomainEntity(chirp);
                context.Entry<Data.Model.Chirp>(newChirp).State = 
                    System.Data.EntityState.Added;
                context.SaveChanges();
                return newChirp.Id;
            }
        }

        public long RegisterUser(Domain.Model.ChirpyRUser newUser)
        {
            Data.Model.ChirpyRUser user = 
                new Model.ChirpyRUser()
                    .LoadFromDomainEntity(newUser);
            using (ChirpyRDataContext context = 
                new ChirpyRDataContext(_connectionName, 
                    _schemaName))
            {
                context.Entry<ChirpyRUser>(user).State 
                    = System.Data.EntityState.Added;
                context.SaveChanges();
            }
            return user.Id;
        }

        public long FollowChirpR(Domain.Model.ChirpyRUser currentUser, Domain.Model.ChirpyRUser followUser)
        {
            using (ChirpyRDataContext context = new ChirpyRDataContext(_connectionName, _schemaName))
            {
                ChirpyRUser cUser = context.ChirpyRUsers.Single<ChirpyRUser>(c => c.UserId == currentUser.UserId);
                ChirpyRUser fUser = context.ChirpyRUsers.Single<ChirpyRUser>(c => c.UserId == followUser.UserId);
                ChirpyRRelation relation = new ChirpyRRelation { Parent = cUser, Child = fUser };
                context.Entry<ChirpyRRelation>(relation).State = System.Data.EntityState.Added;
                return relation.Id;
            }
        }



        public Domain.Model.ChirpyRUser 
            GetUserById(string userId)
        {
            using (ChirpyRDataContext context = new 
                ChirpyRDataContext(_connectionName, 
                    _schemaName))
            {
                try
                {
                    ChirpyRUser user = context.ChirpyRUsers
                        .Single<ChirpyRUser>
                            (c => c.UserId == userId);
                    if (user != null)
                    {
                        return user.ToDomainEntity();
                    }
                }
                catch (InvalidOperationException ex)
                {
                    return null;
                    // Log the exception here
                }
            }
            return null;
        }


        public IList<Domain.Model.Chirp> GetLatestChirpsFor(string user)
        {
            using (ChirpyRDataContext context =
                new ChirpyRDataContext(_connectionName, _schemaName))
            {
                IEnumerable<Data.Model.Chirp> chirps =
                    context.Chirps.Include("ChirpBy");
                IList<Domain.Model.Chirp> list =
                    (from chirp in chirps
                     where chirp.ChirpBy != null && chirp.ChirpBy.UserId == user
                     select chirp.ToDomainEntity()).Take<Domain.Model.Chirp>(20)
                        .ToList<Domain.Model.Chirp>();
                return list;
            }
        }

        public long UpdateUserAccount(Domain.Model.ChirpyRUser updatedUser)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Model.ChirpyRUser> GetFollowers(Domain.Model.ChirpyRUser user)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Model.ChirpyRUser> GetFollowing(Domain.Model.ChirpyRUser user)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Model.ChirpTag> GetChirpTags(int topCount)
        {
            throw new NotImplementedException();
        }

        public long UnRegisterUser(Domain.Model.ChirpyRUser removeUser)
        {
            throw new NotImplementedException();
        }

        public long DeleteUserAccount(Domain.Model.ChirpyRUser deletedUser)
        {
            throw new NotImplementedException();
        }


        public long UnfollowChirpR(Domain.Model.ChirpyRUser currentUser, Domain.Model.ChirpyRUser unfollowUser)
        {
            throw new NotImplementedException();
        }



    }
}
