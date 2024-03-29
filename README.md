# TodoBlazor
Project build application using Blazor and Web API. The code will using Docker Container for host to application.

# Required

- Dotnet core latest version, refer from version 5.0
- Docker desktop
- Nodejs
- Visual Code or Visual Studio 2019 latest version

# How to run application

## In windows

1. Run command
```Bash
docker-compose build
docker-compose up
```
OR 
Using Visual Studio 2019
## In Linux (Ubuntu)

1. Add host.docker.internal to hosts and mapping it to current machine
```BASH
docker run -it --add-host=host.docker.internal:host-gateway alpine cat /etc/hosts
```
2. Run command
```Bash
docker-compose build
docker-compose up
```

Go to address

* Todo API - http://localhost:5102

* Todo Blazor App - http://localhost:5103

## Run by Tye command

1. Install Tye command at [link](https://github.com/dotnet/tye)

2. Go to folder cointain file tye.yaml

3. Open command or powershell. Run the command as below:
```BASH
tye run
```
4. Go to address

* Tye Dashboard - http://localhost:8000

* Todo API - http://localhost:8001

* Todo Blazor App - http://localhost:8002


## Acess to application

- API URL: http://localhost:5102 or http://host.docker.internal:5102
- Blazor app: http://localhost:5103 or http://host.docker.internal:5103
- SQL Server: `host.docker.internal,5433` - username: `sa` - password: `Pass@word`
