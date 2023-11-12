using EfCoreSamplesApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main( string[] args )
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder( args );

        builder.Services.AddScoped<Application>();

        // string connectionString = $"Server=(localdb)\\mssqllocaldb; Database=EfCoreSamplesApp; Trusted_Connection=True; MultipleActiveResultSets=true";
        string connectionString = $"Server=DESKTOP-HOME; Database=EfCoreSamplesApp; Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate=True;";


        //builder.Services.AddDbContextPool<AppDbContext>( options =>
        //builder.Services.AddDbContext<Blog_OneToMany_BasicDbContext>( options =>

        using IHost host = builder.Build();
        Application application = host.Services.GetRequiredService<Application>();

        await application.RunAsync();

        Console.WriteLine( "End" );
    }
}
