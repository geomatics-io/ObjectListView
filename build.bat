@echo off
cls

REM Find the current version of MSBuild
for /f "delims=" %%i in ('"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe') do set output=%%i

REM Restore packages
%cd%\tools\nuget.exe restore src\ObjectListView.sln

REM Clean, then build the solution
"%output%" src\ObjectListView.sln /t:Clean,Build