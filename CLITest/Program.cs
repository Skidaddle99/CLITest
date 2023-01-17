using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace CLITest
{
    public class BaseOptions
    {
        [Option('c', "connectionstring", Required = true, HelpText = "Connection string for the database.")]
        public string ConnectionString { get; set; }
    }

    [Verb("install", HelpText = "Install Ucommerce")]
    public class InstallOptions : BaseOptions
    {
        [Option('s', "sitecorepath", Required = true, HelpText = "Path to sitecore.")]
        public string SitecorePath { get; set; }

        [Option('d', "upgradedatabase", Required = false, HelpText = "Database will be upgraded as well.")]
        public bool UpgradeDatabase { get; set; }

        [Option('b', "backupdatabase", Required = false,
            HelpText = "A backup of the current database will be created.")]
        public bool BackupDatabase { get; set; }

        [Usage(ApplicationAlias = "CLITest")]
        public static IEnumerable<Example> Examples =>
            new List<Example>
            {
                new Example("Install/upgrade Ucommerce",
                    new InstallOptions
                        { ConnectionString = "YourConnectionString", SitecorePath = "YourSitecorePath" }),
                new Example("Install/upgrade Ucommerce with database",
                    new InstallOptions
                    {
                        ConnectionString = "YourConnectionString", SitecorePath = "YourSitecorePath",
                        UpgradeDatabase = true
                    }),
                new Example("Backup database, then  install/upgrade Ucommerce & database",
                    new InstallOptions
                    {
                        ConnectionString = "YourConnectionString", SitecorePath = "YourSitecorePath",
                        UpgradeDatabase = true, BackupDatabase = true
                    })
            };
    }

    [Verb("upgrade", HelpText = "Upgrade Ucommerce")]
    public class UpgradeOptions : InstallOptions
    {
    }

    [Verb("db", HelpText = "Upgrade database")]
    public class DatabaseOptions : BaseOptions
    {
        [Value(1)] public string subcommand { get; set; }

        [Usage(ApplicationAlias = "CLITest")]
        public static IEnumerable<Example> Examples =>
            new List<Example>
            {
                new Example("Upgrade database",
                    new DatabaseOptions { ConnectionString = "YourConnectionString", subcommand = "upgrade" }),
                new Example("Create backup of current database",
                    new DatabaseOptions { subcommand = "backup", ConnectionString = "YourConnectionString" })
            };
    }

    internal class Program
    {
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<InstallOptions, UpgradeOptions, DatabaseOptions>(args)
                .MapResult(
                    (InstallOptions opts) => RunInstall(opts),
                    (UpgradeOptions opts) => RunUpgrade(opts),
                    (DatabaseOptions opts) => RunDatabase(opts),
                    errs => 1);
        }

        private static int RunInstall(InstallOptions opts)
        {
            Console.WriteLine("Installing..");
            return InstallUcommerce(opts);
        }

        private static int RunUpgrade(UpgradeOptions opts)
        {
            Console.WriteLine("Upgrading..");
            return InstallUcommerce(opts);
        }

        private static int InstallUcommerce(InstallOptions opts)
        {
            if (opts.BackupDatabase) Console.WriteLine("Backing up database..");
            if (opts.UpgradeDatabase) Console.WriteLine("Upgrading database..");
            return 0;
        }

        private static int RunDatabase(DatabaseOptions opts)
        {
            if (opts.subcommand == "upgrade")
            {
                Console.WriteLine("upgrading database");
                return 0;
            }

            if (opts.subcommand == "backup")
            {
                Console.WriteLine("backing up database");
                return 0;
            }

            Console.WriteLine("Subcommand needed.\n[EXAMPLES]\ndb upgrade -c connectionstring\ndb backup -c connectionstring");
            return 1;
        }
    }
}