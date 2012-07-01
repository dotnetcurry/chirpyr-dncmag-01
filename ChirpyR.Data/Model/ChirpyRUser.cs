using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChirpyR.Data.Model
{
    public class ChirpyRUser : 
        IDataEntity<Data.Model.ChirpyRUser, 
                    Domain.Model.ChirpyRUser>
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<ChirpyRRelation> Followers 
                    { get; set; }
        public List<ChirpyRRelation> Following 
                    { get; set; }
        public Domain.Model.ChirpyRUser ToDomainEntity()
        {
            return new Domain.Model.ChirpyRUser
            {
                UserId = this.UserId,Id = this.Id,
                FullName = this.FullName,
                Email = this.Email
            };      
        }
        public ChirpyRUser LoadFromDomainEntity(
            Domain.Model.ChirpyRUser input)
        {
            Id = input.Id;
            UserId = input.UserId;
            FullName = input.FullName;
            Email = input.Email;
            return this;
        }
    }
}
