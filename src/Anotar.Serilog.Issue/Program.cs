using Microsoft.Extensions.Configuration;
using Serilog;

// Create empty configuration
var config = new ConfigurationBuilder().Build();

// Initialize Logging
new LoggerConfiguration()
    .ReadFrom.Configuration(config, "Serilog", new DependencyContextFilter(Microsoft.Extensions.DependencyModel.DependencyContext.Default));

// Custom DependencyContext without Anotar.Serilog.Fody
class DependencyContextFilter : Microsoft.Extensions.DependencyModel.DependencyContext
{
    public DependencyContextFilter(Microsoft.Extensions.DependencyModel.DependencyContext other) : 
        base(other.Target, other.CompilationOptions, other.CompileLibraries, other.RuntimeLibraries.Where(rl => rl.Name != "Anotar.Serilog.Fody"), other.RuntimeGraph)
    {
    }
}