namespace MatrixInfo
{
    internal class Program
    {
        public static void Main()
        {
            int[,] matrix = new int[6, 6];
            string rootProject = Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.Parent.FullName;
            string filePath;//Путь к файлу с матрицей
            for (int i = 1; i <= 8; i++)
            {
                if (i != 1)
                {
                    Console.WriteLine('\n');
                }
                filePath = rootProject + $@"\Tests\m{i}.txt";
                matrix = ReadMatrix(filePath);
                WriteInfo(matrix);
            }

        }

        public static int[,] ReadMatrix(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден!");
            }// Проверка наличия файла

            string[] lines = File.ReadAllLines(filePath);//Чтение строк из файла

            const int rowsCount = 6;//количество строк
            const int columnsCount = 6;//Количество столбцов

            int[,] matrix = new int[rowsCount, columnsCount];//Инициализация матрицы

            for (int i = 0; i < rowsCount; i++)
            {
                string[] numbers = lines[i].Split(' ');
                for (int j = 0; j < columnsCount; j++)
                {
                    matrix[i, j] = int.Parse(numbers[j]);
                }
            }//Заполнение матрицы данными из файла

            return matrix;
        }//Прочесть матрицу из файла

        public static void WriteMatrix(int[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (j != 0)
                    {
                        Console.Write(' ');
                    }
                    Console.Write(matrix[i, j]);
                }

                Console.WriteLine();
            }
        }//Вывести матрицу на экран

        public static string CalculateReflexivity(int[,] matrix)
        {
            int[] countOfZeroAndOne = new int[2];

            for (int i = 0; i < matrix.GetLength(0) &&
                (countOfZeroAndOne[0] == 0 || countOfZeroAndOne[1] == 0);
                countOfZeroAndOne[matrix[i, i]]++, i++);

            var result = "Рефлексивна";
            if (countOfZeroAndOne[0] == 6)
            {
                result = "Антирефлексивна";
            }
            else if (countOfZeroAndOne[1] < 6)
            {
                result = "Не рефлексивна и\n" +
                    "не антирефлексивна";
            }
            return result;
        }//Вычислить рефлексивность

        public static string CalculateSymmetry(int[,] matrix)
        {
            var result = "Симметрична";
            int zeroCount = 0;
            int symmetry = 0;
            int asymmetry = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i != j && matrix[i, j] == 1)
                    {
                        if (matrix[i, j] == matrix[j, i])
                        {
                            symmetry++;
                        }
                        else
                        {
                            asymmetry++;
                        }
                    }
                    else if (i == j && matrix[i, j] == 0)
                    {
                        zeroCount++;
                    }
                }
            }
            
            if (asymmetry != 0)
            {
                if (symmetry != 0)
                {
                    result = "Не подходит под условие симметричности";
                }
                else if (zeroCount == 6)
                {
                    result = "Асимметрична";
                }
                else
                {
                    result = "Антисимметрична";
                }
            }
            return result;
        }//Вычислить симметричность

        public static string CalculateTransitivity(int[,] matrix)
        {
            var isTransitivy = true;
            for (int i = 0; i < matrix.GetLength(0) && isTransitivy; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) && isTransitivy; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        List<int> suitable = new List<int>();
                        for (int k = 0; k < matrix.GetLength(1); k++)
                        {
                            if (matrix[j, k] == matrix[i, j])
                            {
                                suitable.Add(k);
                            }
                        }

                        for (int k = 0; k < suitable.Count && isTransitivy; k++)
                        {
                            isTransitivy = matrix[i, suitable[k]] == matrix[i, j];
                        }
                    }
                }
            }
            var result = "Транзитивна";
            if (!isTransitivy)
            {
                result = "Не транзитивна";
            }
            return result;
        }//Вычислить транзитивность

        public static string CalculateСonnectivity(int[,] matrix)
        {
            var isConnectivity = true;
            for (int i = 0; i < matrix.GetLength(0) && isConnectivity; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) && isConnectivity; j++)
                {
                    if (i != j)
                    {
                        isConnectivity = matrix[i, j] == 1 || matrix[j, i] == 1;
                    }
                }
            }
            var result = "Полная";
            if (!isConnectivity)
            {
                result = "Неполная";
            }
            return result;
        }//Вычислить связность

        public static void WriteInfo(int[,] matrix)
        {
            Console.WriteLine("Матрица");
            WriteMatrix(matrix);
            Console.WriteLine();
            Console.WriteLine(CalculateReflexivity(matrix) + '\n' +
                CalculateSymmetry(matrix) + '\n' +
                CalculateTransitivity(matrix) + '\n' +
                CalculateСonnectivity(matrix));
        }//Вывести информацию о матрице на экран
    }
}