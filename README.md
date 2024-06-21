# Travel and Accommodation Booking Platform API
This API provides a range of endpoints designed for the management of various hotel-related tasks, including booking handling, hotel and city information management, and offering guest services.

## Key Features

### User Authentication

- **User Registration**: Allows new users to create accounts by providing necessary information.
- **User Login**: Enables registered users to log in securely to access booking features.

### Global Hotel Search

- **Search by Various Criteria**: Users can search for hotels using criteria such as hotel name, room type, room capacities, price range, and other properties through text fields.
- **Comprehensive Search Results**: Provides users with detailed information about hotels matching their search criteria.

### Image Management

- **Management of Images and Thumbnails**: Allows for the addition, deletion, and updating of images associated with cities, hotels, and rooms.

### Popular Cities Display

- **Display Most Visited Cities**: Showcases popular cities based on user traffic, allowing users to explore trending destinations easily.

### Email Notifications

- **Booking Confirmation Emails**: Sends confirmation emails to users upon successful booking, containing essential information such as total price, hotel location on the map, and other relevant details.
- **Enhanced User Communication**: Facilitates effective communication with users, keeping them informed about their bookings.

### Admin Interface

- **Search, Add, Update, and Delete Entities**: Provides administrators with full control over system entities, enabling efficient management of cities, hotels, rooms, and other components.
- **Streamlined Administrative Tasks**: Simplifies administrative tasks through a user-friendly interface, enhancing system maintenance.


## Endpoints
### Amenities

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/amenities                | Retrieve a page of amenities                   |
| POST   | /api/amenities                | Create a new amenity                           |
| GET    | /api/amenities/{id}           | Get an amenity specified by ID                 |
| PUT    | /api/amenities/{id}           | Update an existing amenity                     |

### Auth

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| POST   | /api/auth/login               | Processes a login request                      |
| POST   | /api/auth/register-guest      | Processes registering a guest request          |

### Bookings

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| POST   | /api/user/bookings            | Create a new Booking for the current user      |
| GET    | /api/user/bookings            | Get a page of bookings for the current user    |
| DELETE | /api/user/bookings/{id}       | Delete an existing booking specified by ID     |
| GET    | /api/user/bookings/{id}       | Get a booking specified by ID for the current user |
| GET    | /api/user/bookings/{id}/invoice | Get the invoice of a booking specified by ID as PDF for the current user |

### Cities

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/cities                   | Retrieve a page of cities                      |
| POST   | /api/cities                   | Create a new city                              |
| GET    | /api/cities/trending          | Returns TOP N most visited cities (trending cities) |
| PUT    | /api/cities/{id}              | Update an existing city specified by ID        |
| DELETE | /api/cities/{id}              | Delete an existing city specified by ID        |
| PUT    | /api/cities/{id}/thumbnail    | Set the thumbnail of a city specified by ID    |

### Discounts

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/room-classes/{roomClassId}/discounts | Retrieve a page of discounts for a room class |
| POST   | /api/room-classes/{roomClassId}/discounts | Create a discount for a room class specified by ID |
| GET    | /api/room-classes/{roomClassId}/discounts/{id} | Get an existing discount by ID         |
| PUT    | /api/room-classes/{roomClassId}/discounts/{id} | Delete an existing discount specified by ID |

### Guests

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/user/recently-visited-hotels | Retrieve the recently N visited hotels by the current user |

### Hotels

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/hotels                   | Retrieve a page of hotels                      |
| POST   | /api/hotels                   | Create a new hotel                              |
| GET    | /api/hotels/search            | Search and filter hotels based on specific criteria |
| GET    | /api/hotels/featured-deals    | Retrieve N hotel featured deals                |
| GET    | /api/hotels/{id}              | Get hotel by ID for guest                      |
| PUT    | /api/hotels/{id}              | Update an existing hotel specified by ID        |
| DELETE | /api/hotels/{id}              | Delete an existing hotel specified by ID        |
| GET    | /api/hotels/{id}/room-classes | Get room classes for a hotel specified by ID for Guests |
| PUT    | /api/hotels/{id}/thumbnail    | Set the thumbnail of a hotel specified by ID    |
| POST   | /api/hotels/{id}/gallery      | Add a new image to a hotel's gallery specified by ID |

### Owners

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/owners                   | Retrieve a page of owners                      |
| POST   | /api/owners                   | Create a new owner                             |
| GET    | /api/owners/{id}              | Get an existing owner by ID                    |
| PUT    | /api/owners/{id}              | Update an existing owner                       |

## Reviews

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/hotels/{hotelId}/reviews | Retrieve a page of reviews for a hotel specified by ID |
| POST   | /api/hotels/{hotelId}/reviews | Create a new review for a hotel specified by ID |
| GET    | /api/hotels/{hotelId}/reviews/{id} | Get a review specified by ID for a hotel specified by ID |
| PUT    | /api/hotels/{hotelId}/reviews/{id} | Update an existing review specified by ID for a hotel specified by ID |
| DELETE | /api/hotels/{hotelId}/reviews/{id} | Delete an existing review specified by ID for a hotel specified by ID |

### RoomClasses

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/room-classes             | Retrieve a page of room classes                |
| POST   | /api/room-classes             | Create a new room class                        |
| PUT    | /api/room-classes/{id}        | Update an existing room class specified by ID  |
| DELETE | /api/room-classes/{id}        | Delete an existing room class specified by ID  |
| POST   | /api/room-classes/{id}/gallery | Add a new image to a room class's gallery specified by ID |

