﻿new List<System.Collections.Immutable.ImmutableDictionary<(int,int),int>>(){System.Collections.Immutable.ImmutableDictionary.ToImmutableDictionary(File.ReadAllLines("input.txt").SelectMany((row, irow) => row.Select((col, icol) => (irow, icol, col))).Aggregate((System.Collections.Immutable.ImmutableDictionary.Create<char,int>(), System.Collections.Immutable.ImmutableList.Create<(int,int,int)>()), (curr, x) => (x.col == '.') ? (curr.Item1, curr.Item2.Add((x.irow, x.icol, 0))) : (curr.Item1.ContainsKey(x.col) ? (curr.Item1, curr.Item2.Add((x.irow, x.icol, curr.Item1[x.col]))) : (curr.Item1.Add(x.col, curr.Item1.Append(new KeyValuePair<char, int>(' ', 0)).Max(x => x.Value)+1), curr.Item2.Add((x.irow, x.icol, curr.Item1.Append(new KeyValuePair<char, int>(' ', 0)).Max(x => x.Value)+1))))).Item2.ToDictionary(x => (x.Item1, x.Item2), x => x.Item3))}.Select(dict => (dict, dict.Where(x => x.Value != 0).DistinctBy(x => x.Value).Select(x => x.Value).ToList())).Select(mat => mat.Item2.AsParallel().SelectMany(freq => mat.Item1.Where(x => x.Value == freq).SelectMany(x => mat.Item1.Where(x => x.Value == freq).Where(y => y.Key != x.Key).Select(y => (y.Key, x.Key)).SelectMany(pair => Enumerable.Range(0, Math.Max(mat.dict.Max(x => x.Key.Item1), mat.dict.Max(x => x.Key.Item2))).SelectMany<int,(int,int)>(scalar => [(scalar*(pair.Item1.Item1-pair.Item2.Item1)+pair.Item2.Item1,scalar*(pair.Item1.Item2-pair.Item2.Item2)+pair.Item2.Item2), (-scalar*(pair.Item1.Item1-pair.Item2.Item1)+pair.Item2.Item1,-scalar*(pair.Item1.Item2-pair.Item2.Item2)+pair.Item2.Item2)])).Where(anti => mat.dict.ContainsKey(anti)))).GroupBy(x => new{x.Item1, x.Item2}).Count()).ToList().ForEach(Console.WriteLine);