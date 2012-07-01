using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChirpyR.Data.Model
{
    public interface IDataEntity<S,T>
    {
        T ToDomainEntity();
        S LoadFromDomainEntity(T input);
    }
}
