# Arreglqando el list y rceate de persona del
- Voy en la parte del list. cshtml no funciona producto.value ✔️

- Voy en implementacion del crud en pedido ✔️

- proximamemnte a darla a data base y migraciones ✔️

- COnexiones con base de datos my sql ✔️

- Anadir validacion de usuario ✔️

- Mejorar Experiencia Usuario ✔️

- Despliegue ✔️



Tips
1. Porbablemente el proyecto en el estado actual no se funcional esto se debe a que por obias razones elimine mis crdenciales y las carpetas bin/obj/MIgraciones

    - En /Persistencia/AppRepositorio en el archivo App Context
        - Intruduzca sus credenciales para su base de datos sql

    - Ejecutar en /Persisntencia 
```bash
    dotnet ef migrations add MigraInicial --startup-project ..\Consola\
```

```bash
    dotnet ef database update --startup-project ..\Consola\
```

```bash
    dotnet build
```

1. En /Frontend buscar ej script appsettings.json

    - Poner las credenciales.

1.1 Si tiene una version 3.x - 5.x cambiar FrontendIdentityDbContextConnection por  IdentityDataContext

```bash
    dotnet ef migrations add IdentityInitial --context IdentityDataContext
    dotnet ef database update --context IdentityDataContext
```
1.2 si tiene una version 6.x 

Ejecute
```bash
dotnet ef migrations add IdentityInitial  --context FrontendIdentityDbContext
dotnet ef database update  --context FrontendIdentityDbContext
```