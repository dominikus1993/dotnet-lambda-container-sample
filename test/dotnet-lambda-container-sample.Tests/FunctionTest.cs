using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

namespace dotnet_lambda_container_sample.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new SampleLambda.Function();
        var context = new TestLambdaContext();
        var casing = function.FunctionHandler("hello world", context);

        Assert.Equal("hello world", casing.Lower);
        Assert.Equal("HELLO WORLD", casing.Upper);
    }
}