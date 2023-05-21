using Microsoft.Azure.Cosmos;
using MvcCosmosDbSeries.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("CosmosDb");
string database =
    builder.Configuration.GetValue<string>("SeriesCosmosDb:Database");
string container =
    builder.Configuration.GetValue<string>("SeriesCosmosDb:Container");
CosmosClient cosmosClient = new CosmosClient(connectionString);
Container containerCosmos = cosmosClient.GetContainer(database, container);
builder.Services.AddSingleton<CosmosClient>(x => cosmosClient);
builder.Services.AddTransient<Container>(x => containerCosmos);
builder.Services.AddTransient<ServiceCosmosDb>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();