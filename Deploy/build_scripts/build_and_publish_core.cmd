
set Configuration=Release
set ProjectDirectory=..\..\Develop\Replace.App
set ProjectFile=Replace.App.csproj
set OutputDirectory=bin\%Configuration%\netcoreapp2.1\publish



echo projDir %ProjectDirectory%
echo file %ProjectFile%
echo out %OutputDirectory%

set startDirectory=%cd%

cd %ProjectDirectory%

dotnet build %ProjectFile% --configuration %Configuration%
dotnet publish %ProjectFile% /p:Configuration=%Configuration% /p:DeployOnBuild=true /p:PublishProfile=Win64Profile -o %OutputDirectory%\win64
dotnet publish %ProjectFile% /p:Configuration=%Configuration% /p:DeployOnBuild=true /p:PublishProfile=Linux64Profile -o %OutputDirectory%\linux64
dotnet publish %ProjectFile% /p:Configuration=%Configuration% /p:DeployOnBuild=true /p:PublishProfile=OsxProfile -o %OutputDirectory%\osx

cd %startDirectory%