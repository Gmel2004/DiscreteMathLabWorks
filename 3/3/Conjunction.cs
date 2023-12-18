using System.Collections;

namespace _3
{
    public class Conjunction : IEnumerable<char>
    {
        private char[] items; // Или HashSet ?

        public int Count => items.Length;

        public char this[int index]
        {
            get => items[index];
            set { items[index] = value; }
        }

        public Conjunction(int count) => items = new char[count];

        public Conjunction(IEnumerable<char> items)
        {
            this.items = items.ToArray();
        }

        public Conjunction(string items)
        {
            this.items = new char[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                var alpha = (char)('a' + i);
                this[i] = items[i] == '1' ? char.ToUpper(alpha) : alpha;
            }
        }

        public IEnumerator<char> GetEnumerator()
        {
            return ((IEnumerable<char>)items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public override bool Equals(object? obj)
        {
            return obj is Conjunction conjunction && conjunction.SequenceEqual(this);
        }

        public override int GetHashCode()
        {
            return this.Aggregate(0, (t, a) => HashCode.Combine(t, a));
        }

        public override string ToString()
        {
            return string.Join("", items);
        }
    }
}
