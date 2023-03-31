using TaskApp.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.IO;
using System.Text.Json;
using Autofac.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System;
using TaskApp.WorkerService.Core.Extensions;
using System.Reflection;
using TaskApp.Infrastructure.Modules;
using TaskApp.Application.Commands.Register;
using TaskApp.Application.Commands.Task;
using TaskApp.Infrastructure.MongoDataAccess.Queries;
using TaskApp.Application.Queries;
using TaskApp.Infrastructure.MongoDataAccess;
using MongoDB.Driver.Core.Configuration;
using MassTransit;

namespace TaskApp.WebApi
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {                    
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddHealthChecks();
            
            /*services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
                options.Filters.Add(typeof(ValidateModelAttribute));
            }).AddControllersAsServices();
            */ 
            
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
                options.Filters.Add(typeof(ValidateModelAttribute));
            });
            
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.UseInlineDefinitionsForEnums();

                options.IncludeXmlComments(
                    Path.ChangeExtension(
                        typeof(Startup).Assembly.Location,
                        "xml"));

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = Configuration["App:Title"],
                    Version = Configuration["App:Version"],
                    Description = Configuration["App:Description"]//,
                    //TermsOfService = new System.Uri(Configuration["App:TermsOfService"])
                });

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddMassTransitExtension(Configuration);            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ConfigurationModule(Configuration));
            builder.RegisterModule(new WebApiModule());
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterType<RegisterUseCase>().As<IRegisterUseCase>();
            builder.RegisterType<TaskUseCase>().As<ITaskUseCase>();
            builder.RegisterType<TaskQueries>().As<ITaskQueries>();
            builder.RegisterType<Context>()
                .As<Context>()
                .WithParameter("connectionString", Configuration.GetConnectionString("MongoDb"))
                .WithParameter("databaseName", Configuration.GetSection("MongoDB").GetValue<string>("DatabaseName"))
                .SingleInstance();
            //builder.RegisterAssemblyTypes(Assembly.Load("TaskApp.Infrastructure")).AsImplementedInterfaces();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHealthChecks("/status-text");
            app.UseHealthChecks("/status-json",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                statusApplication = report.Status.ToString(),
                            });

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });

            app.UseCors("CorsPolicy");
            
            app.UseRouting();
            //app.UseAuthorization();
            app.UseHttpsRedirection();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });          
            

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks API V1");
               });            
        }
    }
}
