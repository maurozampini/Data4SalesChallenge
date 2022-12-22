# Data4SalesChallenge
Este proyecto fue creado con .Net Core 6.

# Dependencias
- Dapper
- MySql.Data
- System.Data.SqlClient
- Newtonsoft.json

# Requerimientos
- [Microsoft Visual Studio 2022](https://visualstudio.microsoft.com/es/downloads/)
- Crear los archivos .env y .settings en las carpetas indicadas en la puesta en marcha

# Puesta en marcha

1- Clonar repositorio

2- Ubicarse en la carpeta Data4SalesChallenge y crear el archivo ".env". El mismo debe contener:

`USER=<User>`

`PASSWORD=<Password>`

`SERVER=<Server name>`

`DB=<Database name>`

3- Ubicarse en la carpeta Data4SalesChallenge\Tests\bin\Debug\net6.0 y crear el archivo "settings.json". El mismo debe contener:
`{
    "USER": "<User>",
    "PASSWORD": "<Password>",
    "SERVER": "<Server name>",
    "DB": "<Database name>"
}`

4- Compilar solución

5- Llamar al endpoint "Startup" el cual crea las tablas y registros correspondientes.
# Autor
### Mauro Zampini