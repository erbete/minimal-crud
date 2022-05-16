FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY "./server" .
RUN dotnet test ./test
RUN rm -rf ./test
RUN dotnet restore "./src/minimal-crud.csproj"
RUN dotnet publish "./src/minimal-crud.csproj" -c release -o /app --no-restore 

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "minimal-crud.dll"]
