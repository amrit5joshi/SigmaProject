# Sigma Candidate Application

## Overview
The Sigma Candidate Application 

### System Overview
- **Relational Database**: Utilized SQLserver for Data Storage.
- **Caching**: In Memory Caching
- **Entity Framework Core**: ORM for data access and management.
- **API Versioning**: Supports multiple versions of the API for backward compatibility.
- **Serilog**: Facilitates structured logging for better monitoring and diagnostics.
- **Swagger**: Automatically generates API documentation for easy exploration and testing.
- **FluentValidation**: Implements robust validation rules for models.
- **Coverlet**: Provides code coverage metrics for unit tests.
- **xUnit and Moq**: Used for testing and mocking dependencies with In Memory Database.

# Time spent:

- 2 hour - Set up the project, directory structure and implement the basic functionality of handling candidate information (Injecting the required dependencies)
- 1 hour - Added logging (serilog), and implement caching for performance
- 1.5 hour- Set testing for efficient API validation (xunit, moq for mocking, coverlet for coverage).
- 1 hour - Tested the APIs using Swagger and resolve any issues identified
- 1 hour - Created comprehensive documentation, including a README with setup instructions and API details.

### Total time spent: 6.5 hours

# List of Improvements

### 1. Modular Design
- Shift to a microservices architecture to enhance scalability and maintainability.

### 2. MediatR Integration
- Using the Mediator pattern with MediatR for improved separation of concerns and better handling of commands and queries.

### 3. Enhanced Security
- Use OAuth 2.0 for secure access to the API, ensuring only authorized users can manage candidate information.

### 4. Caching Mechanism
- Utilize caching solutions (like Redis) for quick access to frequently needed data, reducing database load.

### 5. Advanced Search Capabilities
- Integrate full-text search tools (e.g., Elasticsearch) to enhance candidate search speed and relevance.

### 6. User-Friendly Interface
- Creating a simple UI prototype to help HR visualize and manage candidate data.

### 7. Docker
- Dockerize the application for efficient deployment and managing the application in the production.

### 8. CI/CD Pipeline
- Establish a CI/CD setup for automated testing and deployment, streamlining the integration of new changes.


## Feature Improvements

### 1. Candidate Searching Feature
- Implement a search functionality to filter candidates based on various criteria (e.g., name, email).

### 2. Get By ID Feature
- Enable fetching a candidate's information using their unique ID for easier access.

### 3. Pagination
- Implement pagination for the Candidate list API to manage large datasets effectively.

### 4. Email
- Implement email sending feature to notify candidate selected for interviews.


# Assumptions

- **Data Integrity**: The application will maintain accurate and consistent candidate information throughout its lifecycle.

- **Scalability**: The application is designed to handle an increasing number of candidates as the user base grows.

## Getting Started

### Prerequisites
- SQL Server
- Visual Studio 

### Installation

1. Clone the repository:
   
   ```bash
   git clone https://github.com/amrit5joshi/SigmaProject.git
 
 ### Database Migrations

#### To add a new migration to the project, use the following command. Make sure to replace YourMigrationName with a descriptive name for your migration:

    
       dotnet ef migrations add Initial --context DataContext -o ..\SigmaProject.Data\Migrations\Core -p .\SigmaProject.Data -s .\SigmaProject.API

### For test coverage, I have used coverlet

    
       coverlet SigmaProject.Test/bin/Debug/net8.0/SigmaProject.Test.dll --target "dotnet" --targetargs "test --no-build" --format cobertura

![image](https://github.com/user-attachments/assets/8b08748d-9967-4efd-a53f-3c1f9303971a)



