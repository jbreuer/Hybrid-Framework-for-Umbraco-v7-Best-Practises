echo off
echo **********************************
echo To run this build script you need to have Microsoft Build Tools 2013 installed (included in VS2013), you can download from here http://www.microsoft.com/en-gb/download/details.aspx?id=40760
echo **********************************
pause
echo on

Call nuget.exe restore ..\UmbracoProject.sln
Call "%programfiles(x86)%\MSBuild\12.0\Bin\MsBuild.exe" ..\UmbracoProject.sln