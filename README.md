# PeopleSearch
PeopleSearch is an ASP.NET Core Responsive SPA.
It is built with Visual Studio 2019 as a single Visual Studio project.

# Building blocks
Back-end: .NET Core 3.1;
ORM: Entity Framework Core / SQL server;
Front-end: Angular/Rxjs/Material/Bootstrap.
Test: PeopleSearch.UnitTests project (MSTest), list-people.component.spec.tc(Jasmin)

# Description
The client app displays cards for all entities in the database and lets user enter a search string.
It sends a search criteria to the service part via HTTP GET request. 
The service Controller returns people information, which first name, last name or full name (first + last names) contains a given sting (case-insensitive).
if a search string is blank, the service returns all entities.
While UI is waiting for a response it displays a progress indicator.
If there are no entities for a given search criteria, UI shows the corresponding message.
If an error happens, the app logs an error message to the console.

# Notes
1. The application operates with the localdb 
("PeopleDbConnection": "server=(localdb)\\MSSQLLocalDB;database=PeopleDB;Trusted_Connection=true"), 
and has the migration to seed the database with 7 entities.
2. It performs fake data retrieval delay that can be adjusted by the setting "DataRetrievalDelay" in appsettings.json (currently is set to 3 sec). 
3. The service and client parts support all CRUD operations, though UI only calls read operation.

# Please run 'update-database' command in Package Manager Console to synchronize the DB (apply the data migration provided in the projrct).








