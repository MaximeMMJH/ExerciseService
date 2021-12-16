#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ExerciseService.csproj", "."]
RUN dotnet restore "./ExerciseService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ExerciseService.csproj" -c Release -o /app/build

FROM build AS dev
WORKDIR "/src"
ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:80"]

FROM build AS publish
RUN dotnet publish "ExerciseService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExerciseService.dll"]