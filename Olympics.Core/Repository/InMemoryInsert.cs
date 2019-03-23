using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olympics.Core.Repository
{
    internal class InMemoryInsert<TValue> : Insert<TValue>
    {
        public InMemoryInsert(IEnumerable<string> keys, TValue value, IDictionary<string, TValue> dictionary) : base(keys, value)
        {
            Dictionary = dictionary;
        }

        private IDictionary<string, TValue> Dictionary { get; set; }



        public override void Apply()
        {
            Dictionary[Keys.First() as string] = Value;
        }
    }

    internal class InMemoryRemove<TValue> : Remove
    {
        public InMemoryRemove(IEnumerable<string> keys, IDictionary<string, TValue> dictionary) : base(keys)
        {
            Dictionary = dictionary;
        }

        private IDictionary<string, TValue> Dictionary { get; set; }

        public override void Apply()
        {
            if (Dictionary.ContainsKey(Keys.First() as string))
                Dictionary.Remove(Keys.First() as string);
        }
    }

    internal class InMemoryModify<TValue> : Modifidy<TValue>
    {
        public InMemoryModify(IEnumerable<object> keys, TValue value, Action<TValue> modifier)
    : base(keys, value, modifier)
        { }
        //===============================================================
        public override void Apply()
        {
            Modifier(Value);
        }
    }

    public class InMemoryRepository<T> : Repository<T> where T : class
    {
        private ConcurrentDictionary<string, T> mData = new ConcurrentDictionary<string, T>();

        public InMemoryRepository(Func<T, Object> keySelector)
        : base(x => new[] { keySelector(x) })
        { }

        public override EnumerableObjectContext<T> Items
        {
            get
            {
                return new EnumerableObjectContext<T>(mData.Values.AsQueryable(), this);
            }
        }

        public override void Dispose()
        {
            //throw new NotImplementedException();
        }

        protected override Insert<T> CreateInsert(IEnumerable<object> keys, T value)
        {
            return new InMemoryInsert<T>(keys.Select(_ => _.ToString()), value, mData);
        }

        protected override Modifidy<T> CreateModify(IEnumerable<object> keys, T value, Action<T> modifier)
        {
            return new InMemoryModify<T>(keys, value, modifier);
        }

        protected override Remove CreateRemove(IEnumerable<object> keys)
        {
            return new InMemoryRemove<T>(keys.Select(_ => _.ToString()), mData);
        }

        public override bool ExistsByKey(params object[] keys)
        {
            if (keys.Length > 1)
                throw new NotSupportedException("InMemoryRepository only supports objects with a single key.");
            return mData.ContainsKey(keys.First().ToString());
        }


        protected override ObjectContext<T> FindImpl(object[] keys)
        {
            var key = keys.First().ToString();
            var obj = default(T);
            if (!mData.TryGetValue(key, out obj))
                return null;

            return new ObjectContext<T>(obj);
        }
    }

    public class InMemoryRepository<TValue, TKey> : Repository<TValue, TKey> where TValue : class
    {
        public InMemoryRepository(Func<TValue, TKey> keySelector) : base(new InMemoryRepository<TValue>(_ => keySelector(_)))
        {

        }
    }
}
