namespace Task1
{
    public class MySet
    {
        public HashSet<int> set = new HashSet<int> { };

        private int begin = -500, end = 500;

        public MySet() {}

        public MySet(int begin, int end) => SetUniverse(begin, end);

        private static bool CheckUniverse(MySet first, MySet second)
        {
            bool isOneUniverse = first.begin == second.begin
                && first.end == second.end;
            return isOneUniverse;
        }//Проверить универсум
        public MySet CreateRandom(int count)
        {
            if (count >= 0 && end - begin + 1 >= count)
            {
                var rnd = new Random();
                MySet result = new MySet();
                for (int i = 0; i < count; i++)
                {
                    var newCount = rnd.Next(-500, 500 + 1);
                    while (set.Contains(newCount))
                    {
                        newCount = rnd.Next(-500, 500 + 1);
                    }
                    result.set.Add(newCount);
                }
                return result;
            }
            else
            {
                throw new Exception("Ошибка: нельзя создать множество, задано недопустимое количество элементов");
            }
        }//Случайно заполнинить

        public void Add(int number)
        {
            if (!set.Contains(number) && begin <= end && number <= end)
            {
                set.Add(number);
            }
            else
            {
                throw new Exception("Ошибка: данный элемент нельзя добавить в множество");
            }
        }//Добавить элемент

        public void Remove(int number)
        {
            set.Remove(number);
        }//Удалить элемент

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
            if (CheckUniverse(first, second))
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
                return result;
            }
            else
            {
                throw new Exception("Невозможно выполнить операцию: Множества имеют разные универсумы");
            }
        }//Перегрузка оператора +

        public static MySet operator -(MySet first, MySet second)
        {
            if (CheckUniverse(first, second))
            {
                MySet result = new MySet();
                foreach (var i in first.set)
                {
                    if (!second.set.Contains(i))
                    {
                        result.set.Add(i);
                    }
                }
                return result;
            }
            else
            {
                throw new Exception("Невозможно выполнить операцию: Множества имеют разные универсумы");
            }
        }//Перегрузка оператора - 

        public MySet Cross(MySet second)
        {
            if (CheckUniverse(second, this))
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
            }
            else
            {
                throw new Exception("Невозможно выполнить операцию: Множества имеют разные универсумы");
            }
        }//Пересечь

        public MySet CalculateSymmetricDif(MySet second)
        {
            if (CheckUniverse(second, this))
            {
                return (this - second) + (second - this);
            }
            else
            {
                throw new Exception("Невозможно выполнить операцию: Множества имеют разные универсумы");
            }
        }//Вычислить симметричрую разность

        public void WriteInformation()
        {
            Console.WriteLine("{", String.Join(", ", set), '}', $" Универсум: [{begin}, {end}]");
        }//Вывести множество на экран

        public MySet GetUniverse()
        {
            MySet result = new MySet();
            result.set = Enumerable.Range(begin, end - begin + 1).ToHashSet();
            return result;
        }//Получить универсум

        public void SetUniverse(int begin, int end)
        {
            if (begin <= end && set.All(t => t >= begin && t <= end))
            {
                this.begin = begin;
                this.end = end;
            }
            else
            {
                throw new Exception("Ошибка: заданы недопустимые границы для универсума");
            }
        }//Задать универсум

        public static MySet operator !(MySet first)
        {
            return first.GetUniverse() - first;
        }//Вычислить дополнение
    }
}