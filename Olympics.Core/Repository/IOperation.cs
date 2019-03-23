using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olympics.Core.Repository
{
    public interface IOperation
    {
        void Apply();
    }

    public abstract class Insert<TValue> : IOperation
    {
        public Insert(IEnumerable<object> keys, TValue value)
        {
            Keys = keys;
            Value = value;
        }

        public IEnumerable<Object> Keys { get; private set; }

        public TValue Value { get; private set; }

        public abstract void Apply();
    }

    public abstract class Remove : IOperation
    {
        public Remove(IEnumerable<object> keys)
        {
            Keys = keys;
        }

        public IEnumerable<Object> Keys { get; private set; }
        public abstract void Apply();
    }

    public abstract class Modifidy<T> : IOperation
    {
        protected Modifidy(IEnumerable<object> keys, T value, Action<T> modifier)
        {
            Keys = keys;
            Value = value;
            Modifier = modifier;
        }

        public IEnumerable<Object> Keys { get; private set; }
        public T Value { get; private set; }
        public Action<T> Modifier { get; private set; }

        public abstract void Apply();

    }

    public abstract class BatchInert<TValue> : IOperation
    {
        public BatchInert(IEnumerable<KeyValuePair<IEnumerable<object>, TValue>> keyValuePairs)
        {
            this.keyValuePairs = keyValuePairs;
        }

        public IEnumerable<KeyValuePair<IEnumerable<Object>, TValue>> keyValuePairs { get; private set; }

        public abstract void Apply();
    }

    public class DefaultBatchInsert<TValue> : BatchInert<TValue>
    {
        public DefaultBatchInsert(IEnumerable<KeyValuePair<IEnumerable<Object>, TValue>> keyValuePairs, Func<IEnumerable<object>, TValue, Insert<TValue>> singleInsertGenerator) : base(keyValuePairs)
        {
            SingleInsertGenerator = singleInsertGenerator;
        }

        public Func<IEnumerable<Object>, TValue, Insert<TValue>> SingleInsertGenerator { get; private set; }

        public override void Apply()
        {
            var inserts = keyValuePairs.Select(_ => SingleInsertGenerator(_.Key, _.Value));
            foreach (var insert in inserts)
                insert.Apply();
        }
    }
}
