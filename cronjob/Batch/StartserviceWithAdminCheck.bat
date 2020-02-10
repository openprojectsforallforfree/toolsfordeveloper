@ECHO OFF
echo starting Windows service 
REM check admin rights
@echo off
AT > NUL
IF %ERRORLEVEL% EQU 0 (
    ECHO you are Administrator
    REM start 
	net start BsoftCronService
) ELSE (
    ECHO you are NOT Administrator. Exiting...
    ECHO Please start your service manually or use command net start with escalated previlage
    PING 127.0.0.1 > NUL 2>&1
    EXIT /B 1
)
pause
 
