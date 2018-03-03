using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static Dictionary<string, int> intersectLengths = new Dictionary<string, int>();
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        string[] words = new string[N];
        for (int i = 0; i < N; i++)
        {
            words[i] = Console.ReadLine();
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i == j)
                {
                    continue;
                }
                int length = CalculateIntersectLength(words[i], words[j]);
                intersectLengths.Add(i.ToString() + j.ToString(), length);
            }
        }

        IEnumerable<string> permutations = GetPermutations(Enumerable.Range(0, N), N);
        List<int> results = new List<int>();
        foreach (string per in permutations)
        {
            int total = 0;
            for (int i = 0; i < per.Length - 1; i++)
            {
                total += intersectLengths[per.Substring(i, 2)];
            }
            results.Add(total);
        }

        Console.WriteLine((words.Sum(y => y.Length) - results.Max()).ToString());
    }

    static IEnumerable<string> GetPermutations(IEnumerable<int> list, int length)
    {
        if (length == 1) return list.Select(t => string.Join("", t));

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e.ToString())),
                (t1, t2) => t1 + (string.Join("", t2)));
    }

    static int CalculateIntersectLength(string s1, string s2)
    {
        int index1 = 0;
        int index2 = 0;

        int intersectLength = 1;
        int maxLength = 0;
        while (index1 + intersectLength <= s1.Length && intersectLength <= s2.Length)
        {
            if (s1.Substring(index1, intersectLength) == s2.Substring(index2, intersectLength))
            {
                if (intersectLength > maxLength)
                {
                    maxLength = intersectLength;
                }
                intersectLength++;
            }
            else
            {
                index1++;
                intersectLength = 1;
                index2 = 0;
                maxLength = 0;
            }
        }
        return maxLength;
    }
}