## Rooms

| HTTP Method | Endpoint                      | Description                                    |
|--------|-------------------------------|------------------------------------------------|
| GET    | /api/room-classes/{roomClassId}/rooms | Retrieve a page of rooms for a room class |
| POST   | /api/room-classes/{roomClassId}/rooms | Create a new room in a room class specified by ID |
| GET    | /api/room-classes/{roomClassId}/rooms/available | Retrieve a page of available rooms for a room class |
| PUT    | /api/room-classes/{roomClassId}/rooms/{id} | Update an existing room with ID in a room class specified by ID |
| DELETE | /api/room-classes/{roomClassId}/rooms/{id} | Delete a room by ID in a room class specified by ID |

## Architecture
- **Clean Architecture**
  - **External Layers**: 
    - Web: Controllers for handling requests and managing client-server communication.
    - Infrastructure: Manages external resources such as databases, email service, PDF generation, image service, and auth management.
  - **Core Layers**:
    - Application Layer: Implements business logic and orchestrates interactions between components.
    - Domain Layer: Contains fundamental business rules and entities, independent of external concerns like databases or user interfaces.

## Technology Stack Overview

### Technologies Used

- **C#**: Main programming language.
- **ASP.NET Core**: Framework for building high-performance, cross-platform web APIs.

### Database

- **Entity Framework Core**: Employing Entity Framework Core for streamlined object-relational mapping (ORM) within the .NET ecosystem, simplifying database interactions.
- **SQL Server**: Utilizing SQL Server for reliable and scalable backend database management, ensuring efficient storage and retrieval of application data.

### Image Storage

- **Firebase Storage**: Part of Google's Firebase platform, offers cloud-based storage for images in this hotel booking system. It provides scalability, reliability, and seamless API integration.

### API Documentation and Design

- **Swagger/OpenAPI**: For API specification and documentation.
- **Swagger UI**: Provides a user-friendly interface for API interaction.

### Authentication and Authorization

- **JWT (JSON Web Tokens)**: For secure transmission of information between parties.

### Monitoring and Logging

- **Serilog**: Logging library for .NET applications.

### Design Patterns

- **RESTful Principles**:  Adhering to RESTful design principles to ensure that APIs are designed for simplicity, scalability, and ease of use.
- **Repository Pattern**:  Implementing the repository pattern to abstract the data layer, enhancing application maintainability, testability, and cleanliness by decoupling data access logic from business logic.
- **Options Pattern**: For efficient configuration management within the application.
- **Unit of Work**: Manages transactions across multiple data operations, ensuring atomicity and data consistency within a single logical unit.

### Security

- **Data Encryption**: Password hashing using `Microsoft.AspNet.Identity.IPasswordHasher`.

## API Versioning
This API leverages the `Asp.Versioning.Mvc` library to implement a streamlined header-based versioning mechanism. This approach facilitates seamless client access to different API versions without requiring adjustments to the base URL or path. By utilizing headers, the versioning process is standardized, offering improved clarity and maintainability. This method also enhances flexibility and ease of integration for users while ensuring optimal compatibility with various client applications.

Users can effortlessly specify their preferred API version by including the x-api-version header in their requests. In instances where no version is explicitly specified, the API seamlessly defaults to the latest available version. For instance:

To explicitly request version 1.0:
```bash
curl -X GET "localhost:8080/api/cities" -H "x-api-version: 1.0"
```


To utilize the latest version by default:
```bash
curl -X GET "localhost:8080/api/cities"
```

## Setup Guide

This guide provides instructions on setting up an existing ASP.NET API project. Follow these steps to clone the repository, configure the `appsettings.json` file, and run the API locally.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) installed on your system.
- Running SQL Server instance with a database.

### Step-by-Step Guide

#### 1. Clone the Repository

Clone the repository of your existing ASP.NET API project to your local machine:

```bash
git clone https://github.com/Nedal-Esrar/Travel-and-Accommodation-Booking-Platform.git
```

#### 2. Navigate to the Project Directory

Change your current directory to the root directory of your ASP.NET API project:

```bash
cd Travel-and-Accommodation-Booking-Platform/src/TABP.Api
```

#### 3. Configure `appsettings.json`

Open the `appsettings.json` file located in your project directory and configure the connection string for SQL Server. Replace the `<connection_string>` placeholder with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "SqlServer": "<connection_string>"
  }
}
```

#### 4. Run the API Locally

Start the ASP.NET API project using the following command:

```bash
dotnet run
```

The API will be accessable using http://localhost:8080.

The swagger UI will open automatically where you can try and explore the endpoints or you can open it using http://localhost:8080/swagger.

##### To access admin's functionalities, authenticate with these credentials:
- **Email:** admin@hotelbooking.com
- **Password:** 11aaAA@@

## Get Involved
Your Feedback and Contributions are Welcome!

### Ways to Contribute:
- **Feedback**: Share your thoughts and ideas.
- **Issue Reporting**: Help us by reporting any bugs or issues on GitHub.
- **Code Contributions**: Contribute to the codebase.

### Contact and Support:
Email: [nedalesrarahmad@gmail.com](mailto:nedalesrarahmad@gmail.com).

GitHub: [Nedal-Esrar](https://github.com/Nedal-Esrar).

Thank you for your interest. I look forward to hearing from you!
