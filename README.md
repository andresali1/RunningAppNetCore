# RunningAppNetCore
Proyecto MVC básico con Net 6 y Entity Framework; crud simple de una pagina que funciona como red social de corredores  

### Tecnologías Utilizadas
:keyboard: C# 10  
:keyboard: .Net 6  
:minidisc: SqlServer 2019  
:computer: Visual Studio 2022  

### :open_book: Configuración  
1. En una carpeta del sistema ejecutar el comando :arrow_forward: git clone https://github.com/andresali1/RunningAppNetCore.git
2. Dentro de la carpeta "RunningAppNetCore" hay una carpeta llamada "SQL_Scripts"; usando SQLServer:    
    * Crear una Base de datos llamada "RunningApp", el nombre está en el archivo llamando DDL.sql
3. Abrir la solución con Visual Studio 2022:
   * Usando la consola del administrador de paquetes se debe ejecutar el comando Update-Database para crear las tablas que usa la aplicación
   * Buscar el archivo /Data/Seed.cs partiendo de la raiz del proyecto despues de la línea 114 están los correos y contraseñas por defecto de la aplicación
5. Revizar appsettings.json 
    * Revisar la cadena de conexión y cambiarla por la cadena de la base de datos a usar
    * Revisar las credenciales de Cloudinary: Se debe iniciar sesión o registrarse en "https://cloudinary.com/"; luego se debe escribir en appsettings.json las credenciales que brinda dicha página dentro de la llave "CloudinarySettings"
    * Revisar las credenciales de Ipinfo: Se debe iniciar sesión o registrarse en "https://ipinfo.io/"; luego se debe escribir en appsettings.json el token para ese usuario dentro de la llave "IpInfo"
6.  Ejecutar el proyecto
