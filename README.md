
# Overview

I kept everything in one solution but obvioulsy this would be seperated. If the Startup is passed over in VS, then runnning the solution will start all the required services and expose their endpoints. 

Though please note everything in memory at the moment, so database persitence will be lost if the services are stopped.

Didnt managed to get the UI fully done but everything can be done by the gatewayAPI, which uses the GraphQL pattern. If you haven't used graphQL before it is introspection, so the playground wil have intellisense (hit ctrl+space for a hint).

https://graphql.org/learn/


# Platform Services and UI

There are three services to run and the UI. 

## PlatformUI 
The Code for the UI is in a different Git Repository `https://github.com/cbrown11/PlatformExampleUI`

Navigate to `http://localhost:4200/`

## Platform.GatewayAPI

Main api is `http://localhost:3000/graphql`. Ive attached playground and voyager to the api so can easily view and explorer 

- For Playground Ground Navigate to `http://localhost:3000/ui/playground`
- For Voyager  Navigate to `http://localhost:3000/ui/voyager`

## Contact.Service

- Endpoint to this service is by a command message. Though could expose the Command as an API call.
- Events will be written to an eventstore and the domain repository will send this events on to the Bus.

## Contact.Projection.API

- The service will subscribe and listen to the domain events thats its interested in
- Using NserviceBus learning Transport so will automatically setup the pub/sub. But other transporters will needs to be configured. Having written in the pass so can be done via the config (SQL, RabbitMQ)

Navigate to view sawagger of the API `http://localhost:58118/swagger/index.html`

# Playground Examples

To create a contact you will need to run the following mutation, which the create component would run in the UI. This is where the data validation is mainly done.

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




