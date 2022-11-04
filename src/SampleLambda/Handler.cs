using Amazon.Lambda.Core;

using Sample.Domain.Model;

namespace Sample;

public static class Handler
{
    public static Casing Case(string input, ILambdaContext context)
    {
        return new Casing(input.ToLower(), input.ToUpper());
    }
}