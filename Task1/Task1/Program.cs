namespace Task1 {
    internal class Program
    {
        static void Main()
        {
            //Задание множеств:
            MySet A = new MySet().CreateRandom(5);//Случайное заполнение

            MySet B = new MySet().FullSet().//По условию: знак, чётность/нечётность, кратность
                Where(x => x > 0 && x % 2 == 0 && x % 6 != 0 && 48 % x == 0);

            MySet C = new MySet();//Ручное заполнение
            C.Add(1);
            C.Add(1);//Проверка на устронение повторов
            C.Add(16);

            //Операции:
            MySet D = C.Cross(B);//Пересечение
            MySet E = A + B;//Объединение
            MySet F = E - A;//Разность
            MySet G = C.CalculateSymmetricDif(B);//Симметрическая разность
            MySet H = new MySet().FullSet().CalculateAddition();//Отрицание

            //Вывод множеств:
            A.Write();
            B.Write();
            C.Write();
            D.Write();
            E.Write();
            F.Write();
            G.Write();
            H.Write();

            //var answer = 0;
            //var dictionaryOfSets = new Dictionary<string, MySet>();
            //while (answer != 5)
            //{
            //    Console.Write("Введите номер команды:/n" +
            //        "1. Создать множество" +
            //        "2. Вывести множество" +
            //        "3. Вывести все множества" +
            //        "4. Решить пример" +
            //        "5. Выход");
            //    var isCorrect = int.TryParse(Console.ReadLine(), out answer);
            //    while (!isCorrect || answer < 0 || answer > 5)
            //    {
            //        Console.WriteLine("Неверный ввод! Введите целое число от 0 до 5: ");
            //        isCorrect = int.TryParse(Console.ReadLine(), out answer);
            //    }
            //    if (answer == 1)
            //    {
            //        Console.Write("Введите имя множества: ");
            //        var name = Console.ReadLine();
            //        Console.WriteLine("Выберите способ заполнения множества:" +
            //            "1. Оставить пустым" +
            //            "2. Назначить универсумом" +
            //            "3. Случайное заполнение" +
            //            "4. Заполнить по условию" +
            //            "5. Ручное заполнение");
            //        isCorrect = int.TryParse(Console.ReadLine(), out answer);//обернуть в функцию
            //        while (!isCorrect || answer < 0 || answer > 5)
            //        {
            //            Console.WriteLine("Неверный ввод! Введите целое число от 0 до 5: ");
            //            isCorrect = int.TryParse(Console.ReadLine(), out answer);
            //        }
            //        if ()

            //    }
            //}
        }
    }
}