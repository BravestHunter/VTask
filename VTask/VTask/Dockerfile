FROM mcr.microsoft.com/dotnet/sdk:7.0

COPY . /source

WORKDIR /source
RUN dotnet publish -c release -o /app

WORKDIR /app

EXPOSE 80

ENV ASPNETCORE_URLS http://+:80

CMD dotnet VTask.dll
