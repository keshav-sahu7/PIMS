## run these commands to add necessary packages
```bash
dotnet add package Microsoft.EntityFrameworkCore -v 6.0.14
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 6.0.14
dotnet add package Microsoft.EntityFrameworkCore.Tools -v 6.0.14
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.0
dotnet add package Microsoft.IdentityModel.Tokens --version 6.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.10.0
```
## install necessary tools
```bash
dotnet tool install --global dotnet-ef
```

## to create migration and database
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## to add nuget packages
```bash
dotnet add package LinqKit
```

