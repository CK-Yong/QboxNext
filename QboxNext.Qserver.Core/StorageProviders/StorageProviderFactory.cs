using Microsoft.Extensions.DependencyInjection;
using QboxNext.Qserver.Core.Exceptions;
using System;
using Microsoft.Extensions.Logging;

namespace QboxNext.Qserver.StorageProviders
{
    /// <summary>
    /// Factory to create a specific <see cref="IStorageProvider"/>.
    /// </summary>
    internal class StorageProviderFactory : IStorageProviderFactory
    {
        private ILogger<StorageProviderFactory> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Type _storageProviderType;

        public StorageProviderFactory(IServiceProvider serviceProvider, Type storageProviderType)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _storageProviderType = storageProviderType ?? throw new ArgumentNullException(nameof(storageProviderType));
        }

        public IStorageProvider GetStorageProvider(StorageProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                return (IStorageProvider)ActivatorUtilities.CreateInstance(_serviceProvider, _storageProviderType, context);
            }
            catch (InvalidOperationException ex)
            {
                _logger = _logger ?? _serviceProvider.GetRequiredService<ILogger<StorageProviderFactory>>();
                _logger.LogError(ex, "Unable to create storage provider {StorageProviderType}.", _storageProviderType.FullName);
                throw new StorageException($"Unable to create storage provider {_storageProviderType.FullName}.", ex);
            }
        }
    }
}
