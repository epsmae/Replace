# Regex Console Replace Application
Contains a replace utility for windows, Linux and Mac

## Why use it

The Replace.exe can be used to replace a regex match with a specific content.
A possible use case is for example to set the assembly information.

## Installation
Since Version 2.0.0.0 there are four diffrent downloads.

Download the [latest release](https://github.com/epsmae/Replace/releases)

| OS            | Platform  | Deployment           | Download file                    |
| ------------- | ----------| -------------------- |--------------------------------- |
| Windows x64   | .net 6.0  | Framework dependent  | Replace_X_X_X_X_win_x64.zip      |
| Windows x64   | .net 6.0  | Self Contained       | Replace_X_X_X_X_win_x64_self     |
| Linux x64     | .net 6.0  | Framework dependent  | Replace_X_X_X_X_linux_x64.zip    |
| Osx x64       | .net 6.0  | Framework dependent  | Replace_X_X_X_X_osx_x64.zip      |

### .net Downloads
[.net 6 Downloads](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Usage
There are many online regex tester to create a specific regex I like "regex101" the most
[Online regex tester](https://regex101.com)

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

use it inside a script [Example Script](/Deploy/setAssemblyInfoConfig_net6.cmd)
```
set repCompany=AssemblyCompany(\"!Company!\")]

rem loop over all AssemblyInfo.cs files and replace the values
for /R %root% %%f in (*AssemblyInfo.cs) do (
	Replace.exe -f %%f -s "AssemblyCompany.+?]" -r !repCompany!
)
```

### Config Replacement
There are a few character which are not allowed in a xml file therefore these have to be esscaped
```
"   &quot;
'   &apos;
<   &lt;
>   &gt;
&   &amp;
```

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
  <FileNames>
    <string>AssemblyInfo.cs</string>
  </FileNames>
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
  <FileNames>
    <string>AssemblyInfo.cs</string>
  </FileNames>
  <PathToSearch>../Develop</PathToSearch>
</Config>
```


## Create new release

Build in release mode
```
cd Deploy\build_scripts
build_and_publish.cmd
```

Test release and set next version
```
cd Deploy
setAssemblyInfoConfig_net6.cmd ..\Develop 2.3.4.5
setAssemblyInfoConfig_net6-self.cmd ..\Develop 3.4.5.6
```

Create release package
```
cd Deploy\build_scripts
build_and_create_artefacts.cmd
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


Credit to the online markdown editor:
Dilinger https://dillinger.io/
