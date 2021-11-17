FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY LinkManagerApi.csproj .
RUN dotnet restore
COPY . .

RUN dotnet publish -c release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app .
ENTRYPOINT [ "dotnet","LinkManagerApi.dll" ]