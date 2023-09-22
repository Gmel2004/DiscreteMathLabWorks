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
        }
    }
}