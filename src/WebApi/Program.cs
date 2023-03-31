using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Saiketsu.Gateway.Application;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Infrastructure.Services;
using Saiketsu.Gateway.WebApi.Security;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

static void InjectSerilog(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());
}

static void AddHttpClients(WebApplicationBuilder builder)
{
    var services = new List<string> { "Party", "Candidate", "User", "Election", "Vote" };

    services.ForEach(service =>
    {
        var serviceName = $"{service}Client";
        var url = $"Services:{service}:Url";

        builder.Services.AddHttpClient(serviceName, httpClient =>
        {
            var address = builder.Configuration[url];
            httpClient.BaseAddress = new Uri(address!);
        });
    });
}

static void AddSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Gateway API",
            Description = ".NET Web API for aggregating services."
        });

        options.EnableAnnotations();
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Gateway.Application.xml"));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Gateway.Domain.xml"));
    });
}

static void AddAuth0(WebApplicationBuilder builder)
{
    var domain = builder.Configuration["Auth0:Domain"] + "/";
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = builder.Configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("read:users",
            policy => policy.Requirements.Add(new HasScopeRequirement("read:users", domain!)));
    });

    builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
    builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();

    AddHttpClients(builder);
    AddSwagger(builder);
    AddAuth0(builder);

    builder.Services.AddScoped<ICandidateService, CandidateService>();
    builder.Services.AddScoped<IPartyService, PartyService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IElectionService, ElectionService>();
    builder.Services.AddScoped<IVoteService, VoteService>();
}

void AddMiddleware(WebApplication app)
{
    app.UseSerilogRequestLogging();

    app.UseAuthentication();
    app.UseAuthorization();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
}

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    InjectSerilog(builder);
    AddServices(builder);

    var app = builder.Build();

    AddMiddleware(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}