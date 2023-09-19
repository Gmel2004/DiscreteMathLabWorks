namespace Task1 {
    public class mySet
    {
        public void Main() { }
        public List<int> set = new List<int> { };

        public mySet CreateRandom(int count)
        {
            var rnd = new Random();
            mySet result = new mySet();
            for (int i = 0; i < count; i++)
            {
                result.set.Add(rnd.Next(-500, 500 + 1));
            }
            return result;
        }

        public void Copy(mySet source)
        {
            set = source.set;
        }

        public void Add(int number)
        {
            if (!set.Contains(number))
            {
                set.Add(number);
            }
        }

        public void CreateBy()
        {

        }

        public static mySet operator +(mySet first, mySet second)
        {
            mySet result = new mySet();
            foreach (var i in first.set)
            {
                result.set.Add(i);
            }
            foreach (var i in second.set)
            {
                result.set.Add(i);
            }
            result.set = result.set.Distinct().ToList();
            return result;
        }

        public static mySet operator -(mySet first, mySet second)
        {
            mySet result = new mySet();
            foreach (var i in first.set)
            {
                if (!second.set.Contains(i))
                {
                    result.set.Add(i);
                }
            }
            return result;
        }

        public mySet Cross(mySet second)
        {
            mySet result = new mySet();
            foreach (var i in set)
            {
                if (second.set.Contains(i))
                {
                    result.set.Add(i);
                }
            }
            return result;
        }

        public void Write()
        {
            Console.Write(set[0]);
            for (var i = 1; i < set.Count; i++)
            {
                Console.Write($" {i}");
            }
            Console.WriteLine();
        }
    }
}