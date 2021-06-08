# TodoBlazor
Project build application using Blazor

# How to run application

## In windows

1. Run command
```Bash
docker-compose build
docker-compose up
```
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
