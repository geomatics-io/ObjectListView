@echo off
cls

REM Delete old packages
if exist *.nupkg del *.nupkg

REM Generate NuGet package
%cd%\tools\nuget.exe pack ObjectListView.nuspec

REM Publish to Myget
%cd%\tools\nuget.exe push *.nupkg 98937c8a-91e2-41ca-8e7d-cc7494b8ad3a -Source "github" -configfile %cd%\tools\NuGet.Config