using Olympics.Core.Repository.ChangeTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olympics.Core.Repository
{
    public abstract class Repository<T>:IDisposable
    {
        protected Repository(Func<T, object[]> keySelector, bool ignoreChangeTracking=false)
        {
            KeySelector = keySelector;
            IgnoreChangeTracking = ignoreChangeTracking;

            UnsavedObjects = new List<ChangeTracker<T>>();
            PendingOperations = new List<IOperation>(); 
        }


        internal bool IgnoreChangeTracking { get; set; }

        protected Func<T,object[]> KeySelector { get; private set; }
        private List<ChangeTracker<T>> UnsavedObjects { get; set; }

        private List<IOperation> PendingOperations { get; set; }
         

        /// <summary>
        /// insert
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract Insert<T> CreateInsert(IEnumerable<object> keys, T value);

        protected virtual BatchInert<T> CreateBatchInsert(IEnumerable<KeyValuePair<IEnumerable<object>,T>> keyValuePairs)
        {
            return new DefaultBatchInsert<T>(keyValuePairs, CreateInsert);
        }

        protected abstract Remove CreateRemove(IEnumerable<object> keys);

        protected abstract Modifidy<T> CreateModify(IEnumerable<object> keys, T value, Action<T> modifier);

        public abstract bool ExistsByKey(params Object[] keys);

        internal void AddChangeTracker(T obj) {
            if(!IgnoreChangeTracking && !ChangeTracker<T>.CanTrackChanges)
            {
                throw new RepositoryException(String.Format("Cannot enable change tracking for type {0}. Please ensure that it has a public default (parameterless) constructor.", typeof(T)));
            }

            UnsavedObjects.Add(new ChangeTracker<T>(obj, KeySelector));
        }

        private void AddPendingOperation(IOperation operation)
        {
            PendingOperations.Add(operation);
        }

        public void Insert(T value)
        {
            AddPendingOperation(CreateInsert(KeySelector(value), value));
        }

        public void Insert(IEnumerable<T> values)
        {
            AddPendingOperation(CreateBatchInsert(values.Select(_ => new KeyValuePair<IEnumerable<object>, T>(KeySelector(_), _))));
        }
         
        public void RemoveAll(IEnumerable<T> objects = null)
        {
            if (null==objects)
            {
                PendingOperations.RemoveAll(_ => _ is Insert<T> || _ is BatchInert<T>);
            }

            objects = objects ?? Items;
            RemoveAllByKey(objects.Select(_ => KeySelector(_)));
        }

        public void RemoveByKey(params object[] keys)
        {
            AddPendingOperation(CreateRemove(keys));
        }

        public void RemoveAllByKey(IEnumerable<object[]> keys)
        {
            foreach (var key in keys)
                RemoveByKey(key);
        }

        public void Remove(T obj)
        {
            RemoveByKey(KeySelector(obj));
        }


        public bool Exists(T obj)
        {
            return ExistsByKey(KeySelector(obj));
        }

        public void Update<TValue>(TValue modifier,params object[] keys)
        {
            var existingItem = FindWrapper(keys, false);
            if (existingItem == null)
                return;

            AddPendingOperation(CreateModify(keys, existingItem.Object, _ => AutoMapper.Mapper.Map(modifier, _)));
        }

        public void Update<TValue,TProperty>(TValue modifier,Func<T,TProperty> getter,params object[] keys)
        {
            var existingItem = FindWrapper(keys, false);
            if (existingItem == null)
                return;

            AddPendingOperation(CreateModify(keys, existingItem.Object, _ => AutoMapper.Mapper.Map(modifier, getter(_))));
        }

        public ObjectContext<T> Find(params object[] keys)
        {
            return FindWrapper(keys, true);
        }

        private ObjectContext<T> FindWrapper(object[] keys,bool trackChanges)
        {
            ObjectContext<T> obj = null;
            try
            {
                obj = FindImpl(keys);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Could not access repository.", e);
            }
            if (trackChanges && obj != null)
                AddChangeTracker(obj.Object);

            return obj;
        }

        protected abstract ObjectContext<T> FindImpl(object[] keys);

        public void SaveChanges()
        {
            try
            {
                ApplyChanges();
            }
            catch (Exception e)
            {

                throw new RepositoryException("Could not commit changes to repository.", e);
            }

            AfterApplyChanges();
        }

        private void ApplyChanges()
        {
            foreach(var obj in UnsavedObjects.Where(x => x.HasChanges))
            {
                Update(obj.CurrentValue, KeySelector(obj.CurrentValue));
            }

            foreach (var change in PendingOperations)
                change.Apply();

            UnsavedObjects.Clear();
            PendingOperations.Clear();
        }

        protected virtual void AfterApplyChanges()
        {

        }

        public abstract EnumerableObjectContext<T> Items { get; }



        public abstract void Dispose();

        internal void SetKeySelector(Func<T,object[]> keySelector)
        {
            KeySelector = keySelector;
            OnKeySelectorChanged(keySelector);
        }

        public virtual void OnKeySelectorChanged(Func<T,object[]> newKeySelector)
        {

        }

      
    }
}
