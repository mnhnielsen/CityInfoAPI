#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /source
#
#COPY *.csproj .
#RUN dotnet restore
#
#COPY . .
#RUN dotnet publish -c Release -o /app --no-restore
#
#FROM mcr.microsoft.com/dotnet/runtime:6.0
#WORKDIR /app
#EXPOSE 7228
#COPY --from=build /app .
#ENTRYPOINT ["dotnet", "dotnetapp.dll"]