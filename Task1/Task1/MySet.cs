namespace Task1
{
    public class MySet
    {
        public List<int> set = new List<int> { };

        public MySet CreateRandom(int count)
        {
            var rnd = new Random();
            MySet result = new MySet();
            for (int i = 0; i < count; i++)
            {
                result.set.Add(rnd.Next(-500, 500 + 1));
            }
            result.set.Sort();
            return result;
        }//Случайно заполнинить

        public void Add(int number)
        {
            if (!set.Contains(number))
            {
                set.Add(number);
            }
            set.Sort();
        }//Добавить элемент

        public void Remove(int number)
        {
            set.Remove(number);
        }//Удалить элемент

        public void RemoveAt(int number)
        {
            set.RemoveAt(number);
        }//Удалить элемент по индексу

        public MySet Where(Func<int, bool> predicate)
        {
            MySet result = new MySet();
            foreach (var i in set)
            {
                if (predicate(i))
                {
                    result.Add(i);
                }
            }
            return result;
        }//Отобрать по условию

        public static MySet operator +(MySet first, MySet second)
        {
            MySet result = new MySet();
            foreach (var i in first.set)
            {
                result.set.Add(i);
            }
            foreach (var i in second.set)
            {
                result.set.Add(i);
            }
            result.set = result.set.Distinct().ToList();
            result.set.Sort();
            return result;
        }//Перегрузка оператора +

        public static MySet operator -(MySet first, MySet second)
        {
            MySet result = new MySet();
            foreach (var i in first.set)
            {
                if (!second.set.Contains(i))
                {
                    result.set.Add(i);
                }
            }
            result.set.Sort();
            return result;
        }//Перегрузка оператора - 

        public MySet Cross(MySet second)
        {
            MySet result = new MySet();
            foreach (var i in set)
            {
                if (second.set.Contains(i))
                {
                    result.set.Add(i);
                }
            }
            return result;
        }//Пересечь

        public MySet CalculateSymmetricDif(MySet second)
        {
            return (this - second) + (second - this);
        }//Вычислить симметричрую разность

        public void Write()
        {
            if (set.Count > 0)
            {
                Console.Write(set[0]);
            }
            for (var i = 1; i < set.Count; i++)
            {
                Console.Write($" {set[i]}");
            }
            Console.WriteLine();
        }//Вывести множество на экран

        public MySet FullSet()
        {
            MySet result = new MySet();
            result.set = Enumerable.Range(-500, 1001).ToList();
            return result;
        }//Задать универсум
    }
}
