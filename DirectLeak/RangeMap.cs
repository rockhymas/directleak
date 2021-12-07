using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DirectLeak
{
    [Serializable]
    public struct Range : IEquatable<Range>
    {
        public static readonly Range None = new Range(int.MinValue, int.MinValue);

        private readonly int begin;
        private readonly int end;

        public int Begin
        {
            get
            {
                return begin;
            }
        }

        public int End
        {
            get
            {
                return end;
            }
        }

        public Range(int begin, int end)
        {
            this.begin = Math.Min(begin, end);
            this.end = Math.Max(begin, end);
        }

        public int Length
        {
            get
            {
                return End - Begin;
            }
        }

        public Range Offset(int offset)
        {
            return new Range(Begin + offset, End + offset);
        }

        public Range Include(Range that)
        {
            return new Range(Math.Min(Begin, that.Begin), Math.Max(End, that.End));
        }

        [Pure]
        public Range Intersect(Range that)
        {
            return new Range(Math.Max(Begin, that.Begin), Math.Min(End, that.End));
        }

        public bool Intersects(Range that)
        {
            return Begin < that.End && that.Begin < End;
        }

        public bool Contains(Range that)
        {
            return Begin <= that.Begin && that.End <= End;
        }

        public bool Contains(int i)
        {
            return Begin <= i && i < End;
        }

        #region Equality

        public bool Equals(Range other)
        {
            return other.Begin == Begin && other.End == End;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(Range))
            {
                return false;
            }
            return Equals((Range)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable NonReadonlyFieldInGetHashCode
                return (Begin * 397) ^ End;
                // ReSharper restore NonReadonlyFieldInGetHashCode
            }
        }

        public static bool operator ==(Range left, Range right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Range left, Range right)
        {
            return !Equals(left, right);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}..{1}", Begin, End);
        }
    }

    [Serializable]
    public class RangeMap<T> : IEnumerable<KeyValuePair<Range, T>>
    {
        public bool Empty
        {
            get
            {
                return entries.Count == 0;
            }
        }

        public Range Domain
        {
            get
            {
                if (Empty)
                {
                    return Range.None;
                }
                var values = entries.Values;
                return new Range(values[0].Key.Begin, values[values.Count - 1].Key.End);
            }
        }

        public IEnumerable<Range> Keys => this.Select(kvp => kvp.Key);

        public RangeMap(bool simplify)
        {
            this.simplify = simplify;
        }

        public RangeMap()
        {
        }

        public void Clear()
        {
            entries.Clear();
        }

        public void Clear(Range range)
        {
            if (range.Length == 0)
            {
                return;
            }
            var index = SetInternal(range, default(T));
            entries.RemoveAt(index);
        }

        public void Clear(IEnumerable<Range> ranges)
        {
            foreach (var range in ranges)
            {
                Clear(range);
            }
        }

        public void Set(Range range, T value)
        {
            if (range.Length == 0)
            {
                return;
            }
            var index = SetInternal(range, value);
            Optimize(index);
        }

        public void Set(IEnumerable<KeyValuePair<Range, T>> values)
        {
            foreach (var value in values)
            {
                Set(value.Key, value.Value);
            }
        }

        public IEnumerable<KeyValuePair<Range, T>> Sub(Range range)
        {
            for (var i = Find(range.Begin); i < entries.Count; ++i)
            {
                var entry = entries.Values[i];
                if (range.End <= entry.Key.Begin)
                {
                    yield break;
                }
                yield return
                    range.Contains(entry.Key)
                        ? entry
                        : new KeyValuePair<Range, T>(entry.Key.Intersect(range), entry.Value);
            }
        }

        public T GetAt(int point)
        {
            var i = Find(point);
            if (entries.Count <= i)
            {
                return default(T);
            }
            var entry = entries.Values[i];
            return entry.Key.Contains(point) ? entry.Value : default(T);
        }

        #region Implementation of IEnumerable

        public IEnumerator<KeyValuePair<Range, T>> GetEnumerator()
        {
            return entries.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            return string.Join(", ", this.Select(p => string.Format("{0}:{1}", p.Key, p.Value)).ToArray());
        }

        private int SetInternal(Range range, T value)
        {
            KeyValuePair<Range, T> existingEntry;
            if (entries.TryGetValue(range.Begin, out existingEntry))
            {
                if (range.End < existingEntry.Key.End)
                {
                    AddEntry(range.End, existingEntry.Key.End, existingEntry.Value);
                }
            }

            entries[range.Begin] = new KeyValuePair<Range, T>(range, value);
            var index = entries.IndexOfKey(range.Begin);

            // Trim following ranges
            while (index + 1 < entries.Count)
            {
                var nextEntry = entries.Values[index + 1];
                if (range.End <= nextEntry.Key.Begin)
                {
                    break;
                }
                if (range.End < nextEntry.Key.End)
                {
                    AddEntry(range.End, nextEntry.Key.End, nextEntry.Value);
                }
                entries.RemoveAt(index + 1);
            }

            // Trim preceding ranges
            while (0 <= index - 1)
            {
                var priorEntry = entries.Values[index - 1];
                if (range.End < priorEntry.Key.End)
                {
                    AddEntry(range.End, priorEntry.Key.End, priorEntry.Value);
                }
                if (priorEntry.Key.End <= range.Begin)
                {
                    break;
                }
                entries.RemoveAt(index - 1);
                if (priorEntry.Key.Begin <= range.Begin)
                {
                    AddEntry(priorEntry.Key.Begin, range.Begin, priorEntry.Value);
                    break;
                }
                --index;
            }

            return index;
        }

        private void Optimize(int index)
        {
            if (!simplify)
            {
                return;
            }

            TryJoin(index);
            TryJoin(index - 1);
        }

        private void TryJoin(int index)
        {
            if (0 > index || index + 1 >= entries.Count)
            {
                return;
            }
            var first = entries.Values[index];
            var second = entries.Values[index + 1];
            if (!Equals(first.Key.End, second.Key.Begin) || !Equals(first.Value, second.Value))
            {
                return;
            }
            entries.RemoveAt(index + 1);
            ReplaceEntry(first.Key.Begin, second.Key.End, first.Value);
        }

        private void AddEntry(int begin, int end, T value)
        {
            AddEntry(new Range(begin, end), value);
        }

        private void AddEntry(Range range, T value)
        {
            entries.Add(range.Begin, new KeyValuePair<Range, T>(range, value));
        }

        private void ReplaceEntry(int begin, int end, T value)
        {
            ReplaceEntry(new Range(begin, end), value);
        }

        private void ReplaceEntry(Range range, T value)
        {
            entries[range.Begin] = new KeyValuePair<Range, T>(range, value);
        }

        // Returns the index of the first element whose ending is after the given begin
        private int Find(int begin)
        {
            return Find(begin, 0, entries.Count);
        }

        private int Find(int begin, int min, int max)
        {
            while (min < max)
            {
                var guess = min + (max - min) / 2;
                var entry = entries.Values[guess];
                if (entry.Key.Contains(begin))
                {
                    return guess;
                }
                if (entry.Key.End <= begin)
                {
                    min = guess + 1;
                }
                else if (begin < entry.Key.End)
                {
                    Debug.Assert(begin < entry.Key.Begin);
                    max = guess;
                }
            }
            Debug.Assert(min == max);
            return min;
        }

        private readonly SortedList<int, KeyValuePair<Range, T>> entries = new SortedList<int, KeyValuePair<Range, T>>();

        private readonly bool simplify = true;
    }
}
