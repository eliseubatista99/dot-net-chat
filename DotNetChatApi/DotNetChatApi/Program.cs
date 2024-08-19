using DotNetChatApi.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotNetChatApi
{
    public class Program
    {
        const string CORS_POLICY = "LocalHostPolicy";

        DatabaseContext dbContext = null;

        private static void ConfigureDatabase(ref WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DatabaseContext")
                       ?? throw new InvalidOperationException("Connection string 'DatabaseContext' not found.");

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }

        private static void ConfigureAuth(ref WebApplicationBuilder builder)
        {
            var authKey = builder.Configuration.GetSection("AuthKey")?.Value
                        ?? throw new InvalidOperationException("Auth key not found.");

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var key = Encoding.ASCII.GetBytes(authKey);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private static void ConfigureCors(ref WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_POLICY,
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:3000",
                                            "https://localhost:3001",
                                            "http://localhost:3000",
                                            "http://localhost:3001")
                                .AllowAnyHeader();
                    });
            });

        }

        private static void ConfigureSwagger(ref WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static WebApplication ConfigureApp(ref WebApplicationBuilder builder)
        {
            var app = builder.Build();

            app.UseCors(CORS_POLICY);
            var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var dbContext = new DatabaseContext(services.GetRequiredService<DbContextOptions<DatabaseContext>>());
            dbContext.Database.Migrate();

            dbContext.SeedDatabase(services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            ConfigureSwagger(ref builder);
            ConfigureAuth(ref builder);

            ConfigureCors(ref builder);

            ConfigureDatabase(ref builder);

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddSingleton<IDatabaseProvider, DatabaseProvider>();

            var app = ConfigureApp(ref builder);

            app.Run();
        }
    }
}