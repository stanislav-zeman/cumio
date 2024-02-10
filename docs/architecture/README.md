# Architecture

This directory contains all materials related to the design of the architecture.

For designing the infrastucture of the system the Google Cloud Platform was selected.

All of the solutions described bellow include a GCP Cloud DNS and Cloud Load Balancer.

The DNS directs clients to the Load Balancer which is used as a proxy for the systems services.
Depending on the specific path, traffic, services usage and availibility, the client gets routed to
the most relevant service.

## Simple

This design should be sufficient for 1 000 active users.

![Simple Diagram](./cumio-architecture-simple.drawio.png)

## Advanced

This design should be sufficient for 10 000 active users.

![Advanced Diagram](./cumio-architecture-advanced.drawio.png)

## Complex

This design should be sufficient for 1 000 000 active users.

![Complex Diagram](./cumio-architecture-complex.drawio.png)
