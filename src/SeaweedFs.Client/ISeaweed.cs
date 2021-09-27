using SeaweedFs.Client.Core;
using SeaweedFs.Client.Core.Master;

namespace SeaweedFs.Client
{
    public interface ISeaweed
    {
        IMaster MasterClient { get; }
        IFiler FilerClient { get; }
    }
}