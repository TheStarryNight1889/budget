FROM mcr.microsoft.com/dotnet/sdk

WORKDIR /App

COPY . /App

RUN dotnet publish api.csproj -c Release

ENTRYPOINT ["dotnet", "bin/Debug/net5.0/publish/api.dll"]