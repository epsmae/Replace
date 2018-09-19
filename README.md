# Regex Console Replace Application
Contains a replace utility for windows

## Why use it

The Replace.exe can be used to replace a regex match with a specific content.
A possible use case is for example to set the assembly information.

## Installation
Since Version 2.0.0.0 there are four diffrent downloads.
Currently the .net core assemblies can not yet be bundled to one execution file.
Therefore the .net framework console application is still in this repository next to the .net core console application. The deployment is framework dependent therefore the .net has to be installed on the os.

Download the [latest release](https://github.com/epsmae/Replace/releases)

| OS            | Platform             | Deployment           | Download file                    |
| ------------- | -------------------- | -------------------- |--------------------------------- |
| Windows x64   | .net Framework 4.6.1 | Framework dependent  | Replace_X_X_X_X_win.zip          |
| Windows x64   | .net Core 2.1        | Framework dependent  | Replace_X_X_X_X_core_win64.zip   |
| Linux x64     | .net Core 2.1        | Framework dependent  | Replace_X_X_X_X_core_linux64.zip |
| Osx x64       | .net Core 2.1        | Framework dependent  | Replace_X_X_X_X_core_osx64.zip   |


### .net Framework 4.6.1
[Microsoft .net 4.6.1 Framework Download](https://www.microsoft.com/de-ch/download/details.aspx?id=49982)
```
Replace.exe
```


### .net core Platform
[Linux .net core install guide](https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x)
[Windows .net core install guide](https://docs.microsoft.com/en-us/dotnet/core/windows-prerequisites?tabs=netcore21)
[OSX .net core install guide](https://docs.microsoft.com/en-us/dotnet/core/macos-prerequisites?tabs=netcore2x)
```
dotnet Replace.App.dll
```

## Usage
```
Usage: replace.exe -f file -s regex -r replacement
Usage: replace.exe -c config.xml
Usage: replace.exe -c config.xml -t #0,Tag0 #1,Tag1
```


### Single file Replacement

```
Replace.exe project.csproj "<Version>.*</Version>" "<Version>1.0.1.5</Version>"
Replace.exe AssemblyInfo.cs "android:versionCode=\".+?\"" "android:versionCode=\"1.0.1.5\""
Replace.exe AssemblyInfo.cs "AssemblyCompany.+?]" "AssemblyCompany(\"Code AG\")]"
```

use it inside a script [Example Script](/Deploy/setAssemblyInfo.cmd)
```
set repCompany=AssemblyCompany(\"!Company!\")]

rem loop over all AssemblyInfo.cs files and replace the values
for /R %root% %%f in (*AssemblyInfo.cs) do (
	Replace.exe -f %%f -s "AssemblyCompany.+?]" -r !repCompany!
)
```

### Config Replacement

```
Replace.exe -c config.xml
```

#### config.xml [Example Config](/Deploy/config.xml)
``` xml
<?xml version="1.0" encoding="utf-8"?>
<Config>
  <RegexReplaceValues>
    <RegexReplaceValue>
      <Regex>AssemblyVersion.+?]</Regex>
      <ReplaceValue>AssemblyVersion("0.0.3.4")]</ReplaceValue>
    </RegexReplaceValue>
  </RegexReplaceValues>
  <FileExtensions>
    <string>AssemblyInfo.cs</string>
  </FileExtensions>
  <PathToSearch>../Develop</PathToSearch>
</Config>
```


### Config Tag Replacement
Example to replace #0 with 1.5.1.0

```
Usage: replace.exe -c config.xml -t #0,1.5.1.0
```

#### config.xml
``` xml
<?xml version="1.0" encoding="utf-8"?>
<Config>
  <RegexReplaceValues>
    <RegexReplaceValue>
      <Regex>AssemblyVersion.+?]</Regex>
      <ReplaceValue>AssemblyVersion("#0")]</ReplaceValue>
    </RegexReplaceValue>
  </RegexReplaceValues>
  <FileExtensions>
    <string>AssemblyInfo.cs</string>
  </FileExtensions>
  <PathToSearch>../Develop</PathToSearch>
</Config>
```
## License

MIT License

Copyright (c) 2018 Manuel Eugster

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
