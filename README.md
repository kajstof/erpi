# erpi

Track module of a ERP class system

## How to setup local environment

### Starting database (postgres) in docker, it can be done with docker-compose tool:

There are three services:
- postgres (`localhost:5442`) - required
- pgadmin4 (`http://localhost:5443`) - for database management
- adminer (`http://localhost:5444`) - for database management

```bash
docker-compose up
```

### Creating secrets (instead of configuration in `appsettings.json`)

```bash
cd src/Erpi.Api/
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:TruckDbContext" "Host=localhost;Database=postgres;Username=postgres;Password=postgres" 
```

To list secrets:

```bash
dotnet user-secrets list
```

### Migrations

#### Installing ef tools (prerequisite)

```bash
dotnet tool install -g dotnet-ef
```

#### Creating migration

```bash
cd ../..
dotnet ef migrations add InitialMigration -p src/Erpi.Trucks.Infrastructure -s src/Erpi.Api --context TruckDbContext
```

#### Performing migrations

```bash
dotnet ef database update -p src/Erpi.Trucks.Infrastructure -s src/Erpi.Api --context TruckDbContext
```

### Generating tokens for users

For example:

```bash
cd ResourceManager/src/ResourceManager.Api
dotnet user-jwts create --name anna --role admin --role user
dotnet user-jwts create --name elza --role user
dotnet user-jwts create --name olaf --role user
```

To list tokens:

```bash
dotnet user-jwts list --output json
```
