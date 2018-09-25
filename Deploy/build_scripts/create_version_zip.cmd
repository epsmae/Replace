@echo off
SETLOCAL ENABLEDELAYEDEXPANSION 

if "%~1"=="" goto wrongParam
set OUTPUT_DIRECTORY=%1

if "%~2"=="" goto wrongParam
set DLL="%~2"

if "%~3"=="" goto wrongParam
set FILES_TO_ZIP="%~3"

if "%~4"=="" goto wrongParam
set SUFFIX="%~4"

set PWD=%cd%


cd %OUTPUT_DIRECTORY%

REM get version name
call powershell.exe -Command "[System.Reflection.Assembly]::LoadFrom('%DLL%').GetName().Version.ToString();" > out.tmp
set /p VERSION=< out.tmp
del /Q out.tmp

SET VERSION=%VERSION:.=_%

REM Create zip file with 7zip
call "%SevenZip%" a "Replace_%VERSION%_%SUFFIX%.zip" %FILES_TO_ZIP%

cd %PWD%

ENDLOCAL
goto end

rem one or more arguments are not correct
:wrongParam
echo usage: %~0 path dll files_to_zip suffix
echo path: The directory from where the zip file should be created 
echo dll: DLL to get the version
echo files_to_zip: to zip: files which should be included in the zip file
echo suffix: suffix for the zip file name

:end
