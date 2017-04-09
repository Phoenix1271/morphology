using System;
using System.Collections.Generic;
using System.Threading;

namespace Morphology.Collections
{
    /// <summary>
    /// Represents a collection of keys and cached values that are evicted based on least recently used item.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class Cache<TKey, TValue>
    {
        #region Private Fields

        private readonly Dictionary<TKey, Lazy<TValue>> _items;
        private readonly LinkedList<TKey> _queue;
        private readonly object _sync = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="Cache{TKey,TValue}"/> cache with least recently used eviction strategy.
        /// </summary>
        /// <param name="capacity">The number of items to hold.</param>
        public Cache(int capacity = 1000)
        {
            if (capacity <= 2)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity should be greater than 1 item.");

            Capacity = capacity;
            _queue = new LinkedList<TKey>();
            _items = new Dictionary<TKey, Lazy<TValue>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the maximum number of entries in the cache.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Gets the current number of entries in the cache.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Gets whether or not the cache is full.
        /// </summary>
        public bool IsFull => _items.Count == Capacity;

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item in the cache. If item already exists, it's updated.
        /// </summary>
        /// <param name="key">The key of the cached item.</param>
        /// <param name="producer">Factory method that produces cached value if it doesn't exists.</param>
        public void Add(TKey key, Func<TValue> producer)
        {
            AddInternal(key, producer);
        }

        /// <summary>
        /// Removes the cached items.
        /// </summary>
        public void Clear()
        {
            lock (_sync)
            {
                _items.Clear();
                _queue.Clear();
            }
        }

        /// <summary>
        /// Check if the item's key is cached.
        /// </summary>
        /// <param name="key">The key of the cached item></param>
        /// <returns><c>true</c> if key is cached; otherwise <c>false</c></returns>
        public bool Contains(TKey key)
        {
            lock (_sync)
            {
                return _items.ContainsKey(key);
            }
        }

        /// <summary>
        /// Retrieves an item from the cache. If the item doesn't exist it will be created.
        /// </summary>
        /// <param name="key">The key of the cached item.</param>
        /// <param name="producer">Factory method that produces cached value if it doesn't exists.</param>
        public TValue Fetch(TKey key, Func<TValue> producer)
        {
            lock (_sync)
            {
                Lazy<TValue> dummy;
                return _items.TryGetValue(key, out dummy)
                    ? dummy.Value
                    : AddInternal(key, producer).Value;
            }
        }

        /// <summary>
        /// Tries to retrieve value from the cache.
        /// </summary>
        /// <param name="key">The key of the cached item.</param>
        /// <param name="value"> Cached item, if method succeeds; otherwise default value.</param>
        /// <returns><c>true</c> if item was retrieved; otherwise <c>false</c></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);

            lock (_sync)
            {
                Lazy<TValue> dummy;
                if (_items.TryGetValue(key, out dummy))
                {
                    _queue.Remove(key);
                    _queue.AddLast(key);

                    value = dummy.Value;
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Private Methods

        private Lazy<TValue> AddInternal(TKey key, Func<TValue> producer)
        {
            lock (_sync)
            {
                CheckCapacity();

                var value = new Lazy<TValue>(producer, LazyThreadSafetyMode.ExecutionAndPublication);
                _items[key] = value;
                _queue.AddLast(key);

                return value;
            }
        }

        private void CheckCapacity()
        {
            lock (_sync)
            {
                if (!IsFull) return;

                var node = _queue.First;
                _items.Remove(node.Value);
                _queue.RemoveFirst();
            }
        }

        #endregion
    }
}
