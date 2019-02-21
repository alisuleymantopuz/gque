using gque.domain.GraphQL;
using gque.domain.GraphQL.Types;
using gque.domain.infrastructure;
using gque.domain.managers;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace gque.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "graphquery-db"));
            services.AddScoped<ProductReviewManager>();
            
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<ProductGraph>();
            services.AddScoped<ProductTypeGraph>();
            services.AddScoped<ProductReviewGraph>();
            services.AddScoped<ProductReviewInputGraph>();
            services.AddScoped<ProductGraphQuery>();
            services.AddScoped<ProductMutation>();
            services.AddScoped<AppSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = HostingEnvironment.IsDevelopment(); })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(context => context.User)
                .AddDataLoader()
                .AddWebSockets();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.Seed();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseGraphQLWebSockets<AppSchema>("/graphql");
            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseWebSockets();
        }
    }
}
