using SeaweedFs.Filer.Store.Catalog;

namespace SeaweedFs.Filer.Store
{
    public interface IFilerStore
    {
        IFilerCatalog GetCatalog(string directory);
    }
}