[![Build status](https://ci.appveyor.com/api/projects/status/115e41aqm9s820b5?svg=true)](https://ci.appveyor.com/project/nstevens1040/anglesharpparser)  
# AngleSharpParser  
## PowerShell Core Quick Start  
### Load the DLL into PowerShell Core
```ps1
try { Set-ExecutionPolicy Bypass -Scope Process -Force } catch {}
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
[System.Net.WebClient]::New().DownloadFile(
    "https://github.com/nstevens1040/AngleSharpParser/releases/latest/download/AngleSharpParser.1.0.58.nupkg",
    "$($ENV:USERPROFILE)\Desktop\AngleSharpParser.1.0.58.nupkg"
)
$null = mkdir "$($ENV:USERPROFILE)\Desktop\AngleSharpParser.1.0.58"
Expand-Archive -Path "$($ENV:USERPROFILE)\Desktop\AngleSharpParser.1.0.58.nupkg" -DestinationPath "$($ENV:USERPROFILE)\Desktop\AngleSharpParser.1.0.58"
Add-Type -Path "$($ENV:USERPROFILE)\Desktop\AngleSharpParser.1.0.58\lib\net6.0\AngleSharpParser.dll"
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
$p = [AngleSharp.Parser]::new()
$document = $p.GetDomDocument($html_string)
$document.GetElementById("test").TextContent
```  
