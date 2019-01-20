namespace QboxNext.Qserver.StorageProviders
{
    public interface IStorageProviderFactory
    {
        /// <summary>
        /// Creates a storage provider.
        /// </summary>
        IStorageProvider GetStorageProvider(StorageProviderContext context);
    }
}
