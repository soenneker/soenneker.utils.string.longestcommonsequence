[![](https://img.shields.io/nuget/v/soenneker.utils.string.longestcommonsequence.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.longestcommonsequence/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.string.longestcommonsequence/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.string.longestcommonsequence/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.string.longestcommonsequence.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.longestcommonsequence/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.string.longestcommonsequence/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.string.longestcommonsequence/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.String.LongestCommonSequence
Longest common subsequence scoring for strings, with normalized and percentage results.

## Installation

```bash
dotnet add package Soenneker.Utils.String.LongestCommonSequence
```

## Usage

```csharp
using Soenneker.Utils.String.LongestCommonSequence;

var text1 = "This is a test";
var text2 = "This is another test";

double score = LcsStringUtil.CalculateSimilarity(text1, text2);
double percentage = LcsStringUtil.CalculateSimilarityPercentage(text1, text2);

// score == 0.7
// percentage == 70
```

The longest common subsequence preserves character order but does not require matching characters to be adjacent. The normalized score is:

```text
LCS length / length of the longer input
```

`CalculateSimilarity` returns a value from `0` to `1`; `CalculateSimilarityPercentage` multiplies it by 100. Identical strings, including two empty strings, return `1` (or `100%`).

## Comparison rules and cost

- Comparison is case-sensitive.
- The algorithm compares UTF-16 code units, not words, Unicode scalar values, or grapheme clusters.
- Whitespace and punctuation participate like any other character.
- Runtime is `O(m × n)` for input lengths `m` and `n`.
- `CalculateSimilarity` uses `O(min(m, n))` working memory.

For long inputs, the quadratic runtime can be significant. Normalize casing or Unicode representation before calling when your application requires those equivalences.

## Prefix lengths

`CalculateSimilarityArray(first, second)` exposes the final dynamic-programming row. Element `j` contains the LCS length between `first` and the first `j` characters of `second`; the returned array therefore has `second.Length + 1` elements. Use its last element for the complete LCS length.

All methods require non-null strings.
