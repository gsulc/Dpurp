using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Dpurp.EntityFramework
{
    public class EntityRepository<TItem> : IRepository<TItem> where TItem : class
    {
        private readonly EntityDataContext _dataContext;
        private DbSet<TItem> _entities;

        public EntityRepository(EntityDataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException("dataContext");
            _dataContext.RegisterEntityType(typeof(TItem));
        }

        protected virtual DbSet<TItem> Items
        {
            get
            {
                return _entities ?? (_entities = _dataContext.Set<TItem>());
            }
        }

        public IQueryable<TItem> Entities { get { return Items; } }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public IEnumerable<TItem> Find(Func<TItem, bool> predicate)
        {
            return Items.Where(predicate);
        }

        public TItem Get(int id)
        {
            return Items.Find(id);
        }

        public IEnumerable<TItem> GetAll()
        {
            return Items.AsEnumerable();
        }

        public void Add(TItem item)
        {
            Items.Add(item);
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            Items.AddRange(items);
        }

        public void Remove(TItem item)
        {
            Items.Remove(item);
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            Items.RemoveRange(items);
        }
    }
}
