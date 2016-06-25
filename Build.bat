@echo off
IF NOT DEFINED SHFBROOT (
	echo Please install Sandcastle help file builder
	echo https://shfb.codeplex.com/
	pause
	exit
)

echo Compiling...
%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe ActiveUp.Net.sln /p:Configuration=Retail /p:Platform="Any CPU"
%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe Build\Documentation.shfbproj /p:Configuration=Release

echo Preparing release...

IF NOT EXIST Release (
	MKDIR Release
)

Del Release\*.* /Q /S
XCOPY "Class Library\bin\Retail\*.xml" Release\
XCOPY "Class Library\bin\Retail\*.dll" Release\
rem Samples shouldn't be released until they are fixed
rem XCOPY Samples Release\Samples /s /i /y
XCOPY Build\Help\Documentation.chm Release\
XCOPY COPYRIGHT.txt Release\
XCOPY LICENSE.txt Release\

PAUSE

