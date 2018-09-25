

RD /S /Q ..\..\Develop\Replace.App\bin
RD /S /Q ..\..\Develop\Replace\bin

call build_and_publish.cmd
call build_and_publish_core.cmd

call create_version_zip.cmd ..\..\Develop\Replace.App\bin\Release\netcoreapp2.1\publish\win64 Replace.App.dll * core_win64
call create_version_zip.cmd ..\..\Develop\Replace.App\bin\Release\netcoreapp2.1\publish\linux64 Replace.App.dll * core_linux64
call create_version_zip.cmd ..\..\Develop\Replace.App\bin\Release\netcoreapp2.1\publish\osx Replace.App.dll * core_osx64
call create_version_zip.cmd ..\..\Develop\Replace\bin\Release Replace.exe Replace.exe win
