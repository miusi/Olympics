using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Olympics.Core.Repository.ChangeTracing
{
    internal class ChangeTracker<T>
    {
        static ChangeTracker()
        {
            CanTrackChanges = typeof(T).GetConstructor(Type.EmptyTypes) != null;

        }

        public ChangeTracker(T value,Func<T, object> keySelector)
        {
            CurrentValue = value;
            KeySelector = keySelector;
            InitialValue = CanTrackChanges ? (T)Activator.CreateInstance(typeof(T)) : default(T);
            if (CanTrackChanges)
                AutoMapper.Mapper.Map(CurrentValue, InitialValue);
        }

        public static bool CanTrackChanges { get; private set; }

        public Func<T,object> KeySelector { get; private set; }

        public T CurrentValue { get; private set; }

        public T InitialValue { get; private set; }

        internal bool HasChanges
        {
            get
            {
                if (!KeySelector(CurrentValue).Equals(KeySelector(InitialValue)))
                {
                    throw new InvalidOperationException("You cannot change an object's key value.");
                }

                return CanTrackChanges && CurrentValue.GetType()==InitialValue.GetType();
            }
        }
    }
}
