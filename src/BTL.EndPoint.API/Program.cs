
using BTL.Application.Contract.Discounts.Commands;
using BTL.Application.Discounts.CommandHandlers;
using BTL.Domain.Discounts.Contracts;
using BTL.Persistence.EF.Contexts;
using BTL.Persistence.EF.Discounts;
using Framework.Application.CQRS.CommandHandling;
using Framework.Application.CQRS.EventHandling;
using Framework.IdGen;
using IdGen;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Query.Retrieval.Discounts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AddCors(builder.Services, builder.Configuration);
builder.Services.AddSingleton<IIdGenerator, FrameworkIdGenerator>();
builder.Services.AddSingleton((sp) => new IdGenerator(0));

builder.Services.AddScoped<ICommandBus, CommandBus>();
builder.Services.AddScoped<IEventBus, EventBus>();
builder.Services.AddTransient<ICommandHandler<CreateDiscountCommand>, CreateDiscountCommandHandler>();


builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IDiscountReadRepository, DiscountReadRepository>();
AddDbContext(builder);

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{

//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();

void AddDbContext(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddDbContext<DbContext, ApplicationContext>(options =>
    {
        options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("ConnectionString"));


        if (webApplicationBuilder.Environment.IsProduction()) return;

        options.LogTo(message => Console.WriteLine(message));
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
        options.ConfigureWarnings(warningLog =>
        {
            warningLog.Log(CoreEventId.FirstWithoutOrderByAndFilterWarning,
                CoreEventId.RowLimitingOperationWithoutOrderByWarning);
        });
    });

    //webApplicationBuilder.Services.AddDbContext<OrderReadContext>(options =>
    //{
    //    options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("OrderConnectionString"),
    //        sqlOptions => { sqlOptions.MigrationsAssembly(typeof(OrderReadContext).Assembly.FullName); }
    //    );
    //});
}

static void AddCors(IServiceCollection services, IConfiguration configuration)
{
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder
            .SetIsOriginAllowed(o => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            );
    });
    //services.AddCors(delegate (CorsOptions options)
    //{
    //    options.AddPolicy("CorsPolicy", delegate (CorsPolicyBuilder builder)
    //    {
    //        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    //    });
    //    options.AddPolicy("signalr", delegate (CorsPolicyBuilder builder)
    //    {
    //        builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials()
    //            .SetIsOriginAllowed((string _) => true);
    //    });
    //});
}