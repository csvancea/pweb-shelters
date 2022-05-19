FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN dotnet tool install -g dotnet-ef --version 6.0.5
ENV PATH $PATH:/root/.dotnet/tools

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5002

RUN chmod +x ./entrypoint.sh

ENTRYPOINT /bin/bash ./entrypoint.sh