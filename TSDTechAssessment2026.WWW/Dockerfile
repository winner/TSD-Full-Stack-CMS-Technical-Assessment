FROM node:20-slim AS sass
WORKDIR /src
COPY package.json package-lock.json ./
RUN npm ci
COPY wwwroot/scss wwwroot/scss
RUN npx sass wwwroot/scss:wwwroot/css --style=compressed --no-source-map

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY ["TSDTechAssessment2026.WWW.csproj", "."]
RUN dotnet restore
COPY . .
COPY --from=sass /src/wwwroot/css wwwroot/css
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TSDTechAssessment2026.WWW.dll"]
