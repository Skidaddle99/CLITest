using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace CLITestCliFx;

public static class Program
{
    public static async Task<int> Main()
    {
        return await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
    }
}

[Command("install")]
public class LogCommand : ICommand
{
    // Order: 0
    [CommandParameter(0, Description = "Connection string for the database.")]
    public string ConnectionString { get; set; }

    // Order: 1
    [CommandParameter(1, Description = "Path to sitecore.")]
    public string SitecorePath { get; set; }

    // Name: --backup
    // Short name: -b
    [CommandOption("backup", 'b', Description = "Do a backup of the current database.")]
    public bool BackupDatabase { get; set; } = false;

    // Name: --upgradedb
    // Short name: -u
    [CommandOption("upgradedb", 'u', Description = "Upgrade the database.")]
    public bool UpgradeDatabase { get; set; } = false;

    public ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine("Installing..");
        if (BackupDatabase) console.Output.WriteLine("Backing up database..");
        if (UpgradeDatabase) console.Output.WriteLine("Upgrading database..");

        return default;
    }
}

[Command("db")]
public class DbCommand : ICommand
{
    // Order: 0
    [CommandParameter(0, Description = "Connection string for the database.")]
    public string ConnectionString { get; set; }

    // Name: --backup
    // Short name: -b
    [CommandOption("backup", 'b', Description = "Do a backup of the current database.")]
    public bool BackupDatabase { get; set; } = false;

    // Name: --upgradedb
    // Short name: -u
    [CommandOption("upgradedb", 'u', Description = "Upgrade the database.")]
    public bool UpgradeDatabase { get; set; } = false;

    public ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine("Installing..");
        if (BackupDatabase) console.Output.WriteLine("Backing up database..");
        if (UpgradeDatabase) console.Output.WriteLine("Upgrading database..");

        return default;
    }
}

// Child of db command
[Command("db upgrade")]
public class DbUpgrade : ICommand
{
    public ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine("Upgrading database..");

        return default;
    }
}

// Child of db command
[Command("db backup")]
public class DbBackup : ICommand
{
    public ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine("Backing up database..");

        return default;
    }
}