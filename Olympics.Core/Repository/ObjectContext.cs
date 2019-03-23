using Olympics.Core.Repository.ChangeTracing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Olympics.Core.Repository
{
    public class ObjectContext<T>
    {
        public ObjectContext(T @object)
        {
            Object = @object;
        }

        public T Object { get; private set; }
    }

    public class EnumerableObjectContext<T> : IQueryable<T>
    {
        private IQueryProvider mCachedQueryProvider = null;

        public EnumerableObjectContext(IQueryable<T> objects, Repository<T> parentRepository)
        {
            Objects = objects;
            ParentRepository = parentRepository;
        }

        private Repository<T> ParentRepository { get; set; }

        private IQueryable<T> Objects { get; set; }

        public Type ElementType
        {
            get
            {
                return Objects.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return Objects.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                if (mCachedQueryProvider == null)
                    mCachedQueryProvider = Objects.Provider;
                return mCachedQueryProvider;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CallbackEnumerator<T>(Objects.GetEnumerator(), x => ParentRepository.AddChangeTracker(x));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TQueryable AsRaw<TQueryable>() where TQueryable : class
        {
            return Objects as TQueryable;
        }
    }
}
