echo Compile

%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe ActiveUp.Net.sln /p:Configuration=Retail
rem %SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe ActiveUp.Net.Compact.sln /p:Configuration=Retail
%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe Build\Documentation.shfbproj /p:Configuration=Release

echo Prepare Release

IF EXIST Release GOTO NOWINDIR
MKDIR Release
:NOWINDIR

Del Release\*.* /Q /S
XCOPY "Class Library\ActiveUp.Net.Mail\bin\Retail\*.xml" Release\
XCOPY "Class Library\ActiveUp.Net.Mail\bin\Retail\*.dll" Release\
XCOPY Samples Release\Samples /s /i /y
XCOPY Build\Help\Documentation.chm Release\
XCOPY COPYRIGHT.txt Release\
XCOPY LICENSE.txt Release\

PAUSE

