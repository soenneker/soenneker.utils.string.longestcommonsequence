using AwesomeAssertions;
using Soenneker.Tests.HostedUnit;


namespace Soenneker.Utils.String.LongestCommonSequence.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class LcsStringUtilTests : HostedUnitTest
{
    public LcsStringUtilTests(Host host) : base(host)
    {
    }

    [Test]
    [Arguments("", "", 1)]
    [Arguments("abc", "", 0)]
    [Arguments("", "xyz", 0)]
    [Arguments("kitten", "sitting", .5714)]
    [Arguments("kitten", "kitten", 1)]
    [Arguments("abc", "def", 0)]
    [Arguments("abcdef", "abc", .5)]
    [Arguments("abc", "abcd", .75)]
    [Arguments("this is sitting on a porch", "this is sitting", .5769)]
    [Arguments("the cat sat on a hat", "sad on a hat", .55)]
    [Arguments("this is a test", "this is another test", .7)]
    public void CalculateSimilarity_Returns_Correct_Similarity_Score(string str1, string str2, double expectedScore)
    {
        double similarityScore = LcsStringUtil.CalculateSimilarity(str1, str2);

        similarityScore.Should().BeApproximately(expectedScore, 0.001);
    }
}

