FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release


WORKDIR /src
#Copy nuget.config to the root of the source directory
COPY nuget.config .

ARG GITHUB_TOKEN
ENV GITHUB_TOKEN=$GITHUB_TOKEN

COPY ["src/Message/Message.csproj", "src/Message/"]
RUN dotnet restore "src/Message/Message.csproj"
COPY src/Message/. /src/Message/
WORKDIR /src/Message
RUN ls -l /src/Message
RUN dotnet build "Message.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Message.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Message.dll"]
