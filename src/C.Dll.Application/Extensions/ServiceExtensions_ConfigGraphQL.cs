using Application.GraphQl;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationWebMVC.Extensions
{
  public static partial class ServiceExtentions
  {
    public static IServiceCollection ConfigGraphQL(this IServiceCollection services)
    {
   
      services.AddGraphQLServer()
          .AddQueryType<BaseQueryType>()
          .AddTypeExtension<EmployeeQueryType>()
          .AddTypeExtension<AccessControlQueryType>()
          .AddMutationType<BaseMutationType>()
          .AddTypeExtension<EmployeeMutations>()         
          .AddTypeExtension<AccessControlMutation>() 
          .AddSubscriptionType<BaseSubscriptionType>() //together with app.UseWebSockets();
          .AddTypeExtension<EmployeeSubscriptionType>()
          .AddProjections()
          .AddFiltering()
          .AddSorting();
      
      services.AddInMemorySubscriptions();      

       return services;
    }
  }
}