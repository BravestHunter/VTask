dotnet dev-certs https --clean
del %USERPROFILE%\.aspnet\https\vtask.pfx

::rmdir /s /q %USERPROFILE%\.aspnet