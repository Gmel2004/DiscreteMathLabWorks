namespace Task1
{
    public class NumericalSet
    {
        public HashSet<int> set = new HashSet<int> { };

        private int begin = -500, end = 500;

        public NumericalSet() {}//Конструктор без параметров

        public NumericalSet(int begin, int end) => SetUniverse(begin, end);//Конструктор с параметрами

        public NumericalSet(NumericalSet Copied)
        { 
            begin = Copied.begin;
            end = Copied.end;
            set = Copied.set;
        }//Конструктор копирования

        private static bool CheckUniverse(NumericalSet first, NumericalSet second)
        {
            bool isOneUniverse = first.begin == second.begin
                && first.end == second.end;
            return isOneUniverse;
        }//Проверить универсум
        public NumericalSet CreateRandom(int count)
        {
            if (count >= 0 && end - begin + 1 >= count)
            {
                var rnd = new Random();
                NumericalSet result = new NumericalSet();
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

        public NumericalSet Where(Func<int, bool> predicate)
        {
            NumericalSet result = new NumericalSet();
            foreach (var i in set)
            {
                if (predicate(i))
                {
                    result.Add(i);
                }
            }
            return result;
        }//Отобрать по условию

        public static NumericalSet operator +(NumericalSet first, NumericalSet second)
        {
            if (CheckUniverse(first, second))
            {
                NumericalSet result = new NumericalSet();
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

        public static NumericalSet operator -(NumericalSet first, NumericalSet second)
        {
            if (CheckUniverse(first, second))
            {
                NumericalSet result = new NumericalSet();
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

        public NumericalSet Cross(NumericalSet second)
        {
            if (CheckUniverse(second, this))
            {
                NumericalSet result = new NumericalSet();
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

        public NumericalSet CalculateSymmetricDif(NumericalSet second)
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
        }//Вывести информацию о множестве

        public NumericalSet GetUniverse()
        {
            NumericalSet result = new NumericalSet();
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

        public static NumericalSet operator !(NumericalSet first)
        {
            return first.GetUniverse() - first;
        }//Вычислить дополнение
    }//дерево синтаксического разбора
}