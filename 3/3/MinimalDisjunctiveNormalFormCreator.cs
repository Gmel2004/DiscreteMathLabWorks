using System.Collections;
using System.Data;

namespace _3
{
	// Теория: https://neerc.ifmo.ru/wiki/index.php?title=%D0%A1%D0%BE%D0%BA%D1%80%D0%B0%D1%89%D1%91%D0%BD%D0%BD%D0%B0%D1%8F_%D0%B8_%D0%BC%D0%B8%D0%BD%D0%B8%D0%BC%D0%B0%D0%BB%D1%8C%D0%BD%D0%B0%D1%8F_%D0%94%D0%9D%D0%A4

	public static class MinimalDisjunctiveNormalFormCreator
	{
		public static List<string> CreateMDNF(string functionVector)
		{
			var log2VectorLength = Math.Log2(functionVector.Length);
			var parametersCount = (int)log2VectorLength;
			if (log2VectorLength - parametersCount != 0)
			{
				throw new ArgumentException();
			}


			var initialConjunctions = GetTrueString(functionVector, parametersCount);
			var conjunctions = new HashSet<Conjunction>(initialConjunctions);
			var incapableAbsorptionConjunctions = new HashSet<Conjunction>();
			var absorbedConjunctions = new HashSet<Conjunction>();

			while (true)
			{
				while (conjunctions.Count > 0)
				{
					var isAbsorbed = false;
					var conjunctionA = conjunctions.First();
					foreach (var conjunctionB in conjunctions.Skip(1))
					{
						var absorbedConjunction = new Conjunction(conjunctionA.Intersect(conjunctionB)); // Можно быстрее если сделать на HashSet

						if (absorbedConjunction.Count > 1 && absorbedConjunction.Count == conjunctionA.Count - 1)
						{
							isAbsorbed = true;
							absorbedConjunctions.Add(absorbedConjunction);
							conjunctions.Remove(conjunctionB);
							break;
						}
					}

					if (!isAbsorbed)
					{
						incapableAbsorptionConjunctions.Add(conjunctionA);
					}

					conjunctions.Remove(conjunctionA);
				}

				if (absorbedConjunctions.Count == 0) break;

				conjunctions = absorbedConjunctions;
				absorbedConjunctions = new();
			} // все в incapableAbsorptionConjunctions

			var columnsTable = initialConjunctions.ToDictionary(t => t, t => new HashSet<Conjunction>());
			var rowsTable = incapableAbsorptionConjunctions.ToDictionary(t => t, t => new HashSet<Conjunction>());

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

			var core = new HashSet<Conjunction>();
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

			var other = FindMinNotCoreConjunction(rowsTable, columnsTable.Keys.ToHashSet());
			var result = core.Union(other);


			return result
				.Select(t => t.ToString())
				.ToList();
		}

		private static List<Conjunction> GetTrueString(string functionVector, int parametersCount)
		{
			var result = new List<Conjunction>();
			for (var i = 0; i < functionVector.Length; i++)
			{
				if (functionVector[i] == '1')
				{
					result.Add(new(Convert.ToString(i, 2).PadLeft(parametersCount, '0')));
				}
			}

			return result;
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
