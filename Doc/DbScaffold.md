# Hacer scaffold de la base de datos para actualiar tablas

Por ejemplo si tengo el siguiente string de conexión pgsql: 

`postgres://user:1234@ejemplo-de-host:5432/database?sslmode=require`

Podemos extraer el siguiente comando scaffold: 

```bash
dotnet ef dbcontext scaffold "Host=ejemplo-de-host;Database=database;Username=user;Password=1234" Npgsql.EntityFrameworkCore -o Models
```