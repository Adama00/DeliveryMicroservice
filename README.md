Delivery Microservice

The Delivery Microservice is responsible for managing the delivery of orders in an e-commerce or classified application. It handles order delivery requests, tracks delivery status, and calculates delivery fees.
Table of Contents

    Features
    Technologies Used
    Requirements
    Setup Instructions
        1. Clone the repository
        2. Install dependencies
        3. Setup environment variables
        4. Run the application
    API Endpoints
        POST /api/deliveries
        GET /api/deliveries/{orderId}
    Environment Variables
    Docker Support
    License

Features

    Create a delivery request for an order.
    Track the delivery status.
    Calculate delivery fees based on distance.
    Integration with RabbitMQ for inter-service communication with the Order Management microservice.

Technologies Used

    .NET 6 for building the API.
    Entity Framework Core for database management.
    RabbitMQ for message-based communication between services.
    PostgreSQL with PostGIS for geographic calculations.
    Google Maps API for calculating distances.

Requirements

    .NET 6 SDK
    PostgreSQL (with PostGIS extension)
    RabbitMQ
    Google Maps API Key

Setup Instructions
1. Clone the repository

bash

git clone https://github.com/Adama00/DeliveryMicroservice.git
cd DeliveryMicroservice
2. Install dependencies

Restore the .NET dependencies by running:

bash

dotnet restore

3. Setup environment variables

Create an .env.local file in the root directory and configure the following variables:

bash

POSTGRES_CONNECTION_STRING=your_postgres_connection_string
RABBITMQ_URI=your_rabbitmq_connection_uri
GOOGLE_MAPS_API_KEY=your_google_maps_api_key

Example appsettings.json for local development:

json

{
  "ConnectionStrings": {
    "PostgresConnection": "Host=localhost;Port=5432;Database=deliverydb;Username=yourusername;Password=yourpassword"
  },
  "RabbitMQ": {
    "Uri": "amqp://your-username:your-password@your-rabbitmq-host:5672/vhost"
  },
  "GoogleMaps": {
    "ApiKey": "your_google_maps_api_key"
  }
}

4. Run the application

To run the Delivery Microservice locally, use the following command:

bash

dotnet run

The application will be available on https://localhost:5001.
API Endpoints
POST /api/deliveries

Creates a new delivery request for an order.

Request:

json

{
  "orderId": 12345,
  "pickupLatitude": 5.650547,
  "pickupLongitude": -0.190594,
  "deliveryLatitude": 5.680932,
  "deliveryLongitude": -0.210621,
  "scheduledTime": "2024-09-10T15:00:00Z",
  "customerId": "456",
  "fare": 50.00
}

Response:

json

{
  "deliveryId": 9876,
  "orderId": 12345,
  "status": "Pending",
  "fare": 50.00,
  "distance": 4.5,
  "orderPlaced": "2024-09-08T10:00:00Z",
  "scheduledTime": "2024-09-10T15:00:00Z"
}

GET /api/deliveries/{orderId}

Retrieves the delivery details for a specific order.

Response:

json

{
  "deliveryId": 9876,
  "orderId": 12345,
  "status": "In Transit",
  "pickupLocation": {
    "latitude": 5.650547,
    "longitude": -0.190594
  },
  "deliveryLocation": {
    "latitude": 5.680932,
    "longitude": -0.210621
  },
  "fare": 50.00,
  "distance": 4.5,
  "orderPlaced": "2024-09-08T10:00:00Z",
  "scheduledTime": "2024-09-10T15:00:00Z",
  "orderFulfilled": null
}

Environment Variables
Variable	Description
POSTGRES_CONNECTION_STRING	Connection string to the PostgreSQL database
RABBITMQ_URI	Connection string for RabbitMQ
GOOGLE_MAPS_API_KEY	API key for Google Maps for calculating distances
Docker Support

To build and run the service using Docker, ensure you have Docker installed and follow these steps:

    Build the Docker image:

    bash

docker build -t delivery-microservice .

Run the Docker container:

bash

docker run -d -p 5000:80 --name delivery-microservice delivery-microservice

Stop the container:

bash

    docker stop delivery-microservice

License

This project is licensed under the MIT License.
