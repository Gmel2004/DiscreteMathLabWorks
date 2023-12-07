namespace _3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string functionVector = Console.ReadLine()!;

            var initialConjunctions = GetTrueString(functionVector);

            var columnsTable = initialConjunctions.ToDictionary(t => t, t => new HashSet<Conjunction>());
            var rowsTable = incapableAbsorptionConjunctions.ToDictionary(t => t, t => new HashSet<Conjunction>());

            FillImlicateMatrix(columnsTable, rowsTable, initialConjunctions, incapableAbsorptionConjunctions);

            var core = new HashSet<Conjunction>();

            ExtractCore(core, columnsTable, rowsTable, initialConjunctions);

            var other = FindMinNotCoreConjunction(rowsTable, columnsTable.Keys.ToHashSet());
            var result = core.Union(other);


            return result
                .Select(t => t.ToString())
                .ToList();

            Console.WriteLine($"\nF(a, b, c, d) = {string.Join(" \\/ ", MinimalDisjunctiveNormalFormCreator.CreateMDNF("1000100000111111"))}");
        }

        private static void ExtractCore(
            HashSet<Conjunction> core,
            Dictionary<Conjunction, HashSet<Conjunction>> columnsTable,
            Dictionary<Conjunction, HashSet<Conjunction>> rowsTable,
            HashSet<Conjunction> initialConjunctions)
        {
            foreach (var conjunction in initialConjunctions)
            {
                if (columnsTable.ContainsKey(conjunction) && columnsTable[conjunction].Count == 1)
                {
                    var coreConjunction = columnsTable[conjunction].First();
                    core.Add(coreConjunction);
                    var coreRow = rowsTable[coreConjunction];
                    rowsTable.Remove(coreConjunction);
                    foreach (var row in rowsTable)
                    {
                        row.Value.ExceptWith(coreRow);
                    }

                    foreach (var column in coreRow)
                    {
                        columnsTable.Remove(column);
                    }
                }
            }
        }

        private static void FillImplicateMatrix(
            Dictionary<Conjunction, HashSet<Conjunction>> columnsTable,
            Dictionary<Conjunction, HashSet<Conjunction>> rowsTable,
            HashSet<Conjunction> initialConjunctions,
            HashSet<Conjunction> incapableAbsorptionConjunctions)
        {
            foreach (var conjunctionA in initialConjunctions)
            {
                foreach (var conjunctionB in incapableAbsorptionConjunctions)
                {
                    if (conjunctionB.All(t => conjunctionA.Contains(t)))
                    {
                        columnsTable[conjunctionA].Add(conjunctionB);
                        rowsTable[conjunctionB].Add(conjunctionA);
                    }
                }
            }
        }

        private static List<Conjunction> GetTrueString(string functionVector)
        {
            var result = new List<Conjunction>();
            for (var i = 0; i < functionVector.Length; i++)
            {
                if (functionVector[i] == '1')
                {
                    result.Add(new(Convert.ToString(i, 2).PadLeft(4, '0')));
                }
            }

            return result;
        }

        private static void Show(string functionVector)
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

            for (var i = 0; i < base4.Length; i++)
            {
                Console.WriteLine($"{base4[i]} + {functionVector[i]}");
            }
            //Add other notes
        }

        private static List<string> StartBonding(List<string> relevant)
        {
            var result = ExecuteBonding(relevant, 3);

            return result.Item1.Concat(result.Item2).ToList();
        }

        private static (List<string>, List<string>) ExecuteBonding(List<string> relevant, int countCommon)//Добавить разделение на группы
        {
            List<string> outsiders = new();
            bool[] used = new bool[relevant.Count];
            List<string> newRelevant = new();

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
                newRelevant = nextBonding.Item1;
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

        private static HashSet<Conjunction> FindMinNotCoreConjunction(
                    Dictionary<Conjunction, HashSet<Conjunction>> rowsTable,
                    HashSet<Conjunction> columns)
        {
            var result = new HashSet<Conjunction>(columns);
            FindMinNotCoreConjunction(result, new(), rowsTable, rowsTable.Keys.ToList(), columns.Count, 0);
            return result;
        }

        private static void FindMinNotCoreConjunction(
            HashSet<Conjunction> best,
            HashSet<Conjunction> common,
            Dictionary<Conjunction, HashSet<Conjunction>> rowsTable,
            List<Conjunction> rows,
            int columnsCount,
            int rowIndex)
        {
            if (rowIndex == rowsTable.Count)
            {
                var isCovering = common.Select(t => rowsTable[t]).Distinct().Count() == columnsCount;
                if (isCovering && (common.Count < best.Count || common.Count == best.Count && common.Sum(t => t.Count) < best.Sum(t => t.Count)))
                {
                    best.Clear();
                    foreach (var conjunction in common)
                    {
                        best.Add(conjunction);
                    }
                }
                return;
            }

            FindMinNotCoreConjunction(best, common, rowsTable, rows, columnsCount, rowIndex + 1);
            common.Add(rows[rowIndex]);
            FindMinNotCoreConjunction(best, common, rowsTable, rows, columnsCount, rowIndex + 1);
            common.Remove(rows[rowIndex]);
        }
    }
}
}