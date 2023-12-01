namespace _3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string vector = Console.ReadLine()!;

            var columns = ParseVector(vector);
            var rows = StartBonding(columns);
            Console.WriteLine($"\nF(a, b, c, d) = {string.Join(" \\/ ", columns)}");
            Console.WriteLine($"\nF(a, b, c, d) = {string.Join(" ", rows)}\n");

            Console.WriteLine(
                string
                .Join(@"/\",
                StartFindMinCoverage(CreateImplicantMatrix(rows, columns))));
        }

        private static List<string> ParseVector(string vector)
        {
            string[] base4 = new string[16] {
                                    "0000",
                                    "0001",
                                    "0010",
                                    "0011",
                                    "0100",
                                    "0101",
                                    "0110",
                                    "0111",
                                    "1000",
                                    "1001",
                                    "1010",
                                    "1011",
                                    "1100",
                                    "1101",
                                    "1110",
                                    "1111"};

            Console.WriteLine("abcd F");

            HashSet<string> relevant = new();
            for (var i = 0; i < vector.Length; i++)
            {
                Console.WriteLine($"{base4[i]} {vector[i]}");

                if (vector[i] == '1')
                {
                    var values = "";
                    for (var j = 0; j < base4[i].Length; j++)
                    {
                        values += base4[i][j] == '1' ? (char)('A' + j) : (char)('a' + j);
                    }
                    relevant.Add(values);
                }
            }

            return relevant.ToList();
        }

        private static List<string> StartBonding(HashSet<string> relevant)
        {
            var result = ExecuteBonding(relevant, 3);

            return result.Item1.Concat(result.Item2).ToList();
        }

        private static (List<string>, List<string>) ExecuteBonding(HashSet<string> relevant, int countCommon)//Добавить разделение на группы
        {
            List<string> outsiders = new();
            bool[] used = new bool[relevant.Count];
            HashSet<string> newRelevant = new();

            for (int i = 0; i < relevant.Count - 1; i++)
            {
                for (int j = i + 1; j < relevant.Count; j++)
                {
                    var common = FindCommon(relevant[i], relevant[j]);

                    if (common.Length == countCommon)
                    {
                        used[i] = used[j] = true;
                        newRelevant.Add(common);
                    }
                }
            }

            for (int i = 0; i < used.Length; i++)
            {
                if (used[i] == false)
                {
                    outsiders.Add(relevant[i]);
                }
            }

            if (newRelevant.Count > 0 && countCommon > 2)
            {
                var nextBonding = ExecuteBonding(newRelevant, countCommon - 1);
                newRelevant = nextBonding.Item1.ToHashSet();
                outsiders = outsiders.Concat(nextBonding.Item2).ToList();
            }

            return (newRelevant.ToList(), outsiders);
        }

        private static string FindCommon(string str1, string str2)
        {
            string common = "";

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] == str2[j])
                    {
                        common += str1[i];
                    }
                }
            }

            return common;
        }

        private static Dictionary<string, List<string>> CreateImplicantMatrix(List<string> rows, List<string> columns)
        {
            Dictionary<string, List<string>> matrix = new();
            for (int i = 0; i < columns.Count; i++)
            {
                for (int j = 0; j < rows.Count; j++)
                {
                    var isContains = true;

                    for (int k = 0; k < rows[j].Length && isContains; k++)
                    {
                        isContains = columns[i].Contains(rows[j][k]);
                    }

                    if (isContains)
                    {
                        if (matrix.ContainsKey(columns[i]))
                        {
                            matrix[columns[i]].Add(rows[j]);
                        }
                        else
                        {
                            matrix[columns[i]] = new() { rows[j] };
                        }
                    }
                }
                Console.WriteLine($"{columns[i]}: {string.Join(" ", matrix[columns[i]])}");
            }

            return matrix;
        }

        private static HashSet<string> StartFindMinCoverage(Dictionary<string, List<string>> matrix)
        {
            var keys = matrix.Keys.ToArray();

            return FindMinCoverage(matrix, keys, 0, new List<string>());
        }

        private static HashSet<string> FindMinCoverage(Dictionary<string, List<string>> matrix, string[] keys, int index, List<string> coverage)
        {
            HashSet<string> Min = new();
            for (int i = 0; i < matrix[keys[index]].Count; i++)
            {
                coverage.Add(matrix[keys[index]][i]);

                if (index < keys.Length - 1)
                {
                    var posibleMin = FindMinCoverage(matrix, keys, index + 1, coverage);

                    if (Min.Count == 0 || posibleMin.Distinct().Count() < Min.Count)
                    {
                        Min = posibleMin.ToHashSet();
                    }
                }
                else
                {
                    if (Min.Count == 0 || coverage.Distinct().Count() < Min.Count)
                    {
                        Min = coverage.ToHashSet();
                    }
                }
                coverage.RemoveAt(coverage.Count - 1);
            }

            return Min;
        }
    }
}