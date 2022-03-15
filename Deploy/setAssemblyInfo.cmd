@echo off
SETLOCAL ENABLEDELAYEDEXPANSION 

rem check root path from where the AssemblyFiles get searched
if "%~1"=="" goto wrongParam
set root=%1

rem check version number
if "%~2"=="" goto wrongParam
set Version="%~2"

set REPLACE_EXE="..\Develop\Replace\bin\Release\Replace.exe"

rem **********************************************
rem User defined values
rem ##############################################
set Company="Replace AG"
set Product="Replace"
set Copyright="(U0184)"
set Trademark="https://github.com/epsmae/Replace"
set Culture="Culture"
rem ##############################################


echo Directory: %root%
echo Company: %Company%
echo Product: %Product%
echo Version: %Version%

rem has to be declared outside of the for loop in order to work!
set repCompany=AssemblyCompany(\"!Company!\")]
set repCopyRight=AssemblyCopyright(\"!Copyright!\")]
set repProduct=AssemblyProduct(\"!Product!\")]
set repVersion=AssemblyVersion(\"!Version!\")]
set repFileVersion=AssemblyFileVersion(\"!Version!\")]
set repCulture=AssemblyCulture(\"!Culture!\")]
set repTrademark=AssemblyTrademark(\"!Trademark!\")]


rem loop over all AssemblyInfo.cs files and replace the values
for /R %root% %%f in (*AssemblyInfo.cs) do (

	%REPLACE_EXE% -f %%f -s "AssemblyCompany.+?]" -r !repCompany!
	
	%REPLACE_EXE% -f %%f -s "AssemblyCopyright.+?]" -r !repCopyRight!
	
	%REPLACE_EXE% -f %%f -s "AssemblyProduct.+?]" -r !repProduct!
	
	%REPLACE_EXE% -f %%f -s "AssemblyVersion.+?]" -r !repVersion!
	
	%REPLACE_EXE% -f %%f -s "AssemblyFileVersion.+?]" -r !repFileVersion!
	
	%REPLACE_EXE% -f %%f -s "AssemblyCulture.+?]" -r !repCulture!
	
	%REPLACE_EXE% -f %%f -s "AssemblyTrademark.+?]" -r !repTrademark!
)


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
echo Example %~0 ..\Develop 1.2.3.4
ENDLOCAL
exit /B 1

rem Error occured on replacement
:error
echo replacement of a assemblyinfo failed!
ENDLOCAL
echo Error occurred: errlev: %errorlevel%
exit /B 1