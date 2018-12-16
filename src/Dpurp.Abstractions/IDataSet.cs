using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dpurp
{
    public interface IDataSet<TItem> : IEnumerable<TItem>, IEnumerable where TItem : class
    {
        ObservableCollection<TItem> Local { get; }
        TItem Add(TItem entity);
        TItem Attach(TItem entity);
        TItem Create();
        TDerivedItem Create<TDerivedItem>() where TDerivedItem : class, TItem;
        TItem Find(params object[] keyValues);
        TItem Remove(TItem entity);
    }
}
