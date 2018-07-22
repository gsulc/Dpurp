using System.Collections.Generic;

namespace Dpurp.Abstractions
{
    public interface IContext
    {
        void SaveChanges<TItem>(IList<TItem> items);
        IList<TItem> Set<TItem>();
    }
}
