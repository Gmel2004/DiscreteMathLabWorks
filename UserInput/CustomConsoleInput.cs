using System.Drawing;

namespace UserInput
{
    public static class CustomConsoleInput
    {
        public static int ReadInt(string mainMessage, string errorMessage, int begin = int.MinValue, int end = int.MaxValue)
        {
            int answer = 0;
            Console.WriteLine(mainMessage);
            while (!int.TryParse(Console.ReadLine(), out answer) || answer < begin || answer > end)
            {
                Console.WriteLine(errorMessage);
            }
            return answer;
        }
        public static string ReadString(string mainMessage, string errorMessage)
        {
            Console.WriteLine(mainMessage);
            string answer = Console.ReadLine()!;
            while (answer == null)
            {
                Console.WriteLine(errorMessage);
                answer = Console.ReadLine()!;
            }
            return answer;
        }

        public static (int, int) ReadIntPair(string mainMessage,
            string errorMessageFirst, string errorMessageSecond,
            int beginFirst = int.MinValue, int endFirst = int.MaxValue,
            int beginSecond = int.MinValue, int endSecond = int.MaxValue)
        {
            int firstInt = 0, secondInt = 0;
            string[] Input;
            bool isFirstWrong = false;
            bool isSecondWrong = false;

            do
            {
                Console.WriteLine(mainMessage);
                Input = (Console.ReadLine() ?? "").Split().ToArray();
                if (Input.Length != 2)
                {
                    Console.WriteLine("Неверный ввод! Требуется ровно два числа!");
                }
                else
                {
                    isFirstWrong = !int.TryParse(Input[0], out firstInt) || firstInt < beginFirst || firstInt > endFirst;
                    isSecondWrong = !int.TryParse(Input[1], out secondInt) || secondInt < beginSecond || secondInt > endSecond;
                    if (isFirstWrong)
                    {
                        Console.WriteLine(errorMessageFirst);
                    }
                    else if (isSecondWrong)
                    {
                        Console.WriteLine(errorMessageSecond);
                    }
                }
            }
            while (Input.Length != 2 ||
                isFirstWrong ||
                isSecondWrong);

            return (firstInt, secondInt);
        }

        public static double ReadDouble(string mainMessage, string errorMessage,
            double begin = double.MinValue, double end = double.MaxValue)
        {
            double answer = 0;
            Console.WriteLine(mainMessage);
            while (!double.TryParse(Console.ReadLine(), out answer) || answer < begin || answer > end)
            {
                Console.WriteLine(errorMessage);
            }
            return answer;
        }

        public static Color ReadRGB()
        {
            string[] Input;
            var isRGB = false;
            int R = 0, G = 0, B = 0;
            do
            {
                Console.WriteLine("Введите через пробел три компоненты цвета из модели RGB:");
                Input = Console.ReadLine()!.Split();
                if (!(Input.Length == 3 && (isRGB = int.TryParse(Input[0], out R) &
                    int.TryParse(Input[1], out G) &
                    int.TryParse(Input[2], out B))))
                {
                    Console.WriteLine("Неверный ввод! Введите три числа от 0 до 255");
                }
            }
            while (!isRGB);
            return Color.FromArgb(R, G, B);
        }

        public static char ReadChar(string mainMessage, string errorMessage, int begin = 0, int end = 255)
        {
            char answer;
            Console.WriteLine(mainMessage);
            var isCorrect = char.TryParse(Console.ReadLine(), out answer);
            while (!isCorrect || answer < begin || answer > end)
            {
                Console.WriteLine(errorMessage);
                isCorrect = char.TryParse(Console.ReadLine(), out answer);
            }
            return answer;
        }
    }
}