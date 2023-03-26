FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder

COPY . /source

WORKDIR /source
RUN dotnet publish -c release -o /app


#FROM mcr.microsoft.com/dotnet/aspnet:7.0
FROM mcr.microsoft.com/dotnet/sdk:7.0

COPY --from=builder /app /app

WORKDIR /app
RUN dotnet dev-certs https -t

ENV ASPNETCORE_URLS "http://+:80;https://+:443"

EXPOSE 80
EXPOSE 443

ENTRYPOINT dotnet VTask.dll
