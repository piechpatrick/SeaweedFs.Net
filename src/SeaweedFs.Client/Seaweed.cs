using SeaweedFs.Client.Core;
using SeaweedFs.Client.Core.Master;

namespace SeaweedFs.Client
{
    internal class Seaweed : ISeaweed
    {
        private Seaweed()
        {

        }

        public Seaweed(IMaster masterClient, IFiler filerClient)
        {
            MasterClient = masterClient;
            FilerClient = filerClient;
        }
        public IMaster MasterClient { get; }
        public IFiler FilerClient { get; }
    }
}