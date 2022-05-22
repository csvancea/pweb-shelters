#!/bin/bash

set -e

dotnet dev-certs https --clean
dotnet dev-certs https -t

until dotnet ef database update --project ShelterHelper.Api/; do
sleep 1
done

echo "SQL Server is up"
dotnet run --project ShelterHelper.Api/
sleep 5
dotnet run --project ShelterHelper.EmailService/


