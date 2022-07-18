namespace MiniORM
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    internal class ChangeTracker<TEntity>
        where TEntity : class, new()
    {
        private readonly List<TEntity> allEntitites;
        private readonly List<TEntity> addedEntities;
        private readonly List<TEntity> removedEntities;

        private ChangeTracker()
        {
            this.addedEntities = new List<TEntity>();
            this.removedEntities = new List<TEntity>();
        }

        public ChangeTracker(IEnumerable<TEntity> entities)
            : this()
        {
            this.allEntitites = CloneEntities(entities);
        }

        public IReadOnlyCollection<TEntity> AllEntitites => this.allEntitites;

        public IReadOnlyCollection<TEntity> AddedEntities => this.addedEntities;

        public IReadOnlyCollection<TEntity> RemovedEntities => removedEntities;

        public void Add(TEntity item) => this.addedEntities.Add(item);

        public void Remove(TEntity item) => this.removedEntities.Add(item);

        public IEnumerable<TEntity> GetModifiedEntities(DbSet<TEntity> dbSet)
        {
            var modifiedEntities = new List<TEntity>();

            var primaryKeys = typeof(TEntity)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (TEntity proxyEntity in this.AllEntitites)
            {
                var primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                TEntity entity =  dbSet.Entities
                    .Single(e => GetPrimaryKeyValues(primaryKeys, e)
                                .SequenceEqual(primaryKeyValues));

                bool isModified = IsModified(proxyEntity, entity);
                if (isModified)
                {
                    modifiedEntities.Add(entity);
                }
            }

            return modifiedEntities;
        }

        private bool IsModified(object proxyEntity, object entity)
        {
            IEnumerable<PropertyInfo> monitoredProperties = typeof(TEntity)
                .GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType));

            PropertyInfo[] modifiedProperties = monitoredProperties
                .Where(pi => !Equals(pi.GetValue(proxyEntity), pi.GetValue(entity)))
                .ToArray();

            return modifiedProperties.Any();
        }

        private IEnumerable<object> GetPrimaryKeyValues(PropertyInfo[] primaryKeys, TEntity entity)
            => primaryKeys.Select(pk => pk.GetValue(entity));


        private static List<TEntity> CloneEntities(IEnumerable<TEntity> entities)
        {
            List<TEntity> clonedEntities = new List<TEntity>();

            PropertyInfo[] propertiesToClone = typeof(TEntity)
                .GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            foreach (var entity in entities)
            {
                var clonedEntity = Activator.CreateInstance<TEntity>();

                foreach (var property in propertiesToClone)
                {
                    var value = property.GetValue(entity);
                    property.SetValue(clonedEntity, value);
                }

                clonedEntities.Add(clonedEntity);
            }

            return clonedEntities;
        }
    }
}