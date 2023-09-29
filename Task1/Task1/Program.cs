namespace Task1
{
    internal class Program
    {
        public static Dictionary<string, MySet> Sets = new Dictionary<string, MySet>() { };
        public static void Main()
        {
            var answer = 0;
            while (answer != 8)
            {
                Console.Clear();
                Console.WriteLine("Введите номер команды:\n" +
                    "1. Создать/пересоздать множество\n" +
                    "2. Добавить элемент в множество\n" +
                    "3. Удалить элемент из множества\n" +
                    "4. Вывести информацию о множестве на экран\n" +
                    "5. Вывести информацию о всех множествах на экран\n" +
                    "6. Вычислить значение\n" +
                    "7. Изменить универсум\n" +
                    "8. Выход");
                var isCorrect = int.TryParse(Console.ReadLine(), out answer);
                while (!isCorrect || answer < 1 || answer > 8)
                {
                    Console.Write("Неверный ввод! Введите целое число от 1 до 6: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out answer);
                }
                //Do: need try catch?
                switch (answer)
                {
                    case 1:
                        CreateSet();
                        break;
                    case 2:
                        AddToSet();
                        break;
                    case 3:
                        RemoveFromSet();
                        break;
                    case 4:
                        WriteSet();
                        break;
                    case 5:
                        WriteSets();
                        break;
                    case 6:
                        CalculateValue();
                        break;
                    case 7:
                        ChangeUniverse();
                        break;
                }
            }
        }
        public static string ChooseExistingSet()
        {
            var nameOfSet = ChooseSet();
            while (!Sets.Keys.Contains(nameOfSet))
            {
                Console.WriteLine("Такого множества не существует!");
                nameOfSet = ChooseSet();
            }
            return nameOfSet;
        }
        public static (string, int) DoActionWithElement()
        {
            var nameOfSet = ChooseExistingSet();
            int element = 0;
            Console.WriteLine("Введите элемент: ");
            var isCorrect = int.TryParse(Console.ReadLine(), out element);
            while (!isCorrect)
            {
                Console.WriteLine("Неверный ввод! Введите элемент: ");
                isCorrect = int.TryParse(Console.ReadLine(), out element);
            }
            return (nameOfSet, element);
        }
        public static void RemoveFromSet()
        {
            (var nameOfSet, var element) = DoActionWithElement();
            Sets[nameOfSet].Remove(element);
        }
        public static void AddToSet()
        {
            (var nameOfSet, var element) = DoActionWithElement();
            Sets[nameOfSet].Add(element);
        }
        public static string ChooseSet()
        {
            Console.WriteLine("Введите название множества: ");
            string nameOfSet = Console.ReadLine();
            while (nameOfSet == null)
            {
                Console.WriteLine("Неверный ввод. Введите непустую строку: ");
                nameOfSet = Console.ReadLine();
            }
            return nameOfSet;
        }
        public static void CreateSet()
        {
            string nameOfSet = ChooseSet();
            var answer = 0;
            Console.WriteLine("Выберите способ заполнения:\n" +
                "1. Случайное заполнение\n" +
                "2. Ручное заполнение\n" +
                "3. По условию");
            var isCorrect = int.TryParse(Console.ReadLine(),out answer);
            while(!isCorrect || answer < 1 || answer > 3)
            {
                Console.Write("Неверный ввод! Введите целое число от 1 до 3: ");
                isCorrect = int.TryParse(Console.ReadLine(), out answer);
            }
            switch (answer)
            {
                case 1:
                    Console.Write("Введите количество элементов: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    while (!isCorrect)
                    {
                        Console.Write("Неверный ввод! Введите количество элементов: ");
                        isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    }
                    Sets[nameOfSet] = new MySet().CreateRandom(answer);
                    break;
                case 2:
                    Sets[nameOfSet] = new MySet();
                    Console.Write("Введите количество элементов: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    while (!isCorrect)
                    {
                        Console.Write("Неверный ввод! Введите количество элементов: ");
                        isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    }
                    while (answer-- > 0)
                    {
                        var element = 0;
                        isCorrect = int.TryParse(Console.ReadLine(), out element);
                        while (!isCorrect)
                        {
                            Console.Write("Неверный ввод! Введите элемент множества: ");
                            isCorrect = int.TryParse(Console.ReadLine(), out element);
                        }
                        Sets[nameOfSet].set.Add(element);
                    }
                    break;
                case 3://Do
                    Sets[nameOfSet] = new MySet();
                    Console.Write("Новое множество будет создано на основе базового.\n" +
                        "Выберите базовое множество:\n" +
                        "1. Существующее множество\n" +
                        "2. Универсум текущего множества");
                    isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    while (!isCorrect || answer < 1 || answer > 2)
                    {
                        Console.Write("Неверный ввод! Число от 1 до 2: ");
                        isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    }
                    MySet BaseSet = new MySet();
                    if (answer == 1)
                    {
                        string nameOfBaseSet = ChooseExistingSet();
                        BaseSet = Sets[nameOfBaseSet];
                    }
                    else
                    {
                        BaseSet = Sets[nameOfSet].GetUniverse();
                    }
                    Console.Write("Введите номер команды, содержащий знак,\n" +
                        "который вы хотитите применить для отборки элементов:\n" +
                        "1. >\n" +
                        "2. <\n" +
                        "3. >=\n" +
                        "4. <=\n" +
                        "5. %\n");
                    isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    while (!isCorrect || answer < 1 || answer > 2)
                    {
                        Console.Write("Неверный ввод! Число от 1 до 2: ");
                        isCorrect = int.TryParse(Console.ReadLine(), out answer);
                    }
                    break;
            }
        }
        public static void WriteSet()
        {
            var nameOfSet = ChooseSet();
            if (Sets.Keys.Contains(nameOfSet))
            {
                Sets[nameOfSet].WriteInformation();
            }
            else
            {
                Console.WriteLine("Такого множества не существует!");
            }
            Console.WriteLine("Нажмите Enter для выхода в меню");
            Console.ReadLine();
        }
        private static void WriteSets()
        {
            foreach (var i in Sets.Values)
            {
                i.WriteInformation();
            }
            Console.WriteLine("Нажмите Enter для выхода в меню");
            Console.ReadLine();
        }
        public static void CalculateValue()
        {
            Console.WriteLine("Введите выражение:");
            var expression = Console.ReadLine();
            var isCorrect = expression != null;
            //Do
        }
        public static void ChangeUniverse()
        {
            var nameOfSet = ChooseExistingSet();
            int begin, end;
            Console.Write("Введите начальную границу: ");
            var isCorrect = int.TryParse(Console.ReadLine(), out begin);
            while (!isCorrect)
            {
                Console.WriteLine("Неверный ввод! Введите начальную границу: ");
            }
            Console.Write("Введите конечную границу: ");
            isCorrect = int.TryParse(Console.ReadLine(), out end);
            while (!isCorrect)
            {
                Console.WriteLine("Неверный ввод! Введите конечную границу: ");
            }
            Sets[nameOfSet].SetUniverse(begin, end);
        }
    }
}