using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChirpyR.Data.Model
{
    public class ChirpTag : 
        IDataEntity<Data.Model.ChirpTag, 
            Domain.Model.ChirpTag>
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public Domain.Model.ChirpTag ToDomainEntity()
        {
            Domain.Model.ChirpTag domain = 
                new Domain.Model.ChirpTag
            {
                Id = this.Id,
                Tag = this.Tag
            };
            return domain;
        }
        public ChirpTag LoadFromDomainEntity
            (Domain.Model.ChirpTag input)
        {
            this.Id = input.Id;
            this.Tag = input.Tag;
            return this;
        }
    }
}
