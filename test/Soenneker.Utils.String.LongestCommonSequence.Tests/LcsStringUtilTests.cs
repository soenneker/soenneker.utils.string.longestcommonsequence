using FluentAssertions;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Utils.String.LongestCommonSequence.Tests;

[Collection("Collection")]
public class LcsStringUtilTests : FixturedUnitTest
{
    public LcsStringUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Theory]
    [InlineData("", "", 1)]
    [InlineData("abc", "", 0)]
    [InlineData("", "xyz", 0)]
    [InlineData("kitten", "sitting", .5714)]
    [InlineData("kitten", "kitten", 1)]
    [InlineData("abc", "def", 0)]
    [InlineData("abcdef", "abc", .5)]
    [InlineData("abc", "abcd", .75)]
    [InlineData("this is sitting on a porch", "this is sitting", .5769)]
    [InlineData("the cat sat on a hat", "sad on a hat", .55)]
    [InlineData("this is a test", "this is another test", .7)]
    public void CalculateSimilarity_Returns_Correct_Similarity_Score(string str1, string str2, double expectedScore)
    {
        double similarityScore = LcsStringUtil.CalculateSimilarity(str1, str2);

        similarityScore.Should().BeApproximately(expectedScore, 0.001);
    }
}
