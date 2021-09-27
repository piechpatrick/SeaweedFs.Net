namespace SeaweedFs.Client.Infrastructure.Builders
{
    internal interface IRequestBuilder<TRequest>
    {
        TRequest Build();
    }
}