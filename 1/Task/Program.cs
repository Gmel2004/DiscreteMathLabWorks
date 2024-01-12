using static UserInput.CustomConsoleInput;

namespace Task
{
    internal class Program
    {
        public static Dictionary<string, NumericalSet> sets = new Dictionary<string, NumericalSet>() { };

        public static void Main()
        {
            var answer = 0;

            while (answer != 8)
            {
                Console.Clear();
                var mainMessage = "Введите номер команды:\n" +
                    "1. Создать/пересоздать множество\n" +
                    "2. Добавить элемент в множество\n" +
                    "3. Удалить элемент из множества\n" +
                    "4. Вывести информацию о множестве на экран\n" +
                    "5. Вывести информацию о всех множествах на экран\n" +
                    "6. Вычислить значение\n" +
                    "7. Изменить универсум\n" +
                    "8. Выход";
                answer = ReadInt(mainMessage, "Неверный ввод! Введите целое число от 1 до 8:", 1, 8);

                try
                {
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
                catch (Exception ex)
                {
                    Console.WriteLine($"\n{ex}\n");
                    Console.WriteLine("Нажмите Enter для выхода в меню");
                    Console.ReadLine();
                }
            }
        }

        public static (string, int) ChooseSetAndElement()//Выбрать множество и элемент
        {
            var nameOfSet = ChooseExistingSet();
            int element = ReadInt("Введите элемент:", "Неверный ввод! Введите число:");
            return (nameOfSet, element);
        }

        public static void RemoveFromSet()//Удалить из множества
        {
            (var nameOfSet, var element) = ChooseSetAndElement();
            sets[nameOfSet].Remove(element);
        }

        public static void AddToSet()//Добавить во множество
        {
            (var nameOfSet, var element) = ChooseSetAndElement();
            try
            {
                sets[nameOfSet].Add(element);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Нажмите Enter для выхода в меню");
                Console.ReadLine();
            }
        }

        public static string ChooseSet()
        {
            string nameOfSet = ReadString("Введите название множества:", "Неверный ввод. Введите непустую строку:");
            return nameOfSet;
        }

        public static string ChooseExistingSet()//Выбрать множество
        {
            string? nameOfSet = "";
            bool isCorrect = false;
            do
            {
                nameOfSet = ReadString("Введите название множества:", "Неверный ввод. Введите непустую строку:");
                isCorrect = sets.ContainsKey(nameOfSet);
                if (!isCorrect)
                {
                    Console.WriteLine("Такого множества не существует! Повторите ввод!");
                }
            }
            while (!isCorrect);
            return nameOfSet;
        }

        public static void CreateSetRandom(string nameOfSet)//Создать множество случайно
        {
            var answer = ReadInt("Введите количество элементов:", "Неверный ввод! Введите неотрицательное число:", 0);
            try
            {
                sets[nameOfSet] = new NumericalSet().CreateRandom(answer);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Нажмите Enter для выхода в меню");
                Console.ReadLine();
            }
        }

        public static void CreateSetManually(string nameOfSet)//Создать множество вручную
        {
            sets[nameOfSet] = new NumericalSet();
            var answer = ReadInt("Введите количество элементов:", "Неверный ввод!(Учтите, что колчество элементов должно соответствовать универсуму)" +
                "Введите количество элементов:", 0,
                sets[nameOfSet].GetEnd() - sets[nameOfSet].GetBegin() + 1);
            while (answer-- > 0)
            {
                var element = ReadInt("Введите элемент:",
                    $"Неверный ввод! Введите число от {sets[nameOfSet].GetBegin()} до {sets[nameOfSet].GetEnd()}",
                    sets[nameOfSet].GetBegin(), sets[nameOfSet].GetEnd());
                sets[nameOfSet].Add(element);
            }
        }

        public static void CreateSetCondition(string nameOfSet)//Создать множество по условию
        {
            var mainMessage = "Новое множество будет создано на основе базового.\n" +
                "Выберите базовое множество:\n" +
                "1. Существующее множество\n" +
                "2. Универсум текущего множества";
            var answer = ReadInt(mainMessage, "Неверный ввод! Число от 1 до 2:", 1, 2);
            NumericalSet BaseSet = new NumericalSet();
            if (answer == 1)
            {
                string nameOfBaseSet = ChooseExistingSet();
                BaseSet = sets[nameOfBaseSet];
            }
            else
            {
                if (!sets.ContainsKey(nameOfSet))
                {
                    Console.WriteLine("Ошибка! Множества с данным именем не существует! Повторите ввод!");
                    nameOfSet = ChooseExistingSet();
                }
                BaseSet = sets[nameOfSet].GetUniverse();
            }
            mainMessage = "Введите номер команды, содержащий знак,\n" +
                "который вы хотитите применить для отборки элементов:\n" +
                "1. >\n" +
                "2. <\n" +
                "3. >=\n" +
                "4. <=\n" +
                "5. %";
            answer = ReadInt(mainMessage, "Неверный ввод! Число от 1 до 5: ", 1, 5);
            var element = ReadInt("Введите элемент, сопутствующий знаку", "Неверный ввод! Введите число:");
            switch (answer)
            {
                case 1:
                    sets[nameOfSet] = BaseSet.Where(x => x > element);
                    break;
                case 2:
                    sets[nameOfSet] = BaseSet.Where(x => x < element);
                    break;
                case 3:
                    sets[nameOfSet] = BaseSet.Where(x => x >= element);
                    break;
                case 4:
                    sets[nameOfSet] = BaseSet.Where(x => x <= element);
                    break;
                case 5:
                    var ost = ReadInt($"Введите чему должен быть равен остаток от деления элементов множества на {element} ",
                        "Неверный ввод! Введите число!");
                    sets[nameOfSet] = BaseSet.Where(x => x % element == ost);
                    break;
            }
        }

        public static void CreateSet()
        {
            string nameOfSet = ChooseSet();
            var mainMessage = "Выберите способ заполнения:\n" +
                "1. Случайное заполнение\n" +
                "2. Ручное заполнение\n" +
                "3. По условию";
            var answer = ReadInt(mainMessage, "Неверный ввод! Введите целое число от 1 до 3:", 1, 3);
            switch (answer)
            {
                case 1:
                    CreateSetRandom(nameOfSet);
                    break;
                case 2:
                    CreateSetManually(nameOfSet);
                    break;
                case 3:
                    CreateSetCondition(nameOfSet);
                    break;
            }
        }

        public static void WriteSet()
        {
            var nameOfSet = ChooseExistingSet();
            if (sets.ContainsKey(nameOfSet))
            {
                sets[nameOfSet].WriteInformation();
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
            foreach (var i in sets.Keys)
            {
                Console.Write($"{i}: " );
                sets[i].WriteInformation();
            }
            Console.WriteLine("Нажмите Enter для выхода в меню");
            Console.ReadLine();
        }

        public static void CalculateValue()//Вычислить выражение
        {
            Console.WriteLine("Выбор двух множест для операции:");
            var a = ChooseExistingSet();
            var b = ChooseExistingSet();
            Console.WriteLine("Выбор множества, куда будет записан результат:");
            var result = ChooseSet();
            var mainMessage = "Введите номер команды, c операцией которую вы хотите использовать\n" +
                "1. +\n" +
                "2. -\n" +
                "3. *\n" +
                "4. Симметричная разность\n";
            var answer = ReadInt(mainMessage, "Неверный ввод! Число от 1 до 4: ", 1, 4);
            try
            {
                switch (answer)
                {
                    case 1:
                        sets[result] = sets[a] + sets[b];
                        break;
                    case 2:
                        sets[result] = sets[a] - sets[b];
                        break;
                    case 3:
                        sets[result] = sets[a].Cross(sets[b]);
                        break;
                    case 4:
                        sets[result] = sets[a].CalculateSymmetricDif(sets[b]);
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Нажмите Enter для выхода в меню");
                Console.ReadLine();
            }
        }

        public static void ChangeUniverse()
        {
            var nameOfSet = ChooseExistingSet();
            int begin = ReadInt("Введите начальную границу:", "Неверный ввод! Введите целое число:");
            int end = ReadInt("Введите конечную границу:", "Неверный ввод! Введите конечную границу:");
            try
            {
                sets[nameOfSet].SetUniverse(begin, end);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Нажмите Enter для продолжения");
                Console.ReadLine();
            }
        }
    }
}