@ECHO OFF
REM Installs and starts a windows service ,set two things ServiceExeName.exe and ServiceName
REM The following directory is for .NET 4.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%

echo Installing IEPPAMS Win Service...
echo ---------------------------------------------------
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /i D:\Other\BB\CronJob\CronService\bin\Debug\CronService.exe
echo ---------------------------------------------------
pause
echo Done.

REM Start the server
echo starting Windows service 
REM net start CronService
