using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChirpyR.Data.Model
{
    public class Chirp : IDataEntity<Data.Model.Chirp, Domain.Model.Chirp>
    {
        public Chirp()
        {
            Replies = new List<Chirp>();
            Tags = new List<ChirpTag>();
        }
        public long Id { get; set; }
        public string Text { get; set; }
        public ChirpyRUser ChirpBy { get; set; }
        public DateTime ChirpTime { get; set; }
        public IList<Chirp> Replies { get; set; }
        public IList<ChirpTag> Tags { get; set; }
        public Chirp ChirpReplyTo { get; set; }

        public Domain.Model.Chirp ToDomainEntity()
        {
            Domain.Model.Chirp chirp = new Domain.Model.Chirp
            {
                Id = this.Id,
                Text = this.Text
            };
            return chirp;
        }

        public Data.Model.Chirp LoadFromDomainEntity(Domain.Model.Chirp input)
        {
            Text = input.Text;
            ChirpTime = input.ChirpTime;
            ChirpReplyTo = new Chirp().LoadFromDomainEntity(input.InReplyTo);
            ChirpBy = new ChirpyRUser().LoadFromDomainEntity(input.ChirpBy);
            return this;
        }
    }
}
