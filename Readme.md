# Redis Channel Worker POC

## Overview

This project is a Proof of Concept (POC) demonstrating the integration of Redis with the Channel Pattern in ASP.NET Core for efficient background processing. Redis serves as a temporary data store during operations, enabling robust and scalable data handling.

## Key Features

- **Channel Worker**: Utilizes `System.Threading.Channels` for thread-safe queue management
- **Redis Storage**: Implements temporary data persistence with Redis
- **Background Processing**: Processes tasks asynchronously using `IHostedService`
- **Scheduled Jobs**: Leverages Quartz.NET for automated task scheduling

## System Architecture

1. **Request Handling**
   - Incoming requests are serialized to JSON format
   - Data is stored in Redis using List data structures
   - Time-to-live (TTL) values are assigned to manage storage efficiently
   
2. **Data Processing**
   - ChannelWorker continuously consumes data from the Channel
   - Processing occurs without blocking the main application thread
   - Results are routed to other system components as needed

3. **Scheduled Task Management**
   - Quartz.NET manages time-based job execution
   - Tasks can be configured to run at specified intervals

## Redis Implementation

This project leverages Redis for temporary data management as follows:

- **Redis Lists**: Used for FIFO (First In, First Out) request storage
- **Key Expiration**: TTL values prevent excessive storage consumption
- **Persistence Options**: Configurable retention periods based on data importance

## System Components

1. **RedisService.cs**
   - Manages Redis connections and operations
   - Provides methods for storing and retrieving request data

2. **ChannelWorker.cs**
   - Implements background processing as a Hosted Service
   - Consumes and processes data from the Channel

3. **ChannelFactory.cs**
   - Creates and configures Channels for inter-component communication

4. **Quartz Jobs**
   - Defines scheduled tasks and their execution patterns
   - May interact with Redis or perform other system operations

## Installation and Usage

### System Requirements
- .NET 8.0 or later
- Redis Server
- Docker (optional, for containerized Redis)

### Getting Started

1. **Redis Setup**
   ```bash
   # Run Redis using Docker
   docker run --name redis -p 6379:6379 -d redis
   ```

2. **Project Execution**
   ```bash
   # Build and run the project
   dotnet build
   dotnet run
   ```

3. **API Access**
   - Swagger UI: `https://localhost:5001/swagger`
   - API Endpoints: As defined in the Controllers

## Important Considerations

- **Redis Key Expiration**: Configure TTL values appropriately to prevent data loss before processing
- **Memory Management**: Monitor Redis memory usage to avoid performance degradation
- **Error Handling**: Implement robust error handling for Redis connection issues

## Performance Optimization

- Adjust Channel capacity based on expected workload
- Configure worker count according to system resources
- Set Redis TTL values based on data retention requirements

## Troubleshooting

- **Redis Connection Issues**: Verify Redis connectivity and ConnectionString configuration
- **Key Expiration Problems**: Check for services that might delete keys or set shorter TTL values
- **Performance Bottlenecks**: Use Redis CLI commands like `INFO` and `MONITOR` to diagnose issues

---

Developed by [Team/Organization Name]