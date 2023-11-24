# Explicación para desarrollo local

Puede trabaja este proyecto sin necesidad de levantarlo con docker si tiene .Net instalado pero recomiendo que tenga docker y lo use almenos para la base de datos, asi sin necesidad te tener postgres localmente

## Levantar Db con Docker Compose

1. Levantar el contenedor
```bash
docker exec -it auth-api-db bash
```
2. Conectarnos al contenedor y usar **bash**

```bash
psql -U root -d authdb
```
3. Actualizar las tablas en la base de datos
Copie y pegue el contenido del archivo Models/[InitDb.sql](./Models/SQL/InitDB.sql)

## Comandos PGSQL mas usados

| Comando                                    | Descripción                                       |
|--------------------------------------------|---------------------------------------------------|
| `\l` o `\list`                            | Listar todas las bases de datos.                 |
| `\c NOMBRE_BASE_DE_DATOS`                  | Conectarse a una base de datos específica.       |
| `\dt`                                     | Listar todas las tablas en la base de datos.    |
| `\d NOMBRE_TABLA`                         | Mostrar detalles de una tabla específica.       |
| `\du` o `\dg`                             | Listar todos los roles de usuario o grupos.     |
| `\dt+` o `\d+ NOMBRE_TABLA`                | Mostrar detalles extendidos de una tabla.       |
| `\q`                                       | Salir del cliente psql.                          |
| `\i ARCHIVO_SQL`                          | Ejecutar un archivo SQL desde la línea de comandos. |
| `\e`                                       | Abrir un editor externo para escribir consultas.   |
| `\df` o `\df FUNCION`                     | Listar todas las funciones o detalles de una función. |
| `\di` o `\di INDICE`                      | Listar todos los índices o detalles de un índice. |
| `\dn` o `\dn ESQUEMA`                     | Listar todos los esquemas o detalles de un esquema. |
| `\timing`                                 | Mostrar el tiempo de ejecución de las consultas.   |

dotnet ef dbcontext scaffold "Host=localhost;Database=authdb;Username=root;Password=0000" Npgsql.EntityFrameworkCore -o Models
