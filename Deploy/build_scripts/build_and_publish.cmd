dotnet build --configuration Release ..\..\Develop\Replace.sln

dotnet publish -p:PublishProfile=Linux64Profile ..\..\Develop\Replace.App\Replace.App.csproj
dotnet publish -p:PublishProfile=OsxProfile ..\..\Develop\Replace.App\Replace.App.csproj
dotnet publish -p:PublishProfile=Win64Profile ..\..\Develop\Replace.App\Replace.App.csproj
dotnet publish -p:PublishProfile=Win64SelfProfile ..\..\Develop\Replace.App\Replace.App.csproj




