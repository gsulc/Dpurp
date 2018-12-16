using System.Collections.Generic;

namespace Dpurp
{
    public interface IDataContext
    {
        void SaveChanges();
        ISet<TItem> Set<TItem>();
    }
}
