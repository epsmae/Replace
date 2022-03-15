RD /S /Q ..\..\Develop\Replace.App\bin

call build_and_publish.cmd

call create_version_zip.cmd ..\..\Develop\Replace.App\bin\Release\net6.0\publish\linux-x64 ..\..\Replace.App.dll * linux_x64
call create_version_zip.cmd ..\..\Develop\Replace.App\bin\Release\net6.0\publish\osx-x64 ..\..\Replace.App.dll * osx_x64
call create_version_zip.cmd ..\..\Develop\Replace.App\bin\Release\net6.0\publish\win-x64 ..\..\Replace.App.dll * win_x64
call create_version_zip.cmd ..\..\Develop\Replace.App\bin\Release\net6.0\publish\win-x64-self ..\..\Replace.App.dll * win_x64_self