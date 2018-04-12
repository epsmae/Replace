# Replace
Conatins a replace utility for windows

## Why use it

The Replace.exe can be used to replace a regex match with a specific content.
A possible use case is for example to set the assembly information.

## Installation

1. Download the [latest release](https://github.com/epsmae/Replace/releases)

## Usage
```
Usage: replace.exe -f file -s regex -r replacement
Usage: replace.exe -c config.xml
```


### Single file Replacement

```
Replace.exe project.csproj "<Version>.*</Version>" "<Version>1.0.1.5</Version>"
Replace.exe AssemblyInfo.cs "android:versionCode=\".+?\"" "android:versionCode=\"1.0.1.5\""
Replace.exe AssemblyInfo.cs "AssemblyCompany.+?]" "AssemblyCompany(\"Code AG\")]"
```

use it inside a script [See also SetAssemblyInfo](Deploy\setAssemblyInfo.cmd)
```
set repCompany=AssemblyCompany(\"!Company!\")]

rem loop over all AssemblyInfo.cs files and replace the values
for /R %root% %%f in (*AssemblyInfo.cs) do (
	Replace.exe -f %%f -s "AssemblyCompany.+?]" -r !repCompany!
)
```

### Replacement with a config file

```
Replace.exe 
```

#### Config Xml
``` xml
<?xml version="1.0" encoding="utf-8"?>
<Config>
  <RegexReplaceValues>
    <RegexReplaceValue>
      <Regex>AssemblyCompany.+?]</Regex>
      <ReplaceValue>AssemblyCompany(\"Replace AG\")]</ReplaceValue>
    </RegexReplaceValue>
    <RegexReplaceValue>
      <Regex>AssemblyCopyright.+?]</Regex>
      <ReplaceValue>AssemblyCopyright(\"Copyright Replace AG\")]</ReplaceValue>
    </RegexReplaceValue>
    <RegexReplaceValue>
      <Regex>AssemblyProduct.+?]</Regex>
      <ReplaceValue>AssemblyProduct(\"Replace\")]</ReplaceValue>
    </RegexReplaceValue>
    <RegexReplaceValue>
      <Regex>AssemblyVersion.+?]</Regex>
      <ReplaceValue>AssemblyVersion(\"0.0.3.4\")]</ReplaceValue>
    </RegexReplaceValue>
    <RegexReplaceValue>
      <Regex>AssemblyFileVersion.+?]</Regex>
      <ReplaceValue>AssemblyFileVersion(\"0.0.1.2\")]</ReplaceValue>
    </RegexReplaceValue>
    <RegexReplaceValue>
      <Regex>AssemblyCulture.+?]</Regex>
      <ReplaceValue>AssemblyCulture(\"Culture\")]</ReplaceValue>
    </RegexReplaceValue>
    <RegexReplaceValue>
      <Regex>AssemblyTrademark.+?]</Regex>
      <ReplaceValue>AssemblyTrademark(\"Trademark\")]</ReplaceValue>
    </RegexReplaceValue>
  </RegexReplaceValues>
  <FileExtensions>
    <string>AssemblyInfo.cs</string>
    <string>AssemblyFile.txt</string>
    <string>Assembly.as</string>
  </FileExtensions>
  <PathToSearch>./Develop</PathToSearch>
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
