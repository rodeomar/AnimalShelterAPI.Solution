# Animal Shelter API

Welcome to the Animal Shelter API! This API allows you to manage and retrieve information about animals in a shelter.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Usage](#usage)

## Prerequisites

Before you begin, make sure you have the following prerequisites installed:

- .NET Core SDK
- MySQL

## Getting Started

1. Clone this repository to your local machine:

   ```sh
   git clone https://github.com/rodeomar/AnimalShelterAPI.Solution.git
   ```

2. Navigate to the project directory:

   ```sh
   cd AnimalShelterAPI
   ```

3. Update the `appsettings.json` file with your database connection string.
  ```json
  {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=db_name;User=username;Password=password;"
      }
  }
  ```
   
5. Install the required packages:

   ```sh
   dotnet restore
   ```

6. Apply database migrations:

   ```sh
   dotnet ef database update
   ```

7. Run the API locally:

   ```sh
   dotnet run
   ```

## Usage

You can use tools like `curl`, `Postman`, or any programming language's HTTP client to interact with the API. The API provides endpoints to retrieve, create, update, and delete animal records.

## Documentation

API documentation is available using Swagger UI. After starting the API locally, navigate to:

```
https://localhost:5000/index.html
```

You can also access the API documentation at:

```
https://localhost:5000/api/docs
```
Known Bugs
None
```
License Please let me know if you have any questions or concerns raed@alkhanbashi.gmail.com

Copyright (c) 2023 Raed Alkhanbashi.
