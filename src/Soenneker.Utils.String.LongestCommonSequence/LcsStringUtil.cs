using System;
using System.Buffers;
using System.Diagnostics.Contracts;

namespace Soenneker.Utils.String.LongestCommonSequence;

/// <summary>
/// Compares strings using the longest common subsequence algorithm.
/// </summary>
public static class LcsStringUtil
{
    /// <summary>
    /// Calculates the final longest-common-subsequence dynamic-programming row for all prefixes of the second string.
    /// </summary>
    /// <param name="str1">The first string.</param>
    /// <param name="str2">The second string.</param>
    /// <returns>An array whose element at index <c>j</c> is the LCS length between <paramref name="str1"/> and the first <c>j</c> characters of <paramref name="str2"/>.</returns>
    [Pure]
    public static int[] CalculateSimilarityArray(string str1, string str2)
    {
        int m = str1.Length;
        int n = str2.Length;

        var lcsRow = new int[n + 1];

        if (m != 0 && n != 0)
            FillRow(str1, str2, lcsRow);

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

        ReadOnlySpan<char> rows = first;
        ReadOnlySpan<char> columns = second;
        int prefix = rows.CommonPrefixLength(columns);
        rows = rows[prefix..];
        columns = columns[prefix..];
        int suffix = 0;
        while (suffix < columns.Length && rows[^(suffix + 1)] == columns[^(suffix + 1)])
            suffix++;

        columns = columns[..(columns.Length - suffix)];
        if (columns.IsEmpty)
            return prefix + suffix;

        rows = rows[..(rows.Length - suffix)];
        int rowLength = columns.Length + 1;
        int[]? rented = null;
        Span<int> row = rowLength <= 256 ? stackalloc int[rowLength] : (rented = ArrayPool<int>.Shared.Rent(rowLength)).AsSpan(0, rowLength);
        try
        {
            row.Clear();
            FillRow(rows, columns, row);
            return prefix + row[^1] + suffix;
        }
        finally
        {
            if (rented is not null)
                ArrayPool<int>.Shared.Return(rented);
        }
    }

    private static void FillRow(ReadOnlySpan<char> first, ReadOnlySpan<char> second, Span<int> row)
    {
        foreach (char character in first)
        {
            int diagonal = 0;
            for (int j = 1; j < row.Length; j++)
            {
                int above = row[j];
                row[j] = character == second[j - 1] ? diagonal + 1 : Math.Max(above, row[j - 1]);
                diagonal = above;
            }
        }
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
