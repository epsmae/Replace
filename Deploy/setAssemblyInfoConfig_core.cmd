@echo off
SETLOCAL ENABLEDELAYEDEXPANSION 

rem check root path from where the AssemblyFiles get searched
if "%~1"=="" goto wrongParam
set root=%1

rem check version number
if "%~2"=="" goto wrongParam
set Version="%~2"

set REPLACE_DLL="..\Develop\Replace.App\bin\Release\netcoreapp2.1\publish\win64\Replace.App.dll"

rem **********************************************
rem User defined values
rem ##############################################
set Company=""
set Product="Replace"
rem the ¸ (U0184) generates a © sign
set Copyright=""
set Trademark=""
set Culture=""
rem ##############################################


echo root: %root%
echo version %Version%

dotnet %REPLACE_DLL% -c config_with_tags.xml -t #0,%root% #1,%Version% -v




rem everything ok!
:end
echo Assembly infos changed successfully!
ENDLOCAL
exit /B 0

rem one or more arguments are not correct
:wrongParam
echo usage: %~0 rootPath Version Product
echo rootPath: The directory from which the script searches recursively the AssemblyInfo files. 
echo Version: The Version number which should get inserted into the AssemblyInfo files. 
echo Example %~0 ..\..\Develop\App 1.2.3.4
ENDLOCAL
exit /B 1

rem Error occured on replacement
:error
echo replacement of a assemblyinfo failed!
ENDLOCAL
echo Error occurred: errlev: %errorlevel%
exit /B 1