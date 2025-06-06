
# MovieConnect API

## Features  
- **Search Movie Details**: Search your favorite movies across multiple providers and get detailed movie information.   
- **Strategy Design Pattern**: Easily switch between multiple movie and video providers with a clean, extendable design.  
- **Typed HttpClient with Polly**: Resilient API calls with retry and circuit breaker policies implemented via Polly.  
- **Memory Cache**: Cache frequently accessed movie data for improved performance.  
- **Clean Architecture**: Organized codebase following Clean Architecture principles.  
- **CQRS**: Command Query Responsibility Segregation implemented for separation of concerns.  
- **Global Error Handler**: Centralized middleware to catch and handle exceptions globally, returning consistent and meaningful error responses.  
- **Unit and Integration Tests**: Comprehensive tests ensuring application stability and correctness.  
- **Dockerized Deployment**: Ready to deploy with Docker containers.  
- **CI/CD Pipeline**: GitHub Actions workflow for automated builds, tests, and Azure deployments.

## Technologies Used  
- **IDE**: Visual Studio 2022 / VS Code  
- **Framework**: .NET 8  
- **Architecture**: Clean Architecture, CQRS  
- **HTTP Client**: Typed HttpClient with Polly for resiliency  
- **Caching**: MemoryCache  
- **Testing**: xUnit  
- **Containerization**: Docker  
- **CI/CD**: GitHub Actions  
- **Cloud**: Azure Web Apps
- **Container Registry**: Azure Container Registry

## Setup Instructions  

1. **Clone the Repository**  
   ```bash
   git clone https://github.com/yourusername/MovieConnectAPI.git
   cd MovieConnect.API
   ```

2. **Run the Application**  
   Run the API project from Visual Studio or use the .NET CLI:  
   ```bash
   dotnet run --project MovieConnect.API
   ```

3. **Access the API**  
   Navigate to Swagger UI at: `https://localhost:<port>/swagger/index.html`

## Deployment

This project uses **GitHub Actions** for CI/CD to automate the entire process of building, testing, and deploying the application to **Azure Web Apps** with Docker support.

- The **Docker image** is built during the pipeline and pushed to **Azure Container Registry (ACR)**.
- The **Azure Web App** is automatically deployed with the latest Docker image from the registry.

You can access the live API Swagger UI at:  
[https://movieconnectapi-a8a7bmaxcmbgdnh3.westeurope-01.azurewebsites.net/swagger/index.html](https://movieconnectapi-a8a7bmaxcmbgdnh3.westeurope-01.azurewebsites.net/swagger/index.html)

## Acknowledgments  
- Thanks to all open-source providers and libraries used in this project.  
- Inspired by Clean Architecture and best practices in .NET development.
