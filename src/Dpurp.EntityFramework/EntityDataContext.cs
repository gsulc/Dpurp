using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Dpurp.EntityFramework
{
    public class EntityDataContext : DbContext
    {
        private Queue<Type> _unregisteredEntityTypes = new Queue<Type>();

        public EntityDataContext() : base() { }
        public EntityDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            RegisterNewEntityTypes(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void RegisterNewEntityTypes(DbModelBuilder modelBuilder)
        {
            while (_unregisteredEntityTypes.Count > 0)
            {
                var newType = _unregisteredEntityTypes.Dequeue();
                modelBuilder.RegisterEntityType(newType);
            }
        }

        public void RegisterEntityType(Type type)
        {
            _unregisteredEntityTypes.Enqueue(type);
        }

        public void SaveChanges<TItem>(IList<TItem> items)
        {
            SaveChanges();
        }
    }
}
