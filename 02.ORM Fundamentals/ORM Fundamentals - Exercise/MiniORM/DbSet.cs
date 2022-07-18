namespace MiniORM
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class DbSet<TEntity> : ICollection<TEntity>
        where TEntity : class, new()
    {
        internal DbSet(IEnumerable<TEntity> entities)
        {
            this.Entities = entities.ToList();
            this.ChangeTracker = new ChangeTracker<TEntity>(entities);
        }
        internal ChangeTracker<TEntity> ChangeTracker { get; set; }

        internal IList<TEntity> Entities { get; set; }

        public int Count => this.Entities.Count;

        public bool IsReadOnly => this.Entities.IsReadOnly;

        public void Add(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentException(ExceptionMessages.argExMessage);
            }

            this.Entities.Add(item);
            this.ChangeTracker.Add(item);
        }

        public bool Remove(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentException(ExceptionMessages.argExMessage);
            }

            if (this.Entities.Remove(item))
            {
                this.ChangeTracker.Remove(item);
                return true;
            }

            return false;
        }

        public void Clear()
            => this.Entities.Clear();

        public bool Contains(TEntity item)
            => this.Entities.Contains(item);

        public void CopyTo(TEntity[] array, int arrayIndex)
            => this.Entities.CopyTo(array, arrayIndex);

        public IEnumerator<TEntity> GetEnumerator()
            => this.Entities.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}