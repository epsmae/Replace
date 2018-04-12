@echo off
SETLOCAL ENABLEDELAYEDEXPANSION 

rem check root path from where the AssemblyFiles get searched
if "%~1"=="" goto wrongParam
set root=%1

rem check version number
if "%~2"=="" goto wrongParam
set Version="%~2"

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

	Replace.exe -f %%f -s "AssemblyCompany.+?]" -r !repCompany!
	
	Replace.exe -f %%f -s "AssemblyCopyright.+?]" -r !repCopyRight!
	
	Replace.exe -f %%f -s "AssemblyProduct.+?]" -r !repProduct!
	
	Replace.exe -f %%f -s "AssemblyVersion.+?]" -r !repVersion!
	
	Replace.exe -f %%f -s "AssemblyFileVersion.+?]" -r !repFileVersion!
	
	Replace.exe -f %%f -s "AssemblyCulture.+?]" -r !repCulture!
	
	Replace.exe -f %%f -s "AssemblyTrademark.+?]" -r !repTrademark!
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
echo Example %~0 ..\..\Develop\App 1.2.3.4
ENDLOCAL
exit /B 1

rem Error occured on replacement
:error
echo replacement of a assemblyinfo failed!
ENDLOCAL
echo Error occurred: errlev: %errorlevel%
exit /B 1