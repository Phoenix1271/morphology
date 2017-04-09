using System;
using Morphology.Collections;
using Morphology.Test.Support;
using Xunit;

namespace Morphology.Test.Collections
{
    public class CacheTests
    {
        [Fact]
        public void Clear_CacheHasItems_DiscardsAllItems()
        {
            var cache = new Cache<int, string>();

            cache.Fetch(Some.Int(), () => Some.String());
            Assert.Equal(1, cache.Count);

            cache.Clear();
            Assert.Equal(0, cache.Count);
        }

        [Fact]
        public void Contains_KeyExists_ReturnsTrue()
        {
            int key = Some.Int();

            var cache = new Cache<int, string>();
            Assert.False(cache.Contains(key));

            cache.Add(key, () => Some.String());
            Assert.True(cache.Contains(key));
        }

        [Fact]
        public void Fetch_SomeKey_ValueIsProperlyCached()
        {
            int key = Some.Int();
            string value = Some.String();
            var cache = new Cache<int, string>();

            string actual = cache.Fetch(key, () => value);
            Assert.Equal(1, cache.Count);
            Assert.Equal(value, actual);

            string cached = cache.Fetch(key, () => value);
            Assert.Equal(value, cached);
        }

        [Fact]
        public void New_Cache_DefaultValuesAreSet()
        {
            var cache = new Cache<int, string>();

            Assert.Equal(1000, cache.Capacity);
            Assert.Equal(0, cache.Count);
            Assert.False(cache.IsFull);
        }

        [Fact]
        public void New_InvalidCapacity_ArgumentOutOfRangeIsThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cache<int, string>(0));
        }

        [Fact]
        public void TryGetValue_EmptyCache_ReturnsFalse()
        {
            var cache = new Cache<int, string>();

            string value;
            Assert.False(cache.TryGetValue(Some.Int(), out value));
        }

        [Fact]
        public void TryGetValue_ExistingKey_ReturnsKey()
        {
            int key = Some.Int();
            string value = Some.String();

            var cache = new Cache<int, string>();

            cache.Add(key, () => value);
            Assert.Equal(1, cache.Count);

            string actual;
            Assert.True(cache.TryGetValue(key, out actual));
            Assert.Equal(value, actual);
        }

        [Fact]
        public void TryGetValue_NonExistingKey_ReturnsFalse()
        {
            int key = Some.Int();
            string value = Some.String();

            var cache = new Cache<int, string>();

            cache.Add(key, () => value);
            Assert.Equal(1, cache.Count);

            string actual;
            Assert.False(cache.TryGetValue(Some.Int(), out actual));
        }

        [Fact]
        public void Update_FullCache_LeastRecentlyUsedItemIsReplaced()
        {
            int capacity = Some.Int();
            int firstKey = Some.Int();
            string fistValue = Some.String();

            var cache = new Cache<int, string>(capacity);
            Assert.Equal(capacity, cache.Capacity);

            cache.Add(firstKey, () => fistValue);
            for (int i = 0; i < capacity - 1; i++)
            {
                cache.Add(Some.Int(), () => Some.String());
            }

            Assert.True(cache.IsFull);
            Assert.True(cache.Contains(firstKey));

            cache.Add(Some.Int(), () => Some.String());

            Assert.False(cache.Contains(firstKey));
        }

        [Fact]
        public void Update_SomeKey_ValueIsProperlyUpdated()
        {
            int key = Some.Int();
            string value = Some.String();

            var cache = new Cache<int, string>();

            cache.Add(key, () => value);
            Assert.Equal(1, cache.Count);

            string actual;
            Assert.True(cache.TryGetValue(key, out actual));
            Assert.Equal(value, actual);

            string updated = Some.String();
            cache.Add(key, () => updated);
            Assert.Equal(1, cache.Count);

            Assert.True(cache.TryGetValue(key, out actual));
            Assert.Equal(updated, actual);
        }
    }
}
