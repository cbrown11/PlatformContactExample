
# Overview

I kept everything in one solution but obvioulsy this would be seperated. If the Startup is passed over in Visual Studio, then runnning the solution will start all the required services and expose their endpoints. 

Though please note everything in memory at the moment, so database persitence will be lost if the services are stopped.

Didnt managed to get the UI fully done but everything can be done by the gatewayAPI, which uses the GraphQL pattern. Hopefully I've implemntated enough to determine what you are looking for and my current skill sets. 

If you haven't used graphQL before it is introspection, so the playground wil have intellisense (hit ctrl+space for a hint).

Documentation on GraphQL: https://graphql.org/learn/

[An architecture overview can be found here](https://github.com/cbrown11/PlatformContactExample/blob/master/PlatformExample.pdf) (grey means its not been implemented).

# Platform Services and UI

There are three services to run and the UI. All services are written in dotnet  version .net core 3.1. 

So if you haven't got Visual studio can run 'dotnet run' in the project or 'dotnet <dll>' on a compiled version ('dotnet build').
  
## PlatformUI 
The Code for the UI is in a different Git Repository `https://github.com/cbrown11/PlatformExampleUI`

Navigate to `http://localhost:4200/`

## Platform.GatewayAPI

Main api is `http://localhost:3000/graphql`. Ive attached playground and voyager to the api so can easily send mutations, query and explorer the schema and its data. 

- For Playground Ground Navigate to `http://localhost:3000/ui/playground`
- For Voyager  Navigate to `http://localhost:3000/ui/voyager`

TESTS: There would be Specflow tests, testing end to end. Sorry I didnt want to expose the package library I've written to convert specflow into a graphQL schema (for Free). 

``` 
cd .\Platform.GatewayAPI\bin\Debug\netcoreapp3.1
dotnet Platform.GatewayAPI.dll
``` 
## Contact.Service

- Endpoint to this service is by a command message. Though could expose the Command as an API call.
- Events will be written to an eventstore and the domain repository will send this events on to the Bus.

``` 
cd .\Contact.Service\bin\Debug\netcoreapp3.1
dotnet Contact.Service.dll
``` 

## Contact.Projection.API

- The service will subscribe and listen to the domain events thats its interested in
- Using NserviceBus learning Transport so will automatically setup the pub/sub. But other transporters will needs to be configured. Having written in the pass so can be done via the config (SQL, RabbitMQ)

Navigate to view sawagger of the API `http://localhost:58118/swagger/index.html`

``` 
cd .\Contact.Projection.API\bin\Debug\netcoreapp3.1
dotnet Contact.Projection.API.dll
```

## Other Services (ie Shop, Company)

- This could follow the contact service. Whether they follow the CQRS, DDD or just a simple CRUD REST service.
- Will have their own end point but can be pulled together by the Platform.GatewayAPI and its GraphQL schema.

### Country Service (instead of Package Library)

I made a country package library which the Platform Schema references. However this could be a simple API in its own right, especially if the data would change and not be static 

This can be changed in the Platform.GatewayAPI, without effecting anything up stream or the other services.

For example the rest service end point could be a simple Rest API.

- http://localhost:{port}/country
- http://localhost:{port}/country/{alphaCode}

## Contact Linking to Other Services Question

### Simple Link - Latest Version all times

If its simple and all they care about is having the latest version then then linking the contactId is sufficient. As the Platform GraphQL can use that id in its resolver to retrieve the contact details. Same way the Address Type works out its Country Type data.

### Complex Link

If another domain area needs a more complex version, for example need to have the latest contact up to a given time then this can be different (ie submitting legal document). Obviously we stored the events so we can still obtain that information from the contact service. Though dont believe this would be the right approach. What if we want to replace the contact service with another service? 

This seperate domain should have its own contact model (or use a shared kernel value object if using DDD). And therefore have its own persitence of it.
1. The data can either be passed or capture from the contact domain service, either from domain peristence or the read model. Depends on the latency but I'm sure the read model would be sufficient in this scenario and most cases.
2. The domain would then keeps its own contact details up to date by subscribing to the contact domain events. (same was as the contact readmodel)
3. In regards to the Platform Schema. Obviously it could have its own model type but I would think contact model type would be the same in most cases. Therefore for that new domain area model type in the GraphQL schema, the Contact Type resolver would only need to be different. And point to the relative new domain source. 

# Playground Examples

To create a contact you will need to run the following mutation, which the create component would run in the UI. This is where the data validation is mainly done, though the command class has its own validation and checks.

- Along with the standard Graphql validation. There are three custom types of validation I created empty string, empty string without white space (userId) and email validation. Validation will be reported in the output response. 
- If the data is an enum then will let you choose an option (ie countryIsoAlpha3)

## Create a Contact Mutation

To create  a Contact, then send a commnand message to the Contact.Service via this mutation: 
``` 
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
```
## Query Platform

Some useful qraphql queries but can be view via the UI. Though I havent setup pub/sub which graphQL also has.

``` 
query{
  countries{
    alpha3
    name
  }
}
``` 
``` 
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
``` 
``` 
query {
  contact(id: "5bfbf9c1-11df-46ed-b09c-ad24615206a3") {
    userId
    firstName
    lastName
    emailAddress
    dateOfBirth
  }
}
``` 

# EventStore In Memory Verions 

I've used an in memory database for both my eventstore and projection. 

The bus is also using a development version and stored locally to disk - ..\Platform\.learningtransport.

I have already writen packages that can replace these in memory infrastures. For example have written commercially using the EvenStore (Greg Young), SQL, MonogDB, and recently for Azure tables and eventHub.

## EventStore (Greg Young)

You could replace the in memory version with Greg Young Eventstore if you have it installed.  

https://github.com/cbrown11/DomainRepository.EventStore. 

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




# Screen Shots
## Running Services
![Running Services](https://github.com/cbrown11/PlatformContactExample/blob/master/Screen%20Snapshots/running%20services.png)
## Playground
![Playground](https://github.com/cbrown11/PlatformContactExample/blob/master/Screen%20Snapshots/Playground.png)
## Voyager
![Voyager](https://github.com/cbrown11/PlatformContactExample/blob/master/Screen%20Snapshots/Voyager.png)
## UI Components
![Contact.Projection.API](https://github.com/cbrown11/PlatformContactExample/blob/master/Screen%20Snapshots/swagger.png)
![UI Contacts](https://github.com/cbrown11/PlatformContactExample/blob/master/Screen%20Snapshots/UI_contacts.png)
![find Contact](https://github.com/cbrown11/PlatformContactExample/blob/master/Screen%20Snapshots/UI_findContac.png)
