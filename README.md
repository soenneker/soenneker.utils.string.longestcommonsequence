[![](https://img.shields.io/nuget/v/soenneker.utils.string.longestcommonsequence.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.longestcommonsequence/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.string.longestcommonsequence/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.string.longestcommonsequence/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.string.longestcommonsequence.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.longestcommonsequence/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.String.LongestCommonSequence
### A utility library for comparing strings via the Longest Common Sequence algorithm

## Installation

```
dotnet add package Soenneker.Utils.String.LongestCommonSequence
```

## Why?

### Clarity in Similarity:
LCS gives a straightforward measure of similarity. The longer the common subsequence, the more similar the sequences.

### Length-Flexible:
No bias for longer or shorter sequences. Focuses on shared elements, not sequence length.

### Meaningful Over Quantity:
Emphasizes the meaning of elements, not just their frequency. Great for identifying shared meaningful content.

### Efficient for Big Data:
Handles large datasets and extensive sequences efficiently. Practical for tasks involving substantial amounts of data.

## Usage

```csharp
var text1 = "This is a test";
var text2 = "This is another test";

double result = LcsStringUtil.CalculateSimilarityPercentage(text1, text2); // 70
```