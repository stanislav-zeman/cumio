![Cumio API](https://github.com/stanislav-zeman/cumio/actions/workflows/actions.yml/badge.svg)

# CUMIO

Cumio is audio streaming platform.

## Assignment

Design an architecture for platform that allows uploading, sharing and streaming such content as podcasts, music or any other audio. 
The system allows users to subscribe to a specific publisher or a content creator while also allowing one-time payments.

> [!NOTE]
> This project is a part of the Software Architectures course taught at FI MUNI during the autumn semester 2023.

## Technologies and patterns

This project repository is basen on [Vertical Slice Architecture template repository by Nadir Bad](https://github.com/nadirbad/VerticalSliceArchitecture), and reuses most of its technologies.

- [ASP.NET API with .NET 8](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- CQRS with [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://fluentvalidation.net/)
- [AutoMapper](https://automapper.org/)
- [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
