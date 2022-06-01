using Sentry;

var  CorsPolicy = "_CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseSentry(o =>
{
    o.Dsn = "https://8f101848308746d6a700df2ea374a813@o1252838.ingest.sentry.io/6419125";
    o.Debug = true;
    o.TracesSampleRate = 1.0;
    o.AttachStacktrace = true;
});

builder.Services.AddCors(opts =>
{
    opts.AddPolicy(name: CorsPolicy, policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSentryTracing();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();