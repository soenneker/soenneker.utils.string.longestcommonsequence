using System;
using System.Buffers;
using System.Diagnostics.Contracts;

namespace Soenneker.Utils.String.LongestCommonSequence;

/// <summary>
/// A utility library for comparing strings via the Longest Common Sequence algorithm
/// </summary>
public static class LcsStringUtil
{
    /// <summary>
    /// Calculates the similarity array between two strings.
    /// </summary>
    /// <param name="str1">The first string.</param>
    /// <param name="str2">The second string.</param>
    /// <returns>An array representing the similarity between the two strings.</returns>
    [Pure]
    public static int[] CalculateSimilarityArray(string str1, string str2)
    {
        int m = str1.Length;
        int n = str2.Length;

        var lcsRow = new int[n + 1];

        for (var i = 0; i <= m; i++)
        {
            var prev = 0;

            for (var j = 0; j <= n; j++)
            {
                int current = lcsRow[j];

                if (i > 0 && j > 0)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        lcsRow[j] = prev + 1;
                    }
                    else
                    {
                        lcsRow[j] = Math.Max(lcsRow[j], lcsRow[j - 1]);
                    }
                }
                prev = current;
            }
        }

        return lcsRow;
    }

    /// <summary>
    /// Calculates the similarity between two strings.
    /// </summary>
    /// <param name="s1">The first string.</param>
    /// <param name="s2">The second string.</param>
    /// <returns>The similarity between the two strings as a double value.</returns>
    [Pure]
    public static double CalculateSimilarity(string s1, string s2)
    {
        if (s1 == s2)
            return 1;

        int lcsLength = CalculateLength(s1, s2);

        // Calculate similarity as a percentage
        double similarity = (double)lcsLength / Math.Max(s1.Length, s2.Length);

        return similarity;
    }

    private static int CalculateLength(string first, string second)
    {
        if (second.Length > first.Length)
            (first, second) = (second, first);

        int rowLength = second.Length + 1;
        int[]? rented = null;
        Span<int> row = rowLength <= 256 ? stackalloc int[rowLength] : (rented = ArrayPool<int>.Shared.Rent(rowLength)).AsSpan(0, rowLength);
        row.Clear();

        for (var i = 1; i <= first.Length; i++)
        {
            var previous = 0;

            for (var j = 1; j <= second.Length; j++)
            {
                int current = row[j];
                row[j] = first[i - 1] == second[j - 1] ? previous + 1 : Math.Max(row[j], row[j - 1]);
                previous = current;
            }
        }

        int result = row[second.Length];
        if (rented is not null)
            ArrayPool<int>.Shared.Return(rented);

        return result;
    }

    /// <summary>
    /// Calculates the similarity between two strings as a percentage.
    /// </summary>
    /// <param name="s1">The first string.</param>
    /// <param name="s2">The second string.</param>
    /// <returns>The similarity between the two strings as a percentage.</returns>
    [Pure]
    public static double CalculateSimilarityPercentage(string s1, string s2)
    {
        double similarity = CalculateSimilarity(s1, s2);
        return similarity * 100;
    }
}
