# A Microservice Software Architecture Demo

## Summary
* Demonstrates a design and code organization of a typical back-end microservice in a real-life cloud-native web application. 
* This approach combines the benefits of clean abstractions as in "Clean Architecture" style with the practicality of keeping cohesive software components
that tighly collaborate together as proposed in "Vertical Slices" architectural style. 
* It welcomes continuous architecture quality evaluation and makes the system redisign a commodity by allowing for straigthforward recombination of functionalities between microservices.
* Automated application code testing and test-driven development is at the core the established software development process. 

## Technologies
* ASP.NET Core on C# (REST Web API)
* Entity Framework Core
* AWS (ECS, CloudWatch)
* SQL Server
* Serilog

## The Code Solution Structure
* `.github/worflows`: a folder with GitHub workflow files for CI/CD
* `Demo.Microservice.Api`: ASP.NET Core Web API project that exposes the application functionalities through REST endpoints
* `Demo.Microservice`: a class library with the self-contained and unit-testable core application functionalities
* `Demo.Microservice.Core`: a reusable framework with generalized core building blocks for implementation of the application functionalities (powers `Demo.Microservice`, for instance)
* `Demo.Microservice.Test`: contains a comprehensive suite of unit tests for each application functionality
* `Demo.Microservice.Test.Core`: a class libary of reusable building blocks for implementing unit tests
* `task-definition.json`: contains a configuration of Docker containers used for deployment on AWS ECS (used by the `github/workflow/deploy.yml` workflow).

## Instructions for Running the Solution
The demo functionalities in the solution can be run from within the `Demo.Microservice.Test` project.
Please note: This demo code does not support automatic deployment on AWS at the moment. More instructions and condfiguration details are to be provided.
