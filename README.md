I kept everything in one solution but obvioulsy this would be seperated. If the Startup is passed over in VS, then hitting run will start all the required services. 

Though please note everything in memory at the moment, so database persitence will be lost if the services are stopped.


There are three services to run and the UI. 

PlatformUI 
- http://localhost:4200/
1. Platform.GatewayAPI
- http://localhost:3000/ui/playground
- http://localhost:3000/ui/voyager (will give a living model of the schema)
2. Contact.Service
- Fire by message only
3. Contact.Projection.API
- http://localhost:58118/swagger/index.html


Didnt managed to get the UI fully done but everything can be done by the gatewayAPI, which uses the GraphQL pattern. If you have used graphQL before it is introspection, so the playground wil have intellisense (hit ctrl+space for hint).

https://graphql.org/learn/

To create a contact you will need to run the following mutation, which the create component would run in the UI. This is where the data validation is mainly done.

1. Write a Contact. This will send a commnand message to the Contact.Service. 
 
mutation {
  createContact(
    createContact: {
      userId: "foo1"
      emailAddress: "foo@foo.com"
      name: { firstName: "Bob", lastName: "Foo" }
      dateOfBirth: "2012-03-03"
      address: {
        houseNameNumber: "33"
        street: "foo street"
        postcode: "FF1 1WZ"
        city: "foo city"
        countryIsoAlpha3: GBR
      }
    }
  )
}

2. Some useful qraphql queries but can be view via the UI. Though I havent setup pub/sub which graphQL also has.


query{
  countries{
    alpha3
    name
  }
}


query{
  contacts{ 
    id: contactId
    userId
    firstName
    lastName
    name
    dateOfBirth
    emailAddress
    address{
      city
      houseNameNumber
      postcode
      street
      country{
        name
      }
    }
  }
}


query {
  contact(id: "5bfbf9c1-11df-46ed-b09c-ad24615206a3") {
    userId
    firstName
    lastName
    emailAddress
    dateOfBirth
  }
}


I've used a in memory database both for my eventstore, projection. The bus is also using a development version and stored locally to disk - ..\Platform\.learningtransport.


I have already writen packages that can replace these in memory infrastures. For example have written commercially  the eventstore, using Greg Young, SQL, MonogDB, and recently for Azure tables and eventHub.


For example could replace the in memory version with Greg Young if you have it installed, then I'm sure my version could be used - https://github.com/cbrown11/DomainRepository.EventStore. 

Just need to change IOC container to use it for the Contact.Service.


        private static IContainer BuildContainer(IConfigurationRoot configuration) => new Container(x => {
            x.For<ITransientDomainEventPublisher>().Use<TransientDomainEventPublisher>();
            //TODO: Change to Greg Young Event Store 
            x.For<IDomainRepository>().Use<InMemEventStoreDomainRespository>().Ctor<string>("catergory").Is("Contact").Singleton();

            x.Scan(y =>
            {
                y.TheCallingAssembly();
                y.WithDefaultConventions();
            });
        }




