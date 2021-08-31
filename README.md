# TourmalineCore.Common.Logging package

That library contains extensions and configuration for logging implementation

## Startup.cs

```csharp
public void ConfigureServices(IServiceCollection services) {
    ...
    services.AddTourmalineCoreLogging();
    ...
}

public async void Configure(IApplicationBuilder app, IHostingEnvironment env) {
    ...
    app.UseTourmalineCoreLogging();
    ...
}

```

## Program.cs
```csharp
public static void Main(string[] args) {
    ...
    Log.Logger = TourmalineCoreSerilogConfiguration.CreateBasicLogger();
    ...
    BuildWebHost(args).Run();
    ...
}

private static IWebHost BuildWebHost(string[] args) {
    return Host.CreateDefaultBuilder(args)
        .UseTourmalineCoreLogging("app-name")
        ...
        .UseStartup<Startup>()
        .Build();
}

```