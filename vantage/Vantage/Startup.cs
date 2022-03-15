using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vantage.GraphQL;
using Vantage.GraphQL.Comments;
using Vantage.GraphQL.Users;

namespace Vantage
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public const string ClientCorsPolicy = nameof(ClientCorsPolicy);
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddPooledDbContextFactory<Database>(options =>
                    options.UseSqlServer(_configuration["Connections:DatabaseKey"]))
                .AddCors(setup => setup.AddPolicy(name: ClientCorsPolicy,
                    config => config.WithOrigins("https://localhost:44305")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                ))
                .AddGraphQLServer()
                .AddType<UserType>()
                .AddType<CommentType>()
                .AddType<ReplacementLinkType>()
                .AddFiltering()
                .AddSorting()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions()
                .AddQueryType<Query>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseRouting();
            app.UseCors(ClientCorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGraphQL().RequireCors(ClientCorsPolicy);
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            },"/graphql-voyager");

            

        }
    }
}
