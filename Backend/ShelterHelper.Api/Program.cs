using ShelterHelper.Api.Authorization;
using ShelterHelper.Api.Infrastructure;
using ShelterHelper.Api.Web;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseExceptionFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DevelopmentCorsPolicy",
        policy =>
        {
            policy.WithOrigins("http://frontend:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();

// Add Swagger with Bearer Configuration
builder.Services.AddSwaggerWithBearerConfig();

// Add Authentication configuration
builder.AddAuthenticationAndAuthorization();

// Add Database Context
builder.AddShelterHelperDbContext();

// Add MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add Repositories
builder.Services.AddShelterHelperAggregateRepositories();

// Add Api Features Handlers
builder.Services.AddApiFeaturesHandlers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else
{
    app.UseHttpsRedirection();
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCorsPolicy");
}

app.UseAuthenticationAndAuthorization();

app.UseHttpLogging();

app.MapControllers();

app.Run();