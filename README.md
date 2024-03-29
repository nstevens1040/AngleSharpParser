[![Build status](https://ci.appveyor.com/api/projects/status/115e41aqm9s820b5?svg=true)](https://ci.appveyor.com/project/nstevens1040/anglesharpparser)  
# AngleSharpParser  
## PowerShell Core Quick Start  
### Load the DLL into PowerShell Core
```ps1
try { Set-ExecutionPolicy Bypass -Scope Process -Force } catch {}
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
[System.Net.WebClient]::New().DownloadFile(
    "https://github.com/nstevens1040/AngleSharpParser/releases/latest/download/AngleSharpParser-latest.nupkg",
    "$($ENV:USERPROFILE)\Desktop\AngleSharpParser-latest.nupkg"
)
$null = mkdir "$($ENV:USERPROFILE)\Desktop\AngleSharpParser-latest"
Expand-Archive -Path "$($ENV:USERPROFILE)\Desktop\AngleSharpParser-latest.nupkg" -DestinationPath "$($ENV:USERPROFILE)\Desktop\AngleSharpParser-latest"
Add-Type -Path "$($ENV:USERPROFILE)\Desktop\AngleSharpParser-latest\lib\net6.0\AngleSharpParser.dll"
```  
### Do a simple test
```ps1
$html_string = @"
<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1"/>
        <title>Testing HTML</title>
    </head>
    <body>
        <h1>Heading</h1>
        <article>
            <section>
                <h2>subtitle</h2>
                <p>paragraph</p>
                <span id="test">Test succeeded!</span>
            </section>
        </article>
    </body>
</html>
"@
$parser = [Angle.Sharp]::New()
$document = $parser.GetDomDocument($html_string)
$document.GetElementById("test").TextContent
```  
The output should read
```ps1
Test succeeded!
```  
