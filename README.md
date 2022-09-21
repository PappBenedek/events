# Events

This is a small c# application which exposes resources via api calls.
My goal was to create an application which can act as a base for an online shop.
Disclaimer this is just for demonstration purposes I designed this project without any intention to completeness.

used technologies:

.NET - Entity Framework Core - Xunit

Micrososft SQL server

Docker

Javascript

## How to build and run the application:
### Prerequisites
docker, docker compose installed on your system

```bash
git clone https://github.com/PappBenedek/events.git
cd events
docker compose build
docker compose up
```

## A few words about the architecture and design decisions
The first most conspicuous thing is we don't have any kind of service, just controllers and repositories. This is weird right?
So the thing is to fulfill all things that I wanted to achive to majoriti of the buisniss logic is how we access the data,
so I'v could moved some of the logic outside of the repository but I did not see the point in this case because we are talking about
a few hunder lines of code.
### About the generic repository and base entity
If we would like to introduce new entities in the future, we are in a preatty good shape,
we just have to inherit from the base entity and we already have all the CRUD opertaion aviable.
Also we have some base entries in the DB such as creation date, modification date.
### Bad practices
I would like to highlight that the way how I manage connection string is really bad, you rather should use secret manager.
