using Microsoft.Extensions.Configuration;
using Serilog;

// Create empty configuration
var config = new ConfigurationBuilder().Build();

// Initialize Logging
new LoggerConfiguration()
    .ReadFrom.Configuration(config); // This throws the Exception