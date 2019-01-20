using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QboxNext.Core.CommandLine;
using QboxNext.Logging;
using QboxNext.MergeQbx.Utils;
using QboxNext.Qserver.Core.Interfaces;
using QboxNext.Qserver.StorageProviders;
using QboxNext.Qserver.StorageProviders.File;

namespace QboxNext.MergeQbx
{
    class Program
    {
        [Option("", "original", Required = true, HelpText = "Path to original QBX file")]
        public string OriginalQbxPath { get; set; }

        [Option("", "new", Required = true, HelpText = "Path to new QBX file")]
        public string NewQbxPath { get; set; }

        public static IServiceProvider ApplicationServiceProvider { get; set; }

        static void Main(string[] args)
        {
            // Setup static logger factory
            ILoggerFactory loggerFactory = QboxNextLogProvider.LoggerFactory = new LoggerFactory();

            IHost host = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    IHostingEnvironment env = hostingContext.HostingEnvironment;

                    config
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddSingleton(loggerFactory)
                        .AddLogging();

                    services
                        .Configure<kWhStorageOptions>(hostContext.Configuration.GetSection("kWhStorage"))
                        .AddStorageProvider<kWhStorage>();
                })
                .UseConsoleLifetime()
                .Build();

            ApplicationServiceProvider = host.Services;

            var program = new Program();
            var settings = new CommandLineParserSettings { IgnoreUnknownArguments = true, CaseSensitive = false };
            ICommandLineParser parser = new CommandLineParser(settings);
            if (parser.ParseArguments(args, program, System.Console.Error))
            {
                // TODO: further utilize IHost/IHostService
                program.Run();
            }
            else
            {
                Console.WriteLine("Usage: QboxNext.MergeQbx --original=<path to original QBX file> --new=<path to new QBX file>");
            }
        }

        private void Run()
        {
            if (!File.Exists(OriginalQbxPath))
            {
                Console.Error.WriteLine($"Could not find file ${OriginalQbxPath}");
                Environment.Exit(-1);
            }
            if (!File.Exists(NewQbxPath))
            {
                Console.Error.WriteLine($"Could not find file ${NewQbxPath}");
                Environment.Exit(-1);
            }

            using (var originalStorageProvider = GetStorageProviderForPath(OriginalQbxPath))
            {
                using (var newStorageProvider = GetStorageProviderForPath(NewQbxPath))
                {
                    originalStorageProvider.Merge(newStorageProvider);
                }
            }
        }

        private kWhStorage GetStorageProviderForPath(string originalQbxPath)
        {
            var storageProviderContext = new StorageProviderContext
            {
                SerialNumber = QbxPathUtils.GetSerialFromPath(originalQbxPath),
                CounterId = QbxPathUtils.GetCounterIdFromPath(originalQbxPath),
                Precision = Precision.mWh,
                StorageId = QbxPathUtils.GetStorageIdFromPath(originalQbxPath)
            };

            IOptions<kWhStorageOptions> options = ApplicationServiceProvider.GetRequiredService<IOptions<kWhStorageOptions>>() ?? new OptionsWrapper<kWhStorageOptions>(new kWhStorageOptions());

            // Override path.
            options.Value.DataStorePath = QbxPathUtils.GetBaseDirFromPath(originalQbxPath);

            return new kWhStorage(options, storageProviderContext);
        }

    }
}
