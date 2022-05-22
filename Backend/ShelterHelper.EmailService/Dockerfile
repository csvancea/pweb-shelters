FROM mcr.microsoft.com/dotnet/sdk:6.0

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 6000

RUN chmod +x ./entrypoint.sh

ENTRYPOINT /bin/bash ./entrypoint.sh