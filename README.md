# Trailblazer.NET
This project contains code for the Trailblazer.NET library. This library contains 
an opinionated set of components that I like to use when working on DDD projects. 

## Getting started
Follow these steps to get started:

 - `git clone https://github.com/wmeints/trailblazer`
 - `dotnet build -c Debug`

## Running mutation tests
This project includes infrastructure to run mutation tests using [Stryker.NET](https://stryker-mutator.io/stryker-net/).
To run the mutation tests follow these steps:

 - `cd test/Trailblazer.Tests`
 - `dotnet stryker`
