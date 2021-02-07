
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.GraphQL;
using Platform.GraphQL.EnumTypes;
using Platform.GraphQL.InputTypes;
using Platform.GraphQL.Persistence.Repositories;
using Platform.GraphQL.Repositories;
using Platform.GraphQL.Services;
using Platform.GraphQL.Types;
using Platform.GraphQL.ValidationRules;
using System.Collections.Generic;

namespace Platform.GatewayAPI
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IContactService,  ContactService>();
            services.AddSingleton<IContactRepository>(new ContactRepository("http://localhost:58118/api/"));

            // Schema
            services.AddSingleton<ISchema, PlatformSchema>();
            services.AddSingleton<PlatformQuery>();
            services.AddSingleton<PlatformMutation>();
            // Enums
            services.AddSingleton<CountryEnum>();
            // Mutations
            services.AddSingleton<CreateContactInputType>();
            services.AddSingleton<AddressInputType>();
            services.AddSingleton<NameInputType>();
            // Query
            services.AddSingleton<ContactType>();
            services.AddSingleton<CountryType>();

            // Validation Rule
            services.AddSingleton<IEnumerable<IValidationRule>>(new IValidationRule[] {
                new EmailAddressValidationRule(),
                new NonEmptyStringAndWhiteSpaceValidationRule(),
                new NonEmptyStringValidationRule(),
            }); 

            services.AddLogging(builder => builder.AddConsole());
            services.AddHttpContextAccessor();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
            })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
            .AddSystemTextJson()
            .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });

        }


      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // add http for Schema at default url /graphql
            app.UseGraphQL<ISchema>();

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground();
        }
    }
}
